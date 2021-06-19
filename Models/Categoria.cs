using System.Collections.Generic;

namespace TrabalhoComp.Models
{
    public class Categoria
    {

        public Categoria()
        {
            Livros = new List<Livro>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }

    }
}