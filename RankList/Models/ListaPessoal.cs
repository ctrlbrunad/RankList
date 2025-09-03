public class ListaPessoal
{
    public int Id { get; set; }
    public string UsuarioId { get; set; }
    public string Nome { get; set; }
    public List<ListaPessoalEstabelecimento> Estabelecimentos { get; set; }
}