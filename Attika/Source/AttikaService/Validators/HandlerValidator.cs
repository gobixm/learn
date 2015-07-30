using System.Reflection;
using Infotecs.Attika.AttikaService.Messages;

namespace Infotecs.Attika.AttikaService.Validators
{
    public static class HandlerValidator
    {
        public static bool IsMethodValid(MethodInfo methodInfo)
        {
            if ((methodInfo.ReturnType != typeof (BaseMessage)) &&
                (!methodInfo.ReturnType.IsSubclassOf(typeof (BaseMessage))))
            {
                return false;
            }
            ParameterInfo[] parameters = methodInfo.GetParameters();


            if (parameters.Length != 1)
            {
                return false;
            }

            ParameterInfo param = parameters[0];

            if (param.ParameterType == typeof (BaseMessage))
            {
                return true;
            }
            if (param.ParameterType.IsSubclassOf(typeof (BaseMessage)))
            {
                return true;
            }
            return false;
        }
    }
}