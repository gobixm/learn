using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class ViewArticleMessage : MessageBase
    {
        public string ArticleId { get; set; }
    }
}