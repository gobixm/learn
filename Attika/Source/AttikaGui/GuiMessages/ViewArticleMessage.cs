using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.GuiMessages
{
    public class ViewArticleMessage : MessageBase
    {
        public string ArticleId { get; set; }
    }
}