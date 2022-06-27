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
        public string estadoExpedienteNombre { get; set; }
        public List<int> ListaidTipoDerivacion { get; set; }
        public string nroDerivacion { get; set; }
        public string codigoEscalafon { get; set; }
        public string nombreApellido { get; set; }
        public string motivo { get; set; }
        public string establecimiento { get; set; }
        public string observacion { get; set; }
        public string nroExpediente { get; set; }
        public string creadorPor { get; set; }
        public string derivadoA { get; set; }
        public string fechaCreacion { get; set; }

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
        public List<Derivacion> ListaHistorialDerivacion(int id)
        {
            List<Derivacion> list = new List<Derivacion>();
            string consulta = @"select 
                            dc.nroExpediente,
                            u.nombreCompleto AS creadorPor,
                            tp.nombre as derivadoA,
                            d.comentario,
                            dc.fecha as fechaCreacion,
                            d.fechaDerivacion,
                            CASE
                                WHEN d.estadoExpediente = 1 THEN 'ACEPTADO'
                                WHEN d.estadoExpediente = 0 THEN 'DEVUELTO'	
                            ELSE '--'
                            END AS estadoExpediente
                            from derivacion as d
                            inner join documento as dc on dc.id = d.idDocumento
                            inner join usuario as u on u.id = dc.idUsuario
                            inner join tipoderivacion as tp on tp.id = d.idTipoDerivacion
                            where d.idDocumento = " + id;
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
                                Derivacion objDerivacion = new Derivacion
                                {
                                    nroExpediente = Utilitarios.ValidarStr(dr["nroExpediente"]),
                                    creadorPor = Utilitarios.ValidarStr(dr["creadorPor"]),
                                    derivadoA = Utilitarios.ValidarStr(dr["derivadoA"]),
                                    comentario = Utilitarios.ValidarStr(dr["comentario"]),
                                    fechaCreacion = Utilitarios.ValidarDate(dr["fechaCreacion"]).ToShortDateString(),
                                    fechaDerivacion = Utilitarios.ValidarDate(dr["fechaDerivacion"]).ToShortDateString(),
                                    estadoExpedienteNombre = Utilitarios.ValidarStr(dr["estadoExpediente"]),
                                };
                                list.Add(objDerivacion);
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