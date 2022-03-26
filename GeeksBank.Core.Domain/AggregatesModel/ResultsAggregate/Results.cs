using FluentValidation;
using GeeksBank.Core.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate
{
    public class Results : IAggregateRoot, IDto
    {
        public Results()
        {

        }
        [Key]
        public string Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
    }

    public class ResultsValidator : AbstractValidator<Results>
    {
        public ResultsValidator()
        {
            RuleFor(x => x.Number1).NotEmpty();
            RuleFor(x => x.Number2).NotEmpty();
            RuleFor(x => x.Result).NotEmpty();
        }
    }
}