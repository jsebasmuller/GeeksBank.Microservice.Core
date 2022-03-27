using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate;

namespace GeeksBank.Core.Api.Application.Commands
{
    public class AddResultsCommandHandler : IRequestHandler<AddResultsCommand, Response>
    {
        private readonly IResultsRepository _repository;

        public AddResultsCommandHandler(IResultsRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Response> Handle(AddResultsCommand request, CancellationToken cancellationToken)
        {
            var userModel = request.Adapt<Results>();
            return await _repository.AddResult(userModel);
        } 
    }
}