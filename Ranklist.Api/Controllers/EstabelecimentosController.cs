[ApiController]
[Route("api/[controller]")]
public class EstabelecimentosController : ControllerBase
{
    private readonly IEstabelecimentoRepository _repo;

    public EstabelecimentosController(IEstabelecimentoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var estabelecimentos = await _repo.ListarTodosAsync();
        return Ok(estabelecimentos);
    }
}