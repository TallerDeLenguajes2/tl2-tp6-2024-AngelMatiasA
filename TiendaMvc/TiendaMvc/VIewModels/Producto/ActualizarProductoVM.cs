using System.ComponentModel.DataAnnotations;
using Tp5Tienda.Models;

namespace TiendaMvc.VIewModels.Producto
{
    public class ActualizarProductoVM
    {
       
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre del Producto")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Pecio")]
        public int Precio { get; set; }

        public ActualizarProductoVM(Productos Producto)
        {
            IdProducto = Producto.IdProducto;
            Descripcion = Producto.Descripcion;
            Precio = Producto.Precio;

        }

        public ActualizarProductoVM()
        {
        }

    }
}
