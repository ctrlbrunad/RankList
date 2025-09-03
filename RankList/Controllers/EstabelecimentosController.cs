using Microsoft.AspNetCore.Mvc;
using RankList.Models;

[ApiController]
[Route("api/[controller]")]
public class EstabelecimentosController : ControllerBase
{
    private static List<Estabelecimento> estabelecimentos = new();

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(estabelecimentos);
    }

    [HttpPost]
    public IActionResult Criar([FromBody] Estabelecimento estabelecimento)
    {
        estabelecimento.Id = estabelecimentos.Count + 1;
        estabelecimentos.Add(estabelecimento);
        return CreatedAtAction(nameof(Criar), new { id = estabelecimento.Id }, estabelecimento);
    }
}