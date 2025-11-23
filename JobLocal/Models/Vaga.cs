using System;

namespace JobLocal.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Cidade { get; set; }
        public string TipoServico { get; set; }
        public decimal Valor { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
        public bool Ativa { get; set; } = true;
    }
}