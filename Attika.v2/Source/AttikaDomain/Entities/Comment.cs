using System;
using Infotecs.Attika.AttikaDomain.Entities.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaDomain.Entities
{
    public sealed class Comment : IEntity
    {
        private Comment()
        {
        }

        public string Text { get; private set; }

        public DateTime Created { get; private set; }
        public Guid Id { get; private set; }

        public static Comment Create(CommentState c)
        {
            return new Comment
                {
                    Id = c.Id,
                    Text = c.Text,
                    Created = c.Created
                };
        }
    }
}