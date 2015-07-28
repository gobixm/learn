using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.GuiMessages
{
    public sealed class ChangeStateMessage : MessageBase
    {
        public string State { get; set; }
    }
}