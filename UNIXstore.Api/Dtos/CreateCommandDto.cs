using System.ComponentModel.DataAnnotations;

namespace UNIXstore.Api.Dtos
{
    public record CreateCommandDto
    {
        [Required]
        public string command {get; init;}

        [Required]
        public string Description {get; init;}
    }
}