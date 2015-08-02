using GalaSoft.MvvmLight.Messaging;
using Infotecs.Attika.AttikaGui.DataTransferObjects;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class AddArticleMessage : MessageBase
    {
        public ArticleDto Article { get; set; }
    }
}