using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;
using TrabalhoComp.Models.DTO.Autores;

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
                var autores = _context.Autores.ToList();
                
                return Ok(autores.Select(AutorResponse.CriarCom));
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

                return Ok(AutorResponse.CriarCom(autor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] AutorRequest request)
        {
            try
            {
                var autor = new Autor();
                autor.Nome = request.Nome;

                _context.Autores.Add(autor);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] AutorRequest request, int id){
            try
            {
                var autor = _context.Autores.Find(id);

                if(autor == null)
                    throw new InvalidOperationException("Autor não encontrado");

                autor.Nome = request.Nome;

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
                
                if (autor == null)
                {
                    throw new InvalidOperationException("Autor não encontrado");
                }
                
                var autorLivro = _context.Livros.Find(id);

                if (autorLivro != null)
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