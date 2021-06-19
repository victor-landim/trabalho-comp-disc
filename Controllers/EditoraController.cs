using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;
using TrabalhoComp.Models.DTO.Editoras;

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
                var editoras = _context.Editoras.ToList();

                return Ok(editoras.Select(EditoraResponse.CriarCom));
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
                    throw new InvalidOperationException("Editora não encontrada");
                }

                return Ok(EditoraResponse.CriarCom(editora));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] EditoraRequest request)
        {
            try
            {
                var editora = new Editora();
                editora.Nome = request.Nome;

                _context.Editoras.Add(editora);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] EditoraRequest request, int id)
        {
            try
            {
                var editora = _context.Editoras.Find(id);

                if (editora == null)
                    throw new InvalidOperationException("editora não encontrada");

                editora.Nome = request.Nome;

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