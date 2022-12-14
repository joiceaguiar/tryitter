using System.ComponentModel.DataAnnotations;

namespace TryitterAPI.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo é obrigatório")]
        [MinLength(5, ErrorMessage = "O titulo deve ter no mínimo cinco caracteres")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "O texto é obrigatório")]
        [MinLength(5, ErrorMessage = "O texto deve ter no mínimo cinco caracteres")]
        [MaxLength(300, ErrorMessage = "O texto pode ter no máximo 300 caracteres")]
        public string Text { get; set; } = "";

        [Required(ErrorMessage = "O ID da pessoa estudante é obrigatório")]
        public int StudentId { get; set; }

        public List<Image>? Images { get; set; } = new List<Image>();
    }
}