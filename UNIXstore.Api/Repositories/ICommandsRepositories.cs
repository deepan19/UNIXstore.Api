using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNIXstore.Api.Entities;

namespace UNIXstore.Api.Repositories
{
    public interface ICommandsRepository
    {
        Task<Command> GetCommandAsync(Guid id);
        Task<IEnumerable<Command>> GetCommandsAsync();

        Task CreateCommandAsync(Command command);

        Task UpdateCommandAsync(Command command);

        Task DeleteCommandAsync(Guid id);

    }
}