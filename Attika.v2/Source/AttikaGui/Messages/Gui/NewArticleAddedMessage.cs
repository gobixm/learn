using System;
using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class NewArticleAddedMessage : MessageBase
    {
        public ArticleDto Article { get; set; }
    }
}
