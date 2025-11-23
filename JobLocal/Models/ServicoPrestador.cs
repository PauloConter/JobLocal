using System;

namespace JobLocal.Models
{
    public class ServicoPrestador
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string TipoServico { get; set; } // ⭐⭐ ADICIONE ESTA LINHA ⭐⭐
        public decimal Valor { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}