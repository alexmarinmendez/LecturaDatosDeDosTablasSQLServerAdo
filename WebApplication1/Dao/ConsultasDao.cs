using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApplication1.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Dao
{
    public class ConsultasDao
    {
        string cadena = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;

        public List<Categorias> ListarCategorias()
        {
            List<Categorias> lista = new List<Categorias>();

            SqlConnection con = new SqlConnection(cadena);
            con.Open();

            string sql = "select c.IdCategoria, c.NombreCategoria, c.Descripcion, c.Eliminado from Categorias c";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Categorias categoria = new Categorias()
                {
                    IdCategoria = reader.GetInt32(0),
                    NombreCategoria = reader.GetString(1),
                    Descripcion = reader.GetString(2),
                    Eliminado = reader.GetString(3),
                };
                lista.Add(categoria);
            }
            reader.Close();
            con.Close();
            return lista;
        }


        public List<Productos> ListarProductos(int IdCategoria)
        {
            List<Productos> lista = new List<Productos>();

            SqlConnection con = new SqlConnection(cadena);
            con.Open();

            string sql = "select p.IdProducto, p.NombreProducto, p.PrecioUnidad as Precio, p.UnidadesEnExistencia as Stock, prv.NombreProveedor, prv.Pais from Productos as p inner join Proveedores as prv on p.IdProveedor = prv.IdProveedor where p.IdCategoria = " + IdCategoria;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Productos producto = new Productos()
                {
                    IdProducto = reader.GetInt32(0),
                    NombreProducto = reader.GetString(1),
                    Precio = reader.GetDecimal(2),
                    Stock = reader.GetInt16(3),
                    NombreProveedor = reader.GetString(4),
                    Pais = reader.GetString(5),
                };
                lista.Add(producto);
            }
            reader.Close();
            con.Close();
            return lista;
        }

        public List<Productos> ListarProductosSP(int IdCategoria)
        {
            List<Productos> lista = new List<Productos>();

            SqlConnection con = new SqlConnection(cadena);
            con.Open();

            string sql = "pa_productos_por_categoria";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Productos producto = new Productos()
                {
                    IdProducto = reader.GetInt32(0),
                    NombreProducto = reader.GetString(1),
                    Precio = reader.GetDecimal(2),
                    Stock = reader.GetInt16(3),
                    NombreProveedor = reader.GetString(4),
                    Pais = reader.GetString(5),
                };
                lista.Add(producto);
            }
            reader.Close();
            con.Close();
            return lista;
        }
    }
}