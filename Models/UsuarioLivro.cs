using System;

namespace TrabalhoComp.Models
{
    public class UsuarioLivro
    {

        public int Id { get; set; }
        public int LivroId { get; set; }
        public int UsuarioId { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataEmprestimo { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}