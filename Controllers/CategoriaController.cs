using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;
using TrabalhoComp.Models.DTO.Categorias;

namespace TrabalhoComp.Controllers
{

    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        public readonly LivrariaContext _context;
        public CategoriaController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                var categorias = _context.Categorias.ToList();

                return Ok(categorias.Select(CategoriaResponse.CriarCom));
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
                var categoria = _context.Categorias.Find(id);

                if (categoria == null)
                {
                    throw new InvalidOperationException("Categoria não encontrada");
                }

                return Ok(CategoriaResponse.CriarCom(categoria));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] CategoriaRequest request)
        {
            try
            {
                var categoria = new Categoria();
                categoria.Nome =  request.Nome;  

                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] CategoriaRequest request, int id)
        {
            try
            {
                var categoria = _context.Categorias.Find(id);

                if (categoria == null)
                    throw new InvalidOperationException("Categoria não encontrada");

                categoria.Nome = request.Nome;

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
                var categoria = _context.Categorias.Find(id);

                if (categoria == null)
                {
                    throw new InvalidOperationException("Categoria não encontrada");
                }

                var categoriaLivro = _context.Livros.Find(id);

                if (categoriaLivro != null)
                {
                    throw new InvalidOperationException("Não é possivel realizar exclusão, categoria pertence a um livro cadastrado");
                }

                _context.Categorias.Remove(categoria);
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