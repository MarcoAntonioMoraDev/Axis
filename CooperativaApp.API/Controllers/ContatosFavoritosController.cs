using CooperativaApp.Application.DTOs;
using CooperativaApp.Application.Interfaces.Services;
using CooperativaApp.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CooperativaApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoFavoritoController : ControllerBase
    {
        private readonly IContatoFavoritoService _contatoFavoritoService;

        public ContatoFavoritoController(IContatoFavoritoService contatoFavoritoService)
        {
            _contatoFavoritoService = contatoFavoritoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contatoFavoritoDto = await _contatoFavoritoService.GetByIdAsync(id);
            return contatoFavoritoDto is null ? NotFound("Contato favorito não encontrado.") : Ok(contatoFavoritoDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _contatoFavoritoService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContatoFavoritoDTORequest contatoFavoritoDTORequest)
        {
            var createdContatoFavorito = await _contatoFavoritoService.AddAsync(contatoFavoritoDTORequest);
            return CreatedAtAction(nameof(GetById), new { id = createdContatoFavorito.CodigoContatoFavoritoId }, createdContatoFavorito);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContatoFavoritoDTORequest contatoCooperadoDTORequest)
        {
            var contatoCooperado = await _contatoFavoritoService.GetByIdAsync(id);
            if (contatoCooperado == null)
            {
                return BadRequest(new { message = "IDs não correspondem." });
            }

            await _contatoFavoritoService.UpdateAsync(contatoCooperadoDTORequest);
            return CreatedAtAction(nameof(GetById), new { id = contatoCooperado.CodigoContatoFavoritoId }, contatoCooperado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contatoFavorito = await _contatoFavoritoService.DeleteAsync(id);
            return contatoFavorito == null ? NotFound(new { message = "Contato favorito não encontrado." }) : Ok(new { message = "Registro deletado com sucesso" });
        }
    }
}