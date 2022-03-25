using GeeksBank.Microservice.Core.Api.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using FluentValidation.TestHelper;
using GeeksBank.Microservice.Core.Api.Application.Commands.Core;

namespace GeeksBank.Microservice.Core.Api.Test.Application.Commands
{

    public class CoreTokenCommandTest
    {
        private CoreTokenCommandValidator validator;

        public CoreTokenCommandTest()
        {
            validator = new CoreTokenCommandValidator();
        }

        [Fact]
        public void Should_have_error_when_is_null()
        {
            var command = new CoreTokenCommand();
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.AppVersion);
            result.ShouldHaveValidationErrorFor(command => command.Location);
            result.ShouldHaveValidationErrorFor(command => command.IpAddress);
        }


        [Fact]
        public void Should_not_have_error_when_Fields_is_specified()
        {
            var command = new CoreTokenCommand(){
                Id = "Medellín",
                Token = "MED",
                Metadata = "Antioquia",
                Device = new CoreTokenCommand.DeviceInfo(){
                    Name = "",
                    OperativeSystem = "",
                    Mac = ""
                },
                IpAddress = "Antioquia",
                AppVersion = "Antioquia",
                Location = "Antioquia"
            };
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(command => command.AppVersion);
            result.ShouldNotHaveValidationErrorFor(command => command.Location);
            result.ShouldNotHaveValidationErrorFor(command => command.IpAddress);
        }

    }
}