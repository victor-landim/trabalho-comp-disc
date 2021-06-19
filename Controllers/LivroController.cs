using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;
using TrabalhoComp.Models.DTO.Livros;

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
                var livros = _context.Livros.ToList();


                return Ok(livros.Select(LivroResponse.CriarCom));
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

                return Ok(LivroResponse.CriarCom(livro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] LivroRequest request)
        {
            try
            {
                var livro = new Livro();

                livro.AutorId = request.AutorId;
                livro.EditoraId = request.EditoraId;
                livro.CategoriaId = request.EditoraId;
                livro.Titulo = request.Titulo;
                livro.Isnb = request.Isnb;
                livro.Quantidade = request.Quantidade;

                _context.Livros.Add(livro);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] LivroRequest request, int id)
        {
            try
            {
                var livro = _context.Livros.Find(id);

                if (livro == null)
                    throw new InvalidOperationException("livro não encontrada");

                if (livro.EstaAlugado())
                {
                    if (request.Quantidade < livro.Quantidade)
                        throw new InvalidOperationException("Livro esta alugado, não é possivel reduzir a quantidade");
                }

                livro.Titulo = request.Titulo;
                livro.AutorId = request.AutorId;
                livro.EditoraId = request.EditoraId;
                livro.CategoriaId = request.EditoraId;
                livro.Titulo = request.Titulo;
                livro.Isnb = request.Isnb;
                livro.Quantidade = request.Quantidade;

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