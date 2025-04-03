using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CooperativaApp.Presentation.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class CooperativasController : ControllerBase
    {
        private readonly ICooperativaService _cooperativaService;

        public CooperativasController(ICooperativaService cooperativaService)
        {
            _cooperativaService = cooperativaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cooperativaDto = await _cooperativaService.GetByIdAsync(id);
            return cooperativaDto == null ? NotFound("Cooperativa não encontrada ou inativa.") : Ok(cooperativaDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cooperativaService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CooperativaDTORequest cooperativaDTORequest)
        {
            var createdCooperativa = await _cooperativaService.AddAsync(cooperativaDTORequest);
            return CreatedAtAction(nameof(GetById), new { id = createdCooperativa.CodigoCooperativaId }, createdCooperativa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CooperativaDTORequest cooperativaDTORequest)
        {
            var updatedCooperativa = await _cooperativaService.UpdateAsync(id, cooperativaDTORequest);
                return updatedCooperativa == null ? NotFound(new { message = "Cooperativa não encontrada ou inativa." }) : Ok(updatedCooperativa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cooperativaService.DeleteAsync(id);
            return result == null ? NotFound("Cooperativa não encontrada ou inativa.") : Ok(new { message = "Registro deletado com sucesso" });
        }
    }
}