using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoComp.Controllers
{

    [Route("api/livro")]
    public class LivroController : ControllerBase
    {
        private readonly LivrariaContext _context;

        public LivroController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                return Ok(_context.Livros.ToList());
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
                var livro = _context.Livros.Find(id);

                if (livro == null)
                {
                    throw new InvalidOperationException("Livro não encontrado");
                }

                return Ok(livro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Livro livro)
        {
            try
            {
                _context.Livros.Add(livro);
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
                var livro = _context.Livros.Find(id);

                if (livro == null)
                    throw new InvalidOperationException("Livro não encontrado");

                if (livro.EstaAlugado())
                    throw new InvalidOperationException("Não é possivel realizar a exclusão, o livro esta alugado");

                _context.Livros.Remove(livro);
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