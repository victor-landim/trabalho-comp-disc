using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Utils;

namespace TrabalhoComp
{
    public class LivrariaContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<UsuarioLivro> UsuariosLivros { get; set; }

        public LivrariaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }

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
    }

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
    }

    public class Autor
    {

        public Autor()
        {
            Livros = new List<Livro>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
    }

    public class Editora
    {

        public Editora()
        {
            Livros = new List<Livro>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Livro> Livros { get; set; }
    }

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