﻿using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.GuiMessages
{
    public sealed class ViewArticleMessage : MessageBase
    {
        public string ArticleId { get; set; }
    }
}