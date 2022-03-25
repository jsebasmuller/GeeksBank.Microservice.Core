using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Moq;
using FluentAssertions;
using MediatR;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.CoreAggregate;
using GeeksBank.Microservice.Core.Api.Application.Commands.Core;
using GeeksBank.Microservice.Core.Api.Application.Model;
using Mapster;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.CoreAggregate;

namespace GeeksBank.Microservice.Core.Api.Test.Application.Commands
{
    public class CoreTokenCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IDeviceRepository> _repository;
        private readonly CoreTokenCommand _command;

        public CoreTokenCommandHandlerTest()
        {
            _repository = new Mock<IDeviceRepository>();
            _mediator = new Mock<IMediator>();
            _command = new CoreTokenCommand()
            {
                Id = "Medellín",
                Token = "MED",
                Metadata = "Antioquia",
                Device = new CoreTokenCommand.DeviceInfo()
                {
                    Name = "",
                    OperativeSystem = "",
                    Mac = ""
                },
                IpAddress = "Antioquia",
                AppVersion = "Antioquia",
                Location = "Antioquia"
            };
        }

        [Fact]
        public async void Handler_when_all_ready_Test()
        {
            var expectedResult = new CoreToken()
            {
                CoreId = "Medellín",
                Token = "MED",
                Metadata = "Antioquia",
                Device = new DeviceInfo()
                {
                    Name = "",
                    OperativeSystem = "",
                    Mac = ""
                },
                IpAddress = "Antioquia",
                AppVersion = "Antioquia",
                Location = "Antioquia"
            };


            var handler = new CoreTokenCommandHandler(_repository.Object);
            var response = FakerData();
            _repository.Setup(x => x.CreateCoreToken(It.IsAny<CoreToken>())).ReturnsAsync(response);
            var actualResult = await handler.Handle(_command, new CancellationToken());
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void Handler_when_throw_an_exception_Test()
        {
            var expectedResult = new Exception();

            _repository.Setup(x => x.CreateCoreToken(It.IsAny<CoreToken>())).ThrowsAsync(expectedResult);
            var handler = new CoreTokenCommandHandler(_repository.Object);

            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(_command, new CancellationToken()));
        }
       

        private CoreToken FakerData()
        {
            var response = new CoreToken()
            {
                CoreId = "Medellín",
                Token = "MED",
                Metadata = "Antioquia",
                Device = new DeviceInfo()
                {
                    Name = "",
                    OperativeSystem = "",
                    Mac = ""
                },
                IpAddress = "Antioquia",
                AppVersion = "Antioquia",
                Location = "Antioquia"
            };
            return response;
        }
    }
}