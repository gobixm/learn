using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaDomain.Factories.Contracts
{
    public interface ICommentFactory
    {
        Comment CreateComment(CommentState comment);
        Comment NewComment(string text);
        Comment CreateComment(CommentDto commentDto);
    }
}