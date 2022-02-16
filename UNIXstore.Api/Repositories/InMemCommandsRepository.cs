using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIXstore.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using UNIXstore.Api.Entities;

namespace UNIXstore.Api.Repositories
{
    

    public class InMemCommandsRepository : ICommandsRepository
    {
        private readonly List<Command> commands = new()
        {
            new Command { Id = Guid.NewGuid(), command = "cd", Description = "change directory", CreatedDate = DateTimeOffset.UtcNow },
            new Command { Id = Guid.NewGuid(), command = "ls", Description = "lists your files", CreatedDate = DateTimeOffset.UtcNow },
            new Command { Id = Guid.NewGuid(), command = "mkdir", Description = "make a new directory", CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Command>> GetCommandsAsync()
        {
            return await Task.FromResult(commands);

        }

        public async Task<Command> GetCommandAsync(Guid id)
        {
            var command = commands.Where(command => command.Id == id).SingleOrDefault();
            return await Task.FromResult(command);
        }

        public async Task CreateCommandAsync(Command command)
        {
            commands.Add(command);
            await Task.CompletedTask;
        }

        public async Task UpdateCommandAsync(Command command)
        {
            var index = commands.FindIndex(existingCommand => existingCommand.Id == command.Id);
            commands[index] = command;
            await Task.CompletedTask;
        }

        public async Task DeleteCommandAsync(Guid id)
        {
            var index = commands.FindIndex(existingCommand => existingCommand.Id == id);
            commands.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}

