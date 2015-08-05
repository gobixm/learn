using System;
using AttikaContracts.DataTransferObjects;
using Infotecs.Attika.AttikaDomain.Entities;
using Infotecs.Attika.AttikaDomain.Factories.Contracts;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using Infotecs.Attika.AttikaInfrastructure.Services.Contracts;

namespace Infotecs.Attika.AttikaDomain.Factories
{
    public sealed class CommentFactory : ICommentFactory
    {
        private readonly IMappingService _mappingService;
        private readonly IValidator<Comment> _validator;

        public CommentFactory(IValidator<Comment> validator, IMappingService mappingService)
        {
            _validator = validator;
            _mappingService = mappingService;
        }

        public Comment CreateComment(CommentDto commentDto)
        {
            var state = _mappingService.Map<CommentState>(commentDto);
            Comment comment = Comment.Create(state);
            string[] errors;
            if (!_validator.Validate(comment, out errors))
            {
                throw new ArgumentException(string.Join("\n", errors));
            }
            return comment;
        }
    }
}
