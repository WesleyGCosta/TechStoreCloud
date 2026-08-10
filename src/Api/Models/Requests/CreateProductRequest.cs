using System.ComponentModel.DataAnnotations;

namespace Api.Models.Requests
{
    public record CreateProductRequest(

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3,ErrorMessage = "O nome deve ter entre {2} e {1} caracteres.")]
    string Name,

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo {1} caracteres.")]
    string Description,

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [StringLength(50, ErrorMessage = "A categoria deve ter no máximo {1} caracteres.")]
    string Category,

     [Range(0.1 ,int.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
     decimal Price
 );
}
