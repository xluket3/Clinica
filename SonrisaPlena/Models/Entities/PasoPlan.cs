using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSonrrisaPlena.Models.Entities
{
    public class PasoPlan
    {
        [Key]
        public int IdPaso { get; set; }
        public DateTime FechaEstimada { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }

        public int IdPlan { get; set; }
        public PlanTratamiento Plan { get; set; }

        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; }
    }
}
