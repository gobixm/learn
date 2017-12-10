using System;
using System.Text;
using System.Threading.Tasks;
using STAN.Client;

namespace Publisher
{
    public class Emitter : IDisposable
    {
        private readonly IStanConnection connection;

        public Emitter(string id)
        {
            var stanConnectionFactory = new StanConnectionFactory();
            var options = StanOptions.GetDefaultOptions();
            options.MaxPubAcksInFlight = 10000;
            connection = stanConnectionFactory.CreateConnection("test-cluster", id, options);
        }

        public void Emit(string content)
        {
            connection.Publish("topic", Encoding.UTF8.GetBytes(content));
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}
