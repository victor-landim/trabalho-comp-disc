using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoComp.Controllers
{

    [Route("api/editora")]
    public class EditoraController : ControllerBase
    {
        public readonly LivrariaContext _context;
        public EditoraController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                return Ok(_context.Editoras.ToList());
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
                var editora = _context.Editoras.Find(id);
                if (editora == null)
                {
                    throw new InvalidOperationException("Livraria não encontrada");
                }

                return Ok(editora);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Editora editora)
        {
            try
            {
                _context.Editoras.Add(editora);
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
                var editora = _context.Editoras.Find(id);

                if (editora == null)
                {
                    throw new InvalidOperationException("Livraria não encontrada");
                }
                if (editora != null)
                {
                    throw new InvalidOperationException("Não é possivel a exclusão, a editora pertence a um livro cadastrado");
                }

                _context.Editoras.Remove(editora);
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