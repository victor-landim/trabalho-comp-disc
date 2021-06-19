using System.Collections.Generic;
using System.Linq;
using TrabalhoComp.Utils;

namespace TrabalhoComp.Models
{
    public class Usuario
    {

        public Usuario()
        {
            LivrosAlugados = new List<UsuarioLivro>();
        }
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

        public virtual ICollection<UsuarioLivro> LivrosAlugados { get; set; }

        public bool CpfJaCadastrado(IEnumerable<Usuario> usuarios)
        {
            return usuarios.Any(u => u.Cpf.Equals(this.Cpf));
        }

        public bool CpfValido(){
            return this.Cpf.CpfValido();
        }

        public bool Alugou(Livro livro){

            return LivrosAlugados.Any(la => la.LivroId == livro.Id);
        }

        public bool PossuiLivroAlugado()
        {
            return LivrosAlugados.Any();
        }
    }
}