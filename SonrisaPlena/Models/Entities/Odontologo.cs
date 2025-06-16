namespace ClinicaSonrrisaPlena.Models.Entities
{
    public class Odontologo: Persona
    {
        public string Matricula { get; set; }
        public string Especialidad { get; set; }

        public ICollection<Turno> Turnos { get; set; }
        public ICollection<HistorialTratamiento> Historiales { get; set; }
        public ICollection<PlanTratamiento> Planes { get; set; }
    }
}
