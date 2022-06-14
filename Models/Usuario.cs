using Project.Funciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public int idTipoUsuario { get; set; }
        public string nombreCompleto { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public int estado { get; set; }

        string _conexion = string.Empty;

        public Usuario()
        {
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }
        public Usuario ValidarLogin(Usuario obj)
        {
            Usuario objUsu = new Usuario();
            string consulta = @"SELECT * FROM usuario AS u WHERE u.usuario = @p0 AND u.password = @p1";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(obj.usuario));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(obj.password));
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                objUsu.id = Utilitarios.ValidarInteger(dr["id"]);
                                objUsu.nombreCompleto = Utilitarios.ValidarStr(dr["nombreCompleto"]);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return objUsu;
        }
    }
}