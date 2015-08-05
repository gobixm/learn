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
    [TypeConverter(typeof(ArticleConverter))]
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

        public DateTime Created
        {
            get { return State.Created; }
        }

        public string Description
        {
            get { return State.Description; }
        }

        public Guid Id
        {
            get { return State.Id; }
        }

        public ArticleState State { get; private set; }

        public string Text
        {
            get { return State.Text; }
        }

        public string Title
        {
            get { return State.Title; }
        }

        public static Article Create(ArticleState articleState)
        {
            return new Article
            {
                State = articleState
            };
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
            CommentState[] comments = (from c in State.Comments where c.Id == idGuid select c).ToArray();

            foreach (CommentState comment in comments)
            {
                State.Comments.Remove(comment);
            }
            _comments = null;
        }

        private IEnumerable<Comment> LoadComments()
        {
            return from c in State.Comments select Comment.Create(c);
        }
    }
}
