using System;
using AttikaContracts.DataTransferObjects;
using GalaSoft.MvvmLight.Messaging;

namespace Infotecs.Attika.AttikaGui.Messages.Gui
{
    public sealed class CommentDeletedMessage : MessageBase
    {
        public CommentDto Comment;
    }
}
