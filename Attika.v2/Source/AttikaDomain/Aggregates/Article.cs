using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Entities.Contracts;
using Infotecs.Attika.AttikaDomain.Mappings;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaDomain.Aggregates
{
    [TypeConverter(typeof (ArticleConverter))]
    public sealed class Article : IEntity
    {
        private IEnumerable<Comment> _comments;

        private Article()
        {
        }

        public IEnumerable<Comment> Comments

        {
            get { return _comments ?? (_comments = LoadComments()); }
        }

        public string Text
        {
            get { return State.Text; }
        }

        public string Description
        {
            get { return State.Description; }
        }

        public DateTime Created
        {
            get { return State.Created; }
        }

        public ArticleState State { get; private set; }

        public string Title
        {
            get { return State.Title; }
        }

        public Guid Id
        {
            get { return State.Id; }
        }

        private IEnumerable<Comment> LoadComments()
        {
            return from c in State.Comments select Comment.Create(c);
        }

        public void AddComment(Comment comment)
        {
            State.Comments.Add(new CommentState
                {
                    ArticleId = Id,
                    ArticleState = State,
                    Created = comment.Created,
                    Id = comment.Id,
                    Text = comment.Text
                });
            _comments = null;
        }

        public void DeleteComment(Guid idGuid)
        {
            IEnumerable<CommentState> comments = from c in State.Comments where c.Id == idGuid select c;
            foreach (CommentState comment in comments)
            {
                State.Comments.Remove(comment);
            }
            _comments = null;
        }

        public static Article Create(ArticleState articleState)
        {
            return new Article
                {
                    State = articleState
                };
        }
    }
}