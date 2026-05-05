using System.ComponentModel.DataAnnotations;

namespace OxenteBurguer.DTOs
{
    public class PedidoCreateDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string NomeCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "O pedido não pode estar vazio.")]
        public List<ItemPedidoDto> Itens { get; set; } = new List<ItemPedidoDto>();
    }
}