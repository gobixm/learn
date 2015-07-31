using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infotecs.Attika.AttikaService.Messages.Handlers
{
    public sealed class ProcessorConfiguration : IProcessorConfiguration
    {
        private readonly Dictionary<string, BaseHandler> _handlers = new Dictionary<string, BaseHandler>();
        private readonly Dictionary<string, InOutMessageType> _types = new Dictionary<string, InOutMessageType>();

        public ProcessorConfiguration()
        {
        }

        public ProcessorConfiguration(IEnumerable<BaseHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                RegisterHandler(handler);
            }
        }

        public void RegisterHandler(BaseHandler handler)
        {
            string typeName = handler.GetType().Name;
            if (typeName.EndsWith("Handler"))
            {
                string handlerPrefix = typeName.Replace("Handler", "");
                IEnumerable<MethodInfo> methods = from m in handler.GetType().GetMethods()
                                                  where m.Name == "Handle"
                                                  select m;
                foreach (MethodInfo methodInfo in methods)
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    if (parameters.Length == 1)
                    {
                        string header = handlerPrefix + "." + parameters[0].ParameterType.Name;
                        _handlers.Add(header, handler);
                        _types.Add(header,
                                   new InOutMessageType {In = parameters[0].ParameterType, Out = methodInfo.ReturnType});
                    }
                }
            }
        }

        public BaseHandler GetMessageHandler(string messageHeader)
        {
            return _handlers.ContainsKey(messageHeader) ? _handlers[messageHeader] : null;
        }

        public InOutMessageType GetMessageType(string messageHeader)
        {
            return _types.ContainsKey(messageHeader) ? _types[messageHeader] : null;
        }
    }
}