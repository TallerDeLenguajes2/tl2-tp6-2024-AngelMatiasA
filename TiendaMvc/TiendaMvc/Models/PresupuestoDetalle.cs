using System.Text.Json.Serialization;

namespace Tp5Tienda.Models
{
    public class PresupuestoDetalle
    {
        [JsonIgnore]
        public int IdPresupuesto { get; set; }
        public Productos Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
