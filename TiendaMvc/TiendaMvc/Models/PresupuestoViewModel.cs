namespace Tp5Tienda.Models
{
    public class PresupuestoViewModel
    {
        public int IdPresupuesto { get; set; }
        public string NombreDestinatario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<PresupuestoDetalle> Detalles { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
    }
}
