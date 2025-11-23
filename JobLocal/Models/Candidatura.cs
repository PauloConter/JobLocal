using System;

namespace JobLocal.Models
{
    public class Candidatura
    {
        public int Id { get; set; }
        public int VagaId { get; set; }
        public int CandidatoId { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCandidatura { get; set; }
        public string Status { get; set; } = "Pendente";
    }
}