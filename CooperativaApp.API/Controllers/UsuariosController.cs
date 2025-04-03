using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CooperativaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Create([FromBody] LoginDTORequest request)
        {
            var result = await _usuarioService.Login(request);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTORequest request)
        {
            var result = await _usuarioService.Create(request);
            return Ok(result);
        }
    }
}
