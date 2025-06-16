using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSonrrisaPlena.Models.Entities
{
    public class Tratamiento
    {
        [Key]
        public int IdTratamiento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        [Precision(10, 2)]
        public decimal PrecioEstimado { get; set; }

        public ICollection<HistorialTratamiento> Historiales { get; set; }
        public ICollection<PasoPlan> Pasos { get; set; }

    }
}
