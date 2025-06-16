using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSonrrisaPlena.Models.Entities
{
    [Index(nameof(IdPaciente))]
    [Index(nameof(IdOdontologo))]
    public class PlanTratamiento
    {
        [Key]
        public int IdPlan { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ObservacionesGenerales { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public int IdOdontologo { get; set; }
        public Odontologo Odontologo { get; set; }

        public ICollection<PasoPlan> Pasos { get; set; }
    }
}
