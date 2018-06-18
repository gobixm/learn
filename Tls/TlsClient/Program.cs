using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace TlsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var cert = new X509Certificate2("client.p12", "123123123");
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                SslProtocols = SslProtocols.Tls12
            };
            handler.ClientCertificates.Add(cert);
            handler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                Console.WriteLine(message);
                Console.WriteLine(certificate2.SubjectName);
                Console.WriteLine(arg3);
                Console.WriteLine(arg4);
                return true;
            };
            
            var client = new HttpClient(handler);

            var response = client.GetAsync("https://localhost:5001").Result;
            
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}