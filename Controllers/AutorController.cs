using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoComp.Controllers
{

    [Route("api/autor")]
    public class AutorController : ControllerBase
    {
        public readonly LivrariaContext _context;
        public AutorController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                return Ok(_context.Autores.ToList());
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
                var autor = _context.Autores.Find(id);

                if (autor == null)
                {
                    throw new InvalidOperationException("Autor não encontrado");
                }

                return Ok(autor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Autor autor)
        {
            try
            {
                _context.Autores.Add(autor);
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
                var autor = _context.Autores.Find(id);
                var autorLivro = _context.Livros.Find(id);

                if (autor == null)
                {
                    throw new InvalidOperationException("Autor não encontrado");
                }

                if (autorLivro == null)
                {
                    throw new InvalidOperationException("Não é possivel realizar exclusão, autor pertence a um livro cadastrado");
                }

                _context.Autores.Remove(autor);
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