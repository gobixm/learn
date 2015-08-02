using System;
using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class DeleteArticleMessage : MessageBase
    {
        public Guid ArticleId { get; set; }
    }
}