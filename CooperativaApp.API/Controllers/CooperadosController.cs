using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CooperativaApp.Presentation.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class CooperadosController : ControllerBase
    {
        private readonly ICooperadoService _cooperadoService;

        public CooperadosController(ICooperadoService cooperadoService)
        {
            _cooperadoService = cooperadoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cooperadoDto = await _cooperadoService.GetByIdAsync(id);
            return cooperadoDto is null ? NotFound("Cooperado não encontrado.") : Ok(cooperadoDto);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            var cooperadosDto = await _cooperadoService.GetByNomeAsync(nome);
            return Ok(cooperadosDto);
        }

        [HttpGet("conta/{conta}")]
        public async Task<IActionResult> GetByConta(string conta)
        {
            var cooperadosDto = await _cooperadoService.GetByContaAsync(conta);
            return Ok(cooperadosDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cooperadosDto = await _cooperadoService.GetAllAsync();
            return Ok(cooperadosDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CooperadoDTORequest cooperadoDTORequest)
        {
            var createdCooperado = await _cooperadoService.AddAsync(cooperadoDTORequest);
            return CreatedAtAction(nameof(GetById), new { id = createdCooperado.CodigoCooperadoId }, createdCooperado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CooperadoDTOResponse cooperadoDto)
        {

            if (id != cooperadoDto.CodigoCooperadoId)
            {
                return BadRequest("IDs não correspondem.");
            }
            await _cooperadoService.UpdateAsync(cooperadoDto);
            return Ok(cooperadoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cooperado = await _cooperadoService.DeleteAsync(id);
            return cooperado is null ? NotFound("Cooperado não encontrado ou inativo.") : Ok(new { message = "Registro deletado com sucesso" });
        }
    }
}