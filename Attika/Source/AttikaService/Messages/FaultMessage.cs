namespace Infotecs.Attika.AttikaService.Messages
{
    public class FaultMessage : BaseMessage
    {
        public FaultMessage(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }

        public string Message { get; set; }
        public string Detail { get; set; }
    }
}