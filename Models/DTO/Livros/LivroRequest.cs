namespace TrabalhoComp.Models.DTO.Livros
{
    public class LivroRequest
    {
        public string Titulo { get; set; }
        public string Isnb { get; set; }
        public int AutorId { get; set; }
        public int EditoraId { get; set; }
        public int CategoriaId { get; set; }
        public int Quantidade { get; set; }
    }
}