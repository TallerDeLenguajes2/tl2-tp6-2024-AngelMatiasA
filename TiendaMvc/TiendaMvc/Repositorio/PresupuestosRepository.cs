using System.Data.SQLite;
using Tp5Tienda.Models;

namespace Tp5Tienda.Repositorio
{
    public class PresupuestosRepository
    {
        private string connectionString = @"Data Source =  Tienda.db;Initial Catalog=Northwind;Integrated Security=true";

        public List<Presupuestos> MostrarPresupuestos()
        {
            List<Presupuestos> presupuestos = new List<Presupuestos>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"SELECT * FROM Presupuestos;";
                var command = new SQLiteCommand(queryString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Presupuestos presupuesto = new Presupuestos();
                        presupuesto.IdPresupuesto = Convert.ToInt32(reader["Idpresupuesto"]);
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                        presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                        presupuestos.Add(presupuesto);
                    }
                }
                connection.Close();
            }

            if (presupuestos.Count == 0)
            {
                return null;
            }

            return presupuestos;
        }

        public List<Presupuestos> MostrarPresupuestosConMontos()
        {
            Presupuestos presupuesto = null;
            List<Presupuestos> presupuestos = new List<Presupuestos>();


            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"SELECT 
                                P.idPresupuesto, 
                                P.NombreDestinatario, 
                                P.FechaCreacion, 
                                PR.idProducto, 
                                PR.Descripcion AS Producto, 
                                PR.Precio, 
                                PD.Cantidad 
                              FROM 
                                Presupuestos P
                              JOIN 
                                PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
                              JOIN 
                                Productos PR ON PD.idProducto = PR.idProducto;";
                var command = new SQLiteCommand(queryString, connection);
                int currentId = -1;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        currentId = Convert.ToInt32(reader["idPresupuesto"]);
                        if (presupuesto == null || presupuesto.IdPresupuesto != currentId)
                        {
                            if (presupuesto != null)
                            {
                                double precioPresupuesto = presupuesto.MontoPresupuesto();
                                Console.WriteLine($"El precio total del presupuesto de id nro {currentId} es: {precioPresupuesto}");

                                double precioPresupuestoConIva = presupuesto.MontoPresupuestoConIva();
                                Console.WriteLine($"El precio total del presupuesto es: {precioPresupuestoConIva}");

                                presupuestos.Add(presupuesto);

                            }
                            presupuesto = new Presupuestos
                            {
                                IdPresupuesto = currentId,
                                NombreDestinatario = reader["NombreDestinatario"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                Detalle = new List<PresupuestoDetalle>()
                            };
                        }

                        var detalle = new PresupuestoDetalle
                        {
                            IdPresupuesto = presupuesto.IdPresupuesto,
                            Producto = new Productos
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                Descripcion = reader["Producto"].ToString(),
                                Precio = Convert.ToInt32(reader["Precio"])
                            },
                            Cantidad = Convert.ToInt32(reader["Cantidad"])
                        };

                        presupuesto.Detalle.Add(detalle);
                    }
                    if (presupuesto != null)
                    {
                        double precioPresupuesto = presupuesto.MontoPresupuesto();
                        Console.WriteLine($"El precio total del presupuesto de id nro {currentId} es: {precioPresupuesto}");

                        double precioPresupuestoConIva = presupuesto.MontoPresupuestoConIva();
                        Console.WriteLine($"El precio total del presupuesto es: {precioPresupuestoConIva}");
                        presupuestos.Add(presupuesto);
                        
                    }
                }
                connection.Close();
            }

            return presupuestos;
        }

        public Presupuestos CrearPresupuesto(Presupuestos nuevoPresup)
        {
            int rowAffected = 0;
            if (nuevoPresup == null)
            {
                return null;
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombreDest, @fechaCreac);";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombreDest", nuevoPresup.NombreDestinatario));
                command.Parameters.Add(new SQLiteParameter("@fechaCreac", DateTime.Now));
                rowAffected = command.ExecuteNonQuery();
                connection.Close();


            }

            if (rowAffected == 0)
            {
                return null;
            }

            return nuevoPresup;
        }


        public PresupuestoDetalle AgregarDetalle(PresupuestoDetalle nuevoDetalle, int idPresupuesto)
        {
            int rowAffected = 0;
            if (nuevoDetalle == null)
            {
                return null;
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPre, @idProd, @cantid);";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idPre", idPresupuesto));
                command.Parameters.Add(new SQLiteParameter("@idProd", nuevoDetalle.Producto.IdProducto));
                command.Parameters.Add(new SQLiteParameter("@cantid", nuevoDetalle.Cantidad));
                rowAffected = command.ExecuteNonQuery();
                connection.Close();


            }

            if (rowAffected == 0)
            {
                return null;
            }

            return nuevoDetalle;
        }






        public Presupuestos ObtenerPresupuestoPorId(int idPresupuesto)
        {
            Presupuestos presupuesto = null;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"SELECT 
                                P.idPresupuesto, 
                                P.NombreDestinatario, 
                                P.FechaCreacion, 
                                PR.idProducto, 
                                PR.Descripcion AS Producto, 
                                PR.Precio, 
                                PD.Cantidad 
                              FROM 
                                Presupuestos P
                              JOIN 
                                PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
                              JOIN 
                                Productos PR ON PD.idProducto = PR.idProducto
                              WHERE 
                                P.idPresupuesto = @idPresupuesto;";
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idPresupuesto", idPresupuesto));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (presupuesto == null)
                        {
                            presupuesto = new Presupuestos
                            {
                                IdPresupuesto = idPresupuesto,
                                NombreDestinatario = reader["NombreDestinatario"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                Detalle = new List<PresupuestoDetalle>()
                            };
                        }

                        var detalle = new PresupuestoDetalle
                        {
                            IdPresupuesto = idPresupuesto,
                            Producto = new Productos
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                Descripcion = reader["Producto"].ToString(),
                                Precio = Convert.ToInt32(reader["Precio"])
                            },
                            Cantidad = Convert.ToInt32(reader["Cantidad"])
                        };

                        presupuesto.Detalle.Add(detalle);
                    }
                }
                connection.Close();
            }

            return presupuesto;
        }


    }
}
