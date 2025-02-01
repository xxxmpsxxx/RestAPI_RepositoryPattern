using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI_RepositoryPattern.Dto;
using RestAPI_RepositoryPattern.Services;

namespace RestAPI_RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioInterface.GetUsuarios();

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario(UsuarioCriarDto instance)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(instance);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> PutUsuario(UsuarioEditarDto instance)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(instance);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuarios = await _usuarioInterface.RemoverUsuario(id);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }
    }
}
