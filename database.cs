using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace WindowsFormsApp6
{
    public class database
    {
        //BLL de Clientes en MYSQL del sistema de factuacion
        //Prueba Con la tabla Clientes


        public static MySqlConnection db;
        // Esto es para establecer la conexion 
        public bool TestConnection()
        {
            bool paso = true;
            try
            {
                //Para que funcione a qui se debe de poner los datos del servidor, o si lo correran en su maquina los datos de su maquina de MySQL
                db = new MySqlConnection("server=localhost; database = FacturacionDb; user id=root;password=root");
                db.Open();


            }
            catch (Exception)
            {
                paso = false;
                throw;
            }

            return paso;
        }

        public DataTable Consultar()
        {


            DataTable dt = new DataTable();
            TestConnectiong();
            MySqlDataAdapter con = new MySqlDataAdapter("select * from Clientes", db);
            try
            {


                con.Fill(dt);
                db.Close();


            }
            catch (Exception)
            {

                throw;
            }// Cerramos la conexion
            finally
            {
                db.Close();
            }
            return dt;
        }

        public bool Eliminar(string id)
        {

            MySqlDataAdapter dater = new MySqlDataAdapter();
            bool estado = true;
            try
            {
                TestConnectiong();
                dater.DeleteCommand = new MySqlCommand(" delete from Clientes where IdCliente="+id, db);

                dater.DeleteCommand.Connection = db;
                dater.DeleteCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                estado = false;
                throw;

            }
            finally// Cerramos la conexion
            {
                db.Close();
            }
            return estado;
        }


        public bool Guardar(Cliente cliente)
        {
            MySqlDataAdapter dater = new MySqlDataAdapter();
            bool estado = true;
            try
            {
                TestConnectiong();
                dater.InsertCommand = new MySqlCommand("insert into Clientes (Nombre,Direccion,Cedula,Telefono) values ('" + cliente.Nombre + "','" + cliente.Direccion + "','" + cliente.Cedula + "','" + cliente.Telefono + "')", db);
                
                dater.InsertCommand.Connection = db;
                dater.InsertCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                estado = false;
                throw;

            }//Cerramos la conexion
            finally
            {
                db.Close();
            }
            return estado;
        }


        //asi se pone un datatable en una lista 
        public List<Cliente> Buscar()
        {
            
            DataTable dt = new DataTable();
            
            TestConnectiong();
            MySqlDataAdapter con = new MySqlDataAdapter("select * from Clientes", db);
            try
            {


                con.Fill(dt);
                db.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally// al final siempre se cierra la conexion
            {
                db.Close();
            }


            // Asi pasamos un DataTable a una lista
            List<Cliente> listName = dt.AsEnumerable().Select(m => new Cliente()
            {
                IdCliente = m.Field<int>("IdCliente"),
                Nombre = m.Field<string>("Nombre"),
                Direccion = m.Field<string>("Direccion"),
                Cedula = m.Field<string>("Cedula"),
                Telefono = m.Field<string>("Cedula")
            }).ToList();


            return listName;
        }

        //Este el codigo para modificar 
        public bool Modificar(Cliente cliente)
        {
            MySqlDataAdapter dater = new MySqlDataAdapter();
            bool estado = true;
            try
            {
                TestConnectiong();
               
                dater.UpdateCommand = new MySqlCommand("update Clientes set Nombre= '" + cliente.Nombre+"', Direccion = '"+cliente.Direccion+"', Cedula = '"+cliente.Cedula+"', Telefono = '"+cliente.Telefono+"'where IdCliente = '"+cliente.IdCliente+"'", db);

                dater.UpdateCommand.Connection = db;
                dater.UpdateCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                estado = false;
                throw;

            }//Al final siempre se cierra la conexion
            finally
            {
                db.Close();
            }
            return estado;

        }






    }
}



