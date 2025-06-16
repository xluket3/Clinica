using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSonrrisaPlena.Models.Entities
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }
        public DateTime FechaHora { get; set; }
        public int Duracion { get; set; }
        public string Estado { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public int IdOdontologo { get; set; }
        public Odontologo Odontologo { get; set; }
    }
}
