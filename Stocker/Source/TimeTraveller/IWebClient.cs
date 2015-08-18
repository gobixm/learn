using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTraveller
{
    public interface IWebClient
    {
        Task<string> DownloadStringTaskAsync(string address);
    }
}
