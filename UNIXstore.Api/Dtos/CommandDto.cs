using System;

namespace UNIXstore.Api.Dtos
{
    public record CommandDto
    {
        public Guid Id {get; init;}

        public string command {get; init;}

        public string Description {get; init;}

        public DateTimeOffset CreatedDate{get; init;}
    }
}