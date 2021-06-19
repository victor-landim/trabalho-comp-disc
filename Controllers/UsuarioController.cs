using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoComp.Models;
using TrabalhoComp.Models.DTO.Usuarios;

namespace TrabalhoComp.Controllers
{

    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        public readonly LivrariaContext _context;
        public UsuarioController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try
            {
                var usuarios = _context.Usuarios.ToList();

                return Ok(usuarios.Select(UsuarioResponse.CriarCom));
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
                var usuario = _context.Usuarios.Find(id);

                if (usuario == null)
                {
                    throw new InvalidOperationException("Usuario não encontrado");
                }

                return Ok(UsuarioResponse.CriarCom(usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] UsuarioRequest request)
        {
            try
            {
                var usuarios = _context.Usuarios.AsEnumerable();

                var usuario = new Usuario();

                usuario.Nome = request.Nome;
                usuario.Cpf = request.Cpf;
                usuario.Endereco = request.Endereco;
                usuario.Telefone = request.Telefone;

                if (!usuario.CpfValido())
                {
                    throw new InvalidOperationException("Cpf Inválido");
                }

                if (usuario.CpfJaCadastrado(usuarios))
                {
                    throw new InvalidOperationException("Cpf já cadastrado");
                }

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar([FromBody] UsuarioRequest request, int id)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);
                var usuarios = _context.Usuarios.AsEnumerable();

                if (usuario == null)
                    throw new InvalidOperationException("usuario não encontrada");

                usuario.Nome = request.Nome;
                usuario.Cpf = request.Cpf;
                usuario.Endereco = request.Endereco;
                usuario.Telefone = request.Telefone;

                if (!usuario.CpfValido())
                {
                    throw new InvalidOperationException("Cpf Inválido");
                }

                if (usuario.CpfJaCadastrado(usuarios))
                {
                    throw new InvalidOperationException("Cpf já cadastrado");
                }

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
                var usuario = _context.Usuarios.Find(id);

                if (usuario == null)
                {
                    throw new InvalidOperationException("Usuario não encontrado");
                }

                if (usuario.PossuiLivroAlugado())
                {
                    throw new InvalidOperationException("Não é possivel realizar a exclusão, usuario possui livro(s) alugado(s)");
                }

                _context.Usuarios.Remove(usuario);
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