using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Tp5Tienda.Models;

namespace TiendaMvc.VIewModels.Producto
{
    public class CrearProductoViewModel
    {



        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre del Producto")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Pecio")]
        public int Precio { get; set; }
      
        public CrearProductoViewModel(Productos Producto)
        {
            Descripcion = Producto.Descripcion;
            Precio = Producto.Precio;
           
        }

        public CrearProductoViewModel()
        {
        }
    }
}
