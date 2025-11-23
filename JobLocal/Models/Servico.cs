namespace JobLocal.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string NomeContratante { get; set; }
        public string TipoServico { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Cidade { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
    }
}