using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;

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
}