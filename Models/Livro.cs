using System.ComponentModel.DataAnnotations;
namespace biblioteca.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O autor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O autor deve ter no máximo 100 caracteres.")]
        [Display(Name = "Autor")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Genero { get; set; }
        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; } = true;
    }
}