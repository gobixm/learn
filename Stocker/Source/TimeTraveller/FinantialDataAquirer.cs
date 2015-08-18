using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace TimeTraveller
{
    public class FinantialDataAquirer : IFinancialDataAquirer
    {
        private IWebClient _client;
        public FinantialDataAquirer(IWebClient client)
        {
            _client = client;
        }
        public async Task<ICollection<Index>> GetHistoricalData(string symbol, DateTime startDate, DateTime endDate)
        {
            var json = await _client.DownloadStringTaskAsync(
                string.Format("http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.historicaldata where symbol = \"{0}\" "
                + "and startDate = \"{1}\" and endDate = \"{2}\"&format=json&diagnostics=true&env=store://datatables.org/alltableswithkeys",
                symbol,
                startDate.ToString("yyyy-MM-dd"),
                endDate.ToString("yyyy-MM-dd")));

            return await ParseJsonData(json);
        }
        public async Task<ICollection<Index>> ParseJsonData(string json)
        {
            return await Task.Factory.StartNew<ICollection<Index>>(()=>
                {
                    var collection = new List<Index>();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    foreach(dynamic price in data.query.results.quote)
                    {
                        collection.Add(
                            new Index
                            {
                                Open = price.Open,
                                Close = price.Close,
                                Symbol = price.Symbol,
                                Date = price.Date,
                                High = price.High,
                                Low = price.Low,
                                Volume = price.Volume,
                                AdjClose = price.Adj_Close
                            });                       
                    }
                    return collection;
                });
        }
    }
}
