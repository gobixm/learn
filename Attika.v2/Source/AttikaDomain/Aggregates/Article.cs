﻿using System;
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
    public class Article : IEntity
    {
        private IEnumerable<Comment> _comments;

        public IEnumerable<Comment> Comments

        {
            get { return _comments ?? (_comments = LoadComments()); }
        }

        private Article()
        {
        }

        public string Text
        {
            get { return State.Text; }
            set { State.Text = value; }
        }

        public string Description
        {
            get { return State.Description; }
            set { State.Description = value; }
        }

        public DateTime Created
        {
            get { return State.Created; }
            set { State.Created = value; }
        }

        public ArticleState State { get; private set; }

        public string Title
        {
            get { return State.Title; }
            private set { State.Title = value; }
        }

        public Guid Id
        {
            get { return State.Id; }
            private set { State.Id = value; }
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
        }

        public void DeleteComment(Guid idGuid)
        {
            var comments = from c in State.Comments where c.Id == idGuid select c;
            foreach (var comment in comments)
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