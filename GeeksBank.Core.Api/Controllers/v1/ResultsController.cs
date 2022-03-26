
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Net;
using MediatR;
using GeeksBank.Report.Api.Constants;
using GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate;
using GeeksBank.Core.Domain.Exception;
using GeeksBank.Core.Infrastructure.Extensions;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace GeeksBank.Core.Api.Controllers.v1
{
    [ApiController]
    [Route(ServiceConstants.ContextPath + "results/")]
    public class ResultsController : Controller
    {
        
        private readonly IMediator _mediator;
        private readonly ILogger _logger = Log.ForContext<ResultsController>();


        public ResultsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Route("add-history-user")]
        //[HttpPost()]
        //[ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> AddHistoryUser(RegisterUserCommand command)
        //{
        //    _logger.Information("UserController AddHistoryUser: {@LoginUserCommand}", command);
        //    var result = await _mediator.Send(command).ConfigureAwait(false);
        //    return Ok(result);
        //}
    }
}