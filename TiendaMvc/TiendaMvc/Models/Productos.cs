using TiendaMvc.VIewModels.Producto;

namespace Tp5Tienda.Models
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }

        
        public static Productos MapCrearProductoVm(CrearProductoViewModel crearProd)
        {
            return new Productos
            {
                Descripcion = crearProd.Descripcion,
                Precio = crearProd.Precio
            };
        }
        public static Productos MapActualizarProductoVm(ActualizarProductoVM actProd)
        {
            return new Productos
            {
                IdProducto = actProd.IdProducto,
                Descripcion = actProd.Descripcion,
                Precio = actProd.Precio
            };
        }
    }
}
