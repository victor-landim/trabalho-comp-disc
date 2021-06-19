using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;
using TrabalhoComp.Models.DTO.UsuariosLivros;

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
                var usuariosLivros = _context.UsuariosLivros.ToList();

                return Ok(usuariosLivros.Select(UsuarioLivroResponse.CriarCom));
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
                return Ok(UsuarioLivroResponse.CriarCom(usuarioLivro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("alugar")]
        public IActionResult Alugar([FromBody] UsuarioLivroRequest request)
        {
            try
            {
                var livro = _context.Livros.Find(request.LivroId);

                if (livro == null)
                {
                    throw new InvalidOperationException("Livro não encontrado");
                }

                if (!livro.PodeAlugar())
                {
                    throw new InvalidOperationException("Quantidade indisponivel");
                }

                var usuarioLivro = new UsuarioLivro();
                usuarioLivro.LivroId = request.LivroId;
                usuarioLivro.UsuarioId = request.UsuarioId;
                usuarioLivro.DataEmprestimo = DateTime.Now;
                usuarioLivro.DataDevolucao = DateTime.Now.AddDays(7);

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
        public IActionResult Devolver([FromBody] UsuarioLivroRequest request)
        {
            try
            {
                var usuario = _context.Usuarios.Find(request.UsuarioId);
                var livro = _context.Livros.Find(request.LivroId);

                if (usuario == null)
                    throw new InvalidOperationException("Usuario não encontrado");

                if (livro == null)
                    throw new InvalidOperationException("Livro não encontrado");

                if (!usuario.Alugou(livro))
                    throw new InvalidOperationException("Usuario não alugou o livro");

                var aluguel = _context.UsuariosLivros.FirstOrDefault(ul => ul.LivroId == request.LivroId && ul.UsuarioId == request.UsuarioId);

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
                var usuarioLivro = _context.UsuariosLivros.Find(id);

                if (usuarioLivro == null)
                {
                    throw new InvalidOperationException("Relação não encontrada");
                }

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