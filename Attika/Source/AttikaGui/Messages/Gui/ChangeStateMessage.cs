using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class ChangeStateMessage : MessageBase
    {
        public string State { get; set; }
    }
}