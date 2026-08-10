using System.ComponentModel.DataAnnotations;

namespace Api.Models.Requests
{
    public record LoginRequest
    (
        [Required(ErrorMessage = "Login é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        string Username,
        [Required(ErrorMessage = "Senha é obrigatória")]
        string Password
    );
}
