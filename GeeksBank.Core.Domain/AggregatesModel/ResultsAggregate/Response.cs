using FluentValidation;
using GeeksBank.Core.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate
{
  public class Response : IAggregateRoot, IDto
  {
    public Response()
    {

    }
    public int Result { get; set; }
    public bool IsInFibonacci { get; set; }
  }
  
}
