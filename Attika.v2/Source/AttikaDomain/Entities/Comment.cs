using System;
using Infotecs.Attika.AttikaDomain.Entities.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaDomain.Entities
{
    public sealed class Comment : IEntity
    {
        public string Text { get; set; }
        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        private Comment()
        {
            
        }

        public static Comment Create(CommentState c)
        {
            return new Comment()
            {
                Id = c.Id,
                Text = c.Text,
                Created = c.Created
            };
        }
    }
}