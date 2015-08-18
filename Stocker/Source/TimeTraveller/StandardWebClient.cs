using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TimeTraveller
{
    public class StandardWebClient : IWebClient
    {        
        async public Task<string> DownloadStringTaskAsync(string address)
        {
            using(var client = new WebClient())
            {
                return await client.DownloadStringTaskAsync(address);
            }
        }
    }
}
