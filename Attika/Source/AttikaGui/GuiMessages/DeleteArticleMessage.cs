using System;
using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.GuiMessages
{
    public class DeleteArticleMessage : MessageBase
    {
        public Guid ArticleId { get; set; }
    }
}