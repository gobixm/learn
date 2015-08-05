using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Infotecs.Attika.AttikaGui.DataServices
{
    [Serializable]
    public class DataServiceException : ApplicationException
    {
        public DataServiceException(FaultDto fault, Exception innerException)
            : base(fault.Message, innerException)
        {
            Fault = fault;
        }

        public DataServiceException(FaultDto fault)
            : base(fault.Message)
        {
            Fault = fault;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected DataServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Fault = new FaultDto(info.GetString("DataServiceException.FaultMessage"),
                                 info.GetString("DataServiceException.FaultDetail"));
        }

        public FaultDto Fault { get; private set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("DataServiceException.FaultMessage", Fault.Message);
            info.AddValue("DataServiceException.FaultMessage", Fault.Detail);
        }

        public override string ToString()
        {
            return string.Format("Ошибка:\"{0}\"\nПодробности:\"{1}\"", Fault.Message, Fault.Detail);
        }
    }
}