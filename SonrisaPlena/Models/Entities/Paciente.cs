namespace ClinicaSonrrisaPlena.Models.Entities
{
    public class Paciente : Persona
    {
        public string RUT { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public ICollection<Turno> Turnos { get; set; }
        public ICollection<HistorialTratamiento> Historiales { get; set; }
        public ICollection<PlanTratamiento> Planes { get; set; }

    }
}
