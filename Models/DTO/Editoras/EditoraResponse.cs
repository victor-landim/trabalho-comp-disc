using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrabalhoComp.Models.DTO.Livros;

namespace TrabalhoComp.Models.DTO.Editoras
{
    public class EditoraResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<LivroResponse> Livros { get; set; }

        public EditoraResponse()
        {
            Livros = new List<LivroResponse>();
        }

        public static EditoraResponse CriarCom(Editora editora)
        {
            var response = new EditoraResponse();
            response.Id = editora.Id;
            response.Nome = editora.Nome;
            response.Livros = editora.Livros.Select(LivroResponse.CriarCom);
            return response;
        }
    }
}