using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIXstore.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UNIXstore.Api.Dtos;
using UNIXstore.Api.Entities;

namespace UNIXstore.Api.Controllers
{
    [ApiController]
    [Route("commands")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsRepository repository;

        private readonly ILogger<CommandsController> logger;

        public CommandsController(ICommandsRepository repository, ILogger<CommandsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CommandDto>> GetCommandsAsync()
        {
            var commands = (await repository.GetCommandsAsync())
                            .Select( command => command.AsDto());
            
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {commands.Count()} commands");
            return commands;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandDto>> GetCommandAsync(Guid id)
        {
            var command = await repository.GetCommandAsync(id);

            if(command is null){
                return NotFound();
            }
            return command.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CommandDto>> CreateCommandAsync(CreateCommandDto CommandDto)
        {
            Command command = new(){
                Id = Guid.NewGuid(),
                command = CommandDto.command,
                Description = CommandDto.Description,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateCommandAsync(command);

            return CreatedAtAction(nameof(GetCommandAsync), new {id = command.Id}, command.AsDto());
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommandAsync(Guid id, UpdateCommandDto commandDto)
        {
            var existingCommand = await repository.GetCommandAsync(id);

            if(existingCommand is null){
                return NotFound();
            }

            Command updatedCommand = existingCommand with {
                command = commandDto.command,
                Description = commandDto.Description
            };
            
            await repository.UpdateCommandAsync(updatedCommand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(Guid id)
        {
            var existingCommand = await repository.GetCommandAsync(id);
            
            if(existingCommand is null){
                return NotFound();
            }

            await repository.DeleteCommandAsync(id);

            return NoContent(); 
        }

    }
}