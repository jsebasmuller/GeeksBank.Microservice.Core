using System;
using FluentValidation;
using MediatR;
using GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate;
using GeeksBank.Core.Infrastructure.Extensions;

namespace GeeksBank.Core.Api.Application.Commands
{
    public class AddResultsCommand : IRequest<Response>
    {
        public long Number1 { get; set; }
        public long Number2 { get; set; }

        public AddResultsCommand()
        {
            
        }

        public class AddResultsCommandValidator : AbstractValidator<AddResultsCommand>
        {
            public AddResultsCommandValidator()
            {
                RuleFor(x => x.Number1).IsNull();
                RuleFor(x => x.Number1).IsNotNull();
            }
        }
    }
}