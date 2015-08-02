using System.Collections.Generic;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Services.RequestProcessors;

namespace Infotecs.Attika.AttikaService.Messages.Processors
{
    public class MessageProcessorConfiguration : IMessageProcessorConfiguration
    {
        private readonly Dictionary<string, BaseHandler> _handlers = new Dictionary<string, BaseHandler>();
        private readonly Dictionary<string, InOutMessageType> _types = new Dictionary<string, InOutMessageType>();

        public MessageProcessorConfiguration()
        {
        }

        public MessageProcessorConfiguration(IEnumerable<BaseHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                RegisterHandler(handler);
            }
        }

        public void RegisterHandler(BaseHandler handler)
        {
            var typeName = handler.GetType().Name;
            if (typeName.EndsWith("Handler"))
            {
                var handlerPrefix = typeName.Replace("Handler", "");
                var methods = from m in handler.GetType().GetMethods()
                    where m.Name == "Handle"
                    select m;
                foreach (var methodInfo in methods)
                {
                    var parameters = methodInfo.GetParameters();
                    if (parameters.Length == 1)
                    {
                        var header = handlerPrefix + "." + parameters[0].ParameterType.Name;
                        _handlers.Add(header, handler);
                        _types.Add(header,
                            new InOutMessageType {In = parameters[0].ParameterType, Out = methodInfo.ReturnType});
                    }
                }
            }
        }

        public BaseHandler GetMessageHandler(string messageHeader)
        {
            return _handlers.ContainsKey(messageHeader) ? (BaseHandler) _handlers[messageHeader].Clone() : null;
        }

        public InOutMessageType GetMessageType(string messageHeader)
        {
            return _types.ContainsKey(messageHeader) ? _types[messageHeader] : null;
        }
    }
}