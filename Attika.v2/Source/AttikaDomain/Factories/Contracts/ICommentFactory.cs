using System;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaDomain.Entities;

namespace Infotecs.Attika.AttikaDomain.Factories.Contracts
{
    public interface ICommentFactory
    {
        Comment CreateComment(CommentDto commentDto);
    }
}
