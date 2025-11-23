using System;

namespace JobLocal.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int TrabalhadorId { get; set; }
        public int AvaliadorId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;
    }
}