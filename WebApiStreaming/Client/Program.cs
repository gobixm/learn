using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var file1 = File.OpenRead(@"d:\temp\MapTiles.sqlitedb");
            //var file2 = File.OpenRead(@"d:\temp\MapTiles.sqlitedb");
            using (var content = new MultipartFormDataContent())
            {
//                var pushContent2 = new PushStreamContent((stream, httpContent, transportContext) =>
//                {
//                    //TransferData(file2, stream).Wait();
//                    file2.CopyTo(stream);
//                    stream.Close();
//                });
//                
                content.Add(new StreamContent(file1), "files", "file1.txt");
                //content.Add(pushContent2, "files", "file2.txt");
                //content.Headers.ContentLength = null;
                
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.TransferEncodingChunked = true;
                var result = client.PostAsync(new Uri("http://127.0.0.1:3210/api/file"), content)
                    .Result;
                Console.ReadKey();
            }
        }

        private static Task TransferData(FileStream from, Stream to)
        {
            return Task.Run(() =>
            {
                var buff = new byte[4096];
                int read;
                while ((read = from.Read(buff, 0, buff.Length)) > 0)
                {
                    to.Write(buff, 0, read);
                    //Console.WriteLine("sent");
                }
                to.Close();
            });
        }
    }
}
