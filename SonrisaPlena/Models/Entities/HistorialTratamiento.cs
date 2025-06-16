using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSonrrisaPlena.Models.Entities
{
    public class HistorialTratamiento
    {
        [Key]
        public int IdHistorial { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public int IdTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; }

        public int IdOdontologo { get; set; }
        public Odontologo Odontologo { get; set; }
    }
}
