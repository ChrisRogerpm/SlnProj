using Project.Funciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class TipoDerivacion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
        string _conexion = string.Empty;

        public TipoDerivacion()
        {
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }
        public List<TipoDerivacion> TipoDerivacionListar()
        {
            List<TipoDerivacion> list = new List<TipoDerivacion>();

            string consulta = @"select * from tipoDerivacion";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TipoDerivacion objTipoDerivacion = new TipoDerivacion
                                {
                                    id = Utilitarios.ValidarInteger(dr["id"]),                                   
                                    nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                    estado = Utilitarios.ValidarInteger(dr["estado"]),

                                };
                                list.Add(objTipoDerivacion);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }

            return list;
        }

    }
}