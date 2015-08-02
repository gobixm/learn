using System;
using Infotecs.Attika.AttikaDomain.Aggregates;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.DataTransferObjects;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;

namespace Infotecs.Attika.AttikaDomain.Factories
{
    public class CommentFactory : ICommentFactory
    {
        private readonly IValidator<Comment> _validator;
        private IMappingService _mappingService;

        public CommentFactory(IValidator<Comment> validator, IMappingService mappingService)
        {
            _validator = validator;
            _mappingService = mappingService;
        }

        public Comment CreateComment(CommentState commentState)
        {
            var comment = Comment.Create(commentState);
            string[] errors;
            if (!_validator.Validate(comment, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            return comment;
        }

        public Comment NewComment(string text)
        {
            var comment = Comment.Create(new CommentState
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Text = text
            });
            string[] errors;
            if (!_validator.Validate(comment, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            return comment;
        }

        public Comment CreateComment(CommentDto commentDto)
        {
            var state = _mappingService.Map<CommentState>(commentDto);
            var comment = Comment.Create(state);
            string[] errors;
            if (!_validator.Validate(comment, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            return comment;
        }
    }
}