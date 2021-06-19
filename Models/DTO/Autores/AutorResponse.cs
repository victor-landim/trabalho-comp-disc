using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrabalhoComp.Models.DTO.Livros;

namespace TrabalhoComp.Models.DTO.Autores
{
    public class AutorResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<LivroResponse> Livros { get; set; }

        public AutorResponse()
        {
            Livros = new List<LivroResponse>();
        }

        public static AutorResponse CriarCom(Autor autor)
        {
            var response = new AutorResponse();
            response.Id = autor.Id;
            response.Nome = autor.Nome;
            response.Livros = autor.Livros.Select(LivroResponse.CriarCom);
            return response;
        }
    }
}