using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UNIXstore.Api.Controllers;
using UNIXstore.Api.Dtos;
using UNIXstore.Api.Entities;
using UNIXstore.Api.Repositories;
using Xunit;

namespace UNIXstore.UnitTests
{
    public class UnitTest1
    {
        private readonly Mock<ICommandsRepository> repositoryStub = new();

        private readonly Mock<ILogger<CommandsController>> loggerStub = new();

        [Fact]
        public async Task GetCommandAsync_WithUnexistingCommand_ReturnsNotFOundAsync()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetCommandAsync(It.IsAny<Guid>())).ReturnsAsync((Command)null);

            var controller = new CommandsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetCommandAsync(Guid.NewGuid());
        

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }


        [Fact]
        public async Task GetCommandAsync_WithExistingCommand_ReturnsExpectedCommand()
        {
            // Arrange
            var expectedCommand = CreateRandomCommand();

            repositoryStub.Setup(repo => repo.GetCommandAsync(It.IsAny<Guid>())).ReturnsAsync(expectedCommand);

            var controller = new CommandsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetCommandAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedCommand,
                options => options.ComparingByMembers<Command>());
        }

         [Fact]
        public async Task GetCommandsAsync_WithExistingCommand_ReturnsAllCommands()
        {
            // Arrange
            var expectedCommands = new[] {CreateRandomCommand(),CreateRandomCommand(),CreateRandomCommand()};

            repositoryStub.Setup(repo => repo.GetCommandsAsync())
            .ReturnsAsync(expectedCommands);

            var controller = new CommandsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var actualCommands = await controller.GetCommandsAsync();

            // Assert
            actualCommands.Should().BeEquivalentTo(
                expectedCommands,
                option => option.ComparingByMembers<Command>());

        }

          [Fact]
        public async Task CreateCommandAsync_WithCommandtoCreate_ReturnsCreatedCommand()
        {
            // Arrange
            var commandToCreate = new CreateCommandDto(){
                command = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var controller = new CommandsController(repositoryStub.Object, loggerStub.Object);

            // Act

            var result = await controller.CreateCommandAsync(commandToCreate);
            

            // Assert
            var createdCommand = (result.Result as CreatedAtActionResult).Value as CommandDto;
            commandToCreate.Should().BeEquivalentTo(
                createdCommand,
                options => options.ComparingByMembers<CommandDto>().ExcludingMissingMembers()
            );
            createdCommand.Id.Should().NotBeEmpty();
        }


          [Fact]
        public async Task UpdateCommandAsync_WithExistingCommand_ReturnsNoContent()
        {
            // Arrange

            var existingCommand = CreateRandomCommand();

            repositoryStub.Setup(repo => repo.GetCommandAsync(It.IsAny<Guid>())).ReturnsAsync(existingCommand);

            var commandId = existingCommand.Id;
            var CommandToUpdate = new UpdateCommandDto() {
                command = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var controller = new CommandsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.UpdateCommandAsync(commandId, CommandToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();

        }

          [Fact]
        public async Task DeleteCommandAsync_WithExistingCommand_ReturnsNoContent()
        {
            // Arrange

            var existingCommand = CreateRandomCommand();

            repositoryStub.Setup(repo => repo.GetCommandAsync(It.IsAny<Guid>())).ReturnsAsync(existingCommand);

            var controller = new CommandsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.DeleteCommand(existingCommand.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

        }


        private Command CreateRandomCommand()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                command = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
