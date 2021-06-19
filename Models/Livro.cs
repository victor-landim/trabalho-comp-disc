using System.Collections.Generic;
using System.Linq;

namespace TrabalhoComp.Models
{
    public class Livro
    {

        public Livro()
        {
            LivrosAlugados = new List<UsuarioLivro>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isnb { get; set; }
        public int AutorId { get; set; }
        public int EditoraId { get; set; }
        public int CategoriaId { get; set; }

        public int Quantidade { get; set; }

        public virtual Autor Autor { get; set; }
        public virtual Editora Editora { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<UsuarioLivro> LivrosAlugados { get; set; }

        public bool PodeAlugar()
        {
            return LivrosAlugados.Count() < Quantidade;
        }

        public bool EstaAlugado()
        {
            return LivrosAlugados.Any();
        }
    }
}