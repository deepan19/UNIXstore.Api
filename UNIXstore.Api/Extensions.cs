using UNIXstore.Api.Dtos;
using UNIXstore.Api.Entities;

namespace UNIXstore.Api
{
    public static class Extensions{
        public static CommandDto AsDto(this Command command)
        {
            return new CommandDto
            {
                Id = command.Id,
                command = command.command,
                Description = command.Description,
                CreatedDate = command.CreatedDate
            };
        }
    }
}