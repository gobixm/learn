using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DTO;

namespace Infotecs.Attika.AttikaGui.GuiMessages
{
    public sealed class AddArticleMessage : MessageBase
    {
        public ArticleDto Article { get; set; }
    }
}