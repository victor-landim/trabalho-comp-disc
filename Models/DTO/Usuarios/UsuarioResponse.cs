using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrabalhoComp.Models.DTO.Livros;
using TrabalhoComp.Models.DTO.UsuariosLivros;

namespace TrabalhoComp.Models.DTO.Usuarios
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public IEnumerable<LivroResponse> LivrosAlugados { get; set; }

        public UsuarioResponse()
        {
            LivrosAlugados = new List<LivroResponse>();
        }

        public static UsuarioResponse CriarCom(Usuario usuario)
        {
            var response = new UsuarioResponse();
            response.Id = usuario.Id;
            response.Nome = usuario.Nome;
            response.Cpf = usuario.Cpf;
            response.Endereco = usuario.Endereco;
            response.Telefone = usuario.Telefone;
            response.LivrosAlugados = usuario.LivrosAlugados.Select(la => LivroResponse.CriarCom(la.Livro));
 
            return response;
        }
    }
}