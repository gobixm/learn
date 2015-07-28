using System;
using Infotecs.Attika.AttikaGui.DTO;

namespace Infotecs.Attika.AttikaGui.DataService
{
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

        public FaultDto Fault { get; private set; }

        public override string ToString()
        {
            return string.Format("Ошибка:\"{0}\"\nПодробности:\"{1}\"", Fault.Message, Fault.Detail);
        }
    }
}