using GeeksBank.Microservice.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate
{
  public class Response : IAggregateRoot
  {
    public int Result { get; set; }
    public bool ExistFibonacci { get; set; }
  }
}
