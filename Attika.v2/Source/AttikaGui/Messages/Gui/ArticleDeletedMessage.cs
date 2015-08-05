using System;
using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class ArticleDeletedMessage : MessageBase
    {
        public Guid ArticleId { get; set; }
    }
}
