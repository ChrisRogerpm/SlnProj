using Project.Funciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Derivacion
    {
        public int id { get; set; }
        public int idDocumento { get; set; }
        public int idTipoDerivacion { get; set; }
        public string fechaDerivacion { get; set; }
        public string accionRealizar { get; set; }
        public string documentoEmitido { get; set; }
        public string comentario { get; set; }
        public int estadoExpediente { get; set; }
        public List<int> ListaidTipoDerivacion { get; set; }
        public string nroDerivacion { get; set; }
        public string codigoEscalafon { get; set; }
        public string nombreApellido { get; set; }
        public string motivo { get; set; }
        public string establecimiento { get; set; }
        public string observacion { get; set; }

        string _conexion = string.Empty;

        public Derivacion()
        {
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }
        public bool DerivacionRegistrar(Derivacion obj)
        {
            bool respuesta = false;

            string consulta = @"INSERT INTO Derivacion (
                                    idDocumento,
                                    idTipoDerivacion,
                                    fechaDerivacion,
                                    accionRealizar,                                    
                                    documentoEmitido,
                                    comentario,
                                    estadoExpediente
                                )
                                VALUES (
                                    @p0,
                                    @p1,
                                    @p2,
                                    @p3,
                                    @p4,
                                    @p5,
                                    @p6
                                )";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarInteger(obj.idDocumento));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarInteger(obj.idTipoDerivacion));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(obj.fechaDerivacion));
                    query.Parameters.AddWithValue("@p3", Utilitarios.ValidarStr(obj.accionRealizar));
                    query.Parameters.AddWithValue("@p4", Utilitarios.ValidarStr(obj.documentoEmitido));
                    query.Parameters.AddWithValue("@p5", Utilitarios.ValidarStr(obj.comentario));
                    query.Parameters.AddWithValue("@p6", Utilitarios.ValidarInteger(obj.estadoExpediente));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }
            return respuesta;
        }
        public Derivacion DerivacionDetalle(string nroExpediente, string anio)
        {
            Derivacion obj = new Derivacion();
            string consulta = @"select
                                top 1
                                dc.id as idDocumento,
                                d.fechaDerivacion,
                                d.id as nroDerivacion,
                                dc.codigoEscalafon,
                                CONCAT(dc.nombre,' ',dc.apellido) as nombreApellido,
                                dc.motivo,
                                dc.establecimiento,
                                dc.observacion
                                from derivacion as d
                                inner join documento as dc on dc.id = d.idDocumento
                                where dc.nroExpediente = '" + nroExpediente + "' and YEAR(d.fechaDerivacion) = '" + anio + "'";
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
                                obj.idDocumento = Utilitarios.ValidarInteger(dr["idDocumento"]);
                                obj.fechaDerivacion = Utilitarios.ValidarDate(dr["fechaDerivacion"]).ToShortDateString();
                                obj.nroDerivacion = Utilitarios.ValidarStr(dr["nroDerivacion"]);
                                obj.codigoEscalafon = Utilitarios.ValidarStr(dr["codigoEscalafon"]);
                                obj.nombreApellido = Utilitarios.ValidarStr(dr["nombreApellido"]);
                                obj.motivo = Utilitarios.ValidarStr(dr["motivo"]);
                                obj.establecimiento = Utilitarios.ValidarStr(dr["establecimiento"]);
                                obj.observacion = Utilitarios.ValidarStr(dr["observacion"]);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return obj;
        }
    }
}