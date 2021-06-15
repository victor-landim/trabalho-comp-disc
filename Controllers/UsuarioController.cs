using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoComp.Controllers{

    [Route("api/usuario")]
    public class UsuarioController: ControllerBase{
        public readonly LivrariaContext _context;
        public UsuarioController(LivrariaContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Obter()
        {
            try{
                return Ok(_context.Usuarios.ToList());
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try{

                var usuario = _context.Usuarios.Find(id);

                if(usuario == null){
                   throw new InvalidOperationException("Usuario não encontrado");
                }

                return Ok(usuario);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Usuario usuario)
        {

            try{
                var usuarios = _context.Usuarios.AsEnumerable();

                if(!usuario.CpfValido()){
                    throw new InvalidOperationException("Cpf Inválido");
                }

                if(usuario.CpfJaCadastrado(usuarios))
                {
                   throw new InvalidOperationException("Cpf já cadastrado");
                }

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id){
            try{
                var usuario = _context.Usuarios.Find(id);
                
                if( usuario == null){
                   throw new InvalidOperationException("Usuario não encontrado");
                }

                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}