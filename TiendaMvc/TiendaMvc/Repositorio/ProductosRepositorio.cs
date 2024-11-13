using System.Data.SQLite;
using TiendaMvc.VIewModels.Producto;
using Tp5Tienda.Models;

namespace Tp5Tienda.Repositorio
{
    public class ProductosRepositorio
    {
        private string connectionString = @"Data Source =  Tienda.db;Initial Catalog=Northwind;Integrated Security=true";

        public List<Productos> MostrarProductos()
        {
            List<Productos> productos = new List<Productos>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string queryString = @"SELECT * FROM Productos;";
                var command = new SQLiteCommand(queryString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var producto = new Productos();
                        producto.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);
                        productos.Add(producto);
                    }
                }
                connection.Close();
            }

            if (productos.Count == 0)
            {
                return null;
            }

            return productos;
        }

        public Productos MostrarProductoPorId(int idProd)
        {
            Productos producto = new Productos();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string queryString = @"SELECT * FROM Productos WHERE IdProducto = @idProd;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idProd", idProd));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        producto.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);
                       
                    }
                }
                connection.Close();
            }

            

            return producto;
        }
        public CrearProductoViewModel CrearProductos( CrearProductoViewModel nuevoProducto)
        {
            int rowAffected = 0;
            if (nuevoProducto == null)
            {
                return null;
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio);";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@Descripcion", nuevoProducto.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@Precio", nuevoProducto.Precio));
                rowAffected = command.ExecuteNonQuery();
                connection.Close();
               
            }

            if (rowAffected == 0)
            {
                return null;
            }

            return nuevoProducto;
        }
         public Productos ModificarProductos(int idProducto, Productos nuevoProducto)
        {
            int rowAffected = 0;
            if (nuevoProducto == null)
            {
                return null;
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @idProducto;";

                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@Descripcion", nuevoProducto.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@Precio", nuevoProducto.Precio));
                command.Parameters.Add(new SQLiteParameter("@idProducto", idProducto));


                rowAffected = command.ExecuteNonQuery();
                connection.Close();
               
            }

            if (rowAffected == 0)
            {
                return null;
            }

            return nuevoProducto;
        }

        public bool EliminarProducto(int idProd)
        {
            int rowAffected = 0;
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"DELETE FROM Productos WHERE idProducto = @idProd;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idProd", idProd));
                rowAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            return rowAffected > 0;
        }



    }
}
