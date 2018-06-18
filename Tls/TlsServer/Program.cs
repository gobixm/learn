using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace TlsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.ListenAnyIP(5001, listenOptions =>
                    {
                        var cert = new X509Certificate2("server.p12", "123123123");
                        var authority = new X509Certificate2("ca.cer");
                        var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions
                        {
                            ClientCertificateMode = ClientCertificateMode.AllowCertificate,
                            SslProtocols = SslProtocols.Tls12,
                            ServerCertificate = cert,
                            ClientCertificateValidation = (clientCertificate, ch, arg3) =>
                            {
                                var chain = new X509Chain
                                {
                                    ChainPolicy =
                                    {
                                        RevocationMode = X509RevocationMode.NoCheck,
                                        RevocationFlag = X509RevocationFlag.ExcludeRoot,
                                        VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority,
                                        VerificationTime = DateTime.Now,
                                        UrlRetrievalTimeout = new TimeSpan(0, 0, 0)
                                    }
                                };

                                chain.ChainPolicy.ExtraStore.Add(authority);

                                var isChainValid = chain.Build(clientCertificate);

                                if (!isChainValid)
                                {
                                    var errors = chain.ChainStatus
                                        .Select(x => $"{x.StatusInformation} ({x.Status})")
                                        .ToArray();
                                    var certificateErrorsString = "Unknown errors.";

                                    if (errors.Length > 0) certificateErrorsString = string.Join(", ", errors);

                                    throw new Exception(certificateErrorsString);
                                }

                                var valid = chain.ChainElements
                                    .Cast<X509ChainElement>()
                                    .Any(x => x.Certificate.Thumbprint == authority.Thumbprint);

                                if (!valid) throw new Exception("Thumbprints did not match.");

                                return true;
                            }
                        };
                        listenOptions.UseHttps(httpsConnectionAdapterOptions);
                    });
                });
        }
    }
}