using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoComp.Controllers
{

    [Route("api/usuariolivro")]
    public class UsuarioLivroController : ControllerBase
    {
        private readonly LivrariaContext _context;

        public UsuarioLivroController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                return Ok(_context.UsuariosLivros.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                var usuarioLivro = _context.UsuariosLivros.Find(id);
                return Ok(usuarioLivro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("alugar")]
        public IActionResult Alugar([FromBody] UsuarioLivro usuarioLivro)
        {
            try
            {
                var livro = _context.Livros.Find(usuarioLivro.LivroId);

                if (livro == null)
                {
                    throw new InvalidOperationException("Livro não encontrado");
                }

                if (!livro.PodeAlugar())
                {
                    throw new InvalidOperationException("Quantidade indisponivel");
                }
                _context.UsuariosLivros.Add(usuarioLivro);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("devolver")]
        public IActionResult Devolver([FromBody] UsuarioLivro usuarioLivro)
        {
            try
            {
                var usuario = _context.Usuarios.Find(usuarioLivro.UsuarioId);
                var livro = _context.Livros.Find(usuarioLivro.LivroId);

                if(usuario == null)
                    throw new InvalidOperationException("Usuario não encontrado");

                if(livro == null)
                    throw new InvalidOperationException("Livro não encontrado");

                if(!usuario.Alugou(livro))
                    throw new InvalidOperationException("Usuario não alugou o livro");

                var aluguel = _context.UsuariosLivros.FirstOrDefault(ul => ul.LivroId == usuarioLivro.LivroId && ul.UsuarioId == usuarioLivro.UsuarioId);

                _context.Remove(aluguel);
                _context.SaveChanges();
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {

                if (_context.Livros.Find(id) == null)
                {
                    throw new InvalidOperationException("Relação não encontrada");
                }

                var usuarioLivro = _context.UsuariosLivros.Find(id);
                _context.UsuariosLivros.Remove(usuarioLivro);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}