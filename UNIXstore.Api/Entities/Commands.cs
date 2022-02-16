using System;

namespace UNIXstore.Api.Entities
{
    public record Command
    {
        public Guid Id {get; init;}

        public string command {get; init;}

        public string Description {get; init;}

        public DateTimeOffset CreatedDate{get; init;}
    }
}