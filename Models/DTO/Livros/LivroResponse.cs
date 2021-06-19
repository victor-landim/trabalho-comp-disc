namespace TrabalhoComp.Models.DTO.Livros
{
    public class LivroResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isnb { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }

        public static LivroResponse CriarCom(Livro livro)
        {
            var response = new LivroResponse();
            response.Id = livro.Id;
            response.Autor = livro.Autor.Nome;
            response.Categoria = livro.Categoria.Nome;
            response.Editora = livro.Editora.Nome;
            response.Isnb = livro.Isnb;
            response.Titulo = livro.Titulo;
            response.Quantidade = livro.Quantidade;
            
            return response;
        }
    }
}