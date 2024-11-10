﻿using Microsoft.AspNetCore.Mvc;
using Tp5Tienda.Models;
using Tp5Tienda.Repositorio;

namespace TiendaMvc.Controllers
{
    public class ProductoController : Controller
    {
        private  ProductosRepositorio _productoRepo;

        public ProductoController()
        {
            _productoRepo = new ProductosRepositorio();
        }

        

        [HttpGet]
        public IActionResult Index()
        {
            var productos = _productoRepo.MostrarProductos();
            if (productos.Count == 0)
            {
                return BadRequest("No se pudo obtener la lista de productos de la base de datos");

            }
            
            return View(productos);
        }

        [HttpGet]
        public IActionResult CrearProducto()
        {
          
            return View(new PostProducto());
        }

        [HttpPost]
        public IActionResult CrearProducto(PostProducto nuevoProd)
        {
            var producto = _productoRepo.CrearProductos(nuevoProd);
            if (producto == null)
            {
                return BadRequest("No se pudo crear el producto");

            }
            
            return RedirectToAction("Index");
        }
    }
}