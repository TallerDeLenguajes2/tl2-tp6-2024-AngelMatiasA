namespace Tp5Tienda.Models
{
    public class Presupuestos
    {
        public int IdPresupuesto { get; set; }
        public string NombreDestinatario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<PresupuestoDetalle> Detalle { get; set; } = new List<PresupuestoDetalle>();

        public double MontoPresupuesto()
        {
            double montoTotal = 0;
            foreach (var det in Detalle)
            {
                montoTotal += Convert.ToDouble(det.Producto.Precio) * det.Cantidad;
            }
            return montoTotal;

        }
        public double MontoPresupuestoConIva()
        {
            double conIva = 0;
            conIva = MontoPresupuesto();
            conIva = conIva * 1.21;
            return conIva;

        }
        public int CantiadProductos()
        {
            int cantidad = 0;
            foreach (var det in Detalle)
            {
                cantidad += det.Cantidad;
                
            }
            return cantidad;

        }
    }
}
