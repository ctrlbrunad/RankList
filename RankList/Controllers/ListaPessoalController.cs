using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RankList.Models;
using RankList.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ListasPessoaisController : ControllerBase
{
    private readonly IListaPessoalRepository _repo;

    public ListasPessoaisController(IListaPessoalRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ListaPessoalDto dto)
    {
        // Validação do usuário via Firebase Auth
        // var usuarioId = User.FindFirst("uid")?.Value;
        // if (string.IsNullOrEmpty(usuarioId))
        //     return Unauthorized("Usuário não autenticado.");

        var usuarioId = "teste"; // Valor fixo para testes

        // Validação dos dados obrigatórios
        if (string.IsNullOrWhiteSpace(dto.Nome) || dto.EstabelecimentoIds == null || !dto.EstabelecimentoIds.Any())
            return BadRequest("Nome da lista e estabelecimentos são obrigatórios.");

        var lista = new ListaPessoal
        {
            UsuarioId = usuarioId,
            Nome = dto.Nome,
            Estabelecimentos = dto.EstabelecimentoIds
                .Select(id => new ListaPessoalEstabelecimento { EstabelecimentoId = id })
                .ToList()
        };

        try
        {
            await _repo.CriarAsync(lista);
            return CreatedAtAction(nameof(Criar), new { id = lista.Id }, lista);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar lista: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var listas = await _repo.ListarTodosAsync();
        return Ok(listas);
    }
}