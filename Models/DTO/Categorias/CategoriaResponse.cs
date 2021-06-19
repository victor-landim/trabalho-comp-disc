using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrabalhoComp.Models.DTO.Livros;

namespace TrabalhoComp.Models.DTO.Categorias
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<LivroResponse> Livros { get; set; }

        public CategoriaResponse()
        {
            Livros = new List<LivroResponse>();
        }

        public static CategoriaResponse CriarCom(Categoria categoria)
        {
            var response = new CategoriaResponse();
            response.Id = categoria.Id;
            response.Nome = categoria.Nome;
            response.Livros = categoria.Livros.Select(LivroResponse.CriarCom);
            return response;
        }
    }
}