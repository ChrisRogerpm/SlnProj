using Project.Funciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Resolucion
    {
        public int id { get; set; }
        public int idDocumento { get; set; }
        public string codigo { get; set; }
        public string fechaAsignacion { get; set; }
        public string motivo { get; set; }
        public string dni { get; set; }
        public string tituloProfesional { get; set; }
        public string especialidad { get; set; }
        public string establecimiento { get; set; }
        public string nivelMagisterial { get; set; }
        public string jornadaLaboral { get; set; }
        public string regimenPension { get; set; }
        public string nroIpss { get; set; }
        public string fechaIngreso { get; set; }
        public string fechaCese { get; set; }
        public string codigoEscalafon { get; set; }
        public string nombreApellido { get; set; }
        public string codigoModular { get; set; }
        public string observacion { get; set; }
        public string tipoServidor { get; set; }
        public int anios { get; set; }
        public int meses { get; set; }
        public int dias { get; set; }
        public string cargo { get; set; }
        public string otros { get; set; }
        public Documento objDocument;
        string _conexion = string.Empty;

        public Resolucion()
        {
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }
        public List<Resolucion> ResolucionListar()
        {
            List<Resolucion> list = new List<Resolucion>();

            string consulta = @"select * from resolucion";

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
                                Resolucion objResolucion = new Resolucion
                                {
                                    id = Utilitarios.ValidarInteger(dr["id"]),
                                    idDocumento = Utilitarios.ValidarInteger(dr["idDocumento"]),
                                    motivo = Utilitarios.ValidarStr(dr["motivo"]),
                                    dni = Utilitarios.ValidarStr(dr["dni"]),
                                    tituloProfesional = Utilitarios.ValidarStr(dr["tituloProfesional"]),
                                    especialidad = Utilitarios.ValidarStr(dr["especialidad"]),
                                    establecimiento = Utilitarios.ValidarStr(dr["establecimiento"]),
                                    nivelMagisterial = Utilitarios.ValidarStr(dr["nivelMagisterial"]),
                                    jornadaLaboral = Utilitarios.ValidarStr(dr["jornadaLaboral"]),
                                    regimenPension = Utilitarios.ValidarStr(dr["regimenPension"]),
                                    nroIpss = Utilitarios.ValidarStr(dr["nroIpss"]),
                                    fechaIngreso = Utilitarios.ValidarDate(dr["fechaIngreso"]).ToShortDateString(),
                                    fechaCese = Utilitarios.ValidarDate(dr["fechaCese"]).ToShortDateString(),
                                    codigoEscalafon = Utilitarios.ValidarStr(dr["codigoEscalafon"]),

                                };
                                list.Add(objResolucion);
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
        public List<Resolucion> ResolucionListarConsultar(string nroExpediente, string anio)
        {
            List<Resolucion> list = new List<Resolucion>();

            string consulta = @"select
                                r.id,
                                r.codigo as nroExpedienteResolucion,
                                r.fechaAsignacion,
                                CONCAT(dc.nombre,' ',dc.apellido) as nombreApellido,
                                r.motivo,
                                dc.codigoEscalafon,
                                dc.observacion
                                from resolucion as r
                                inner join documento as dc on dc.id = r.idDocumento
                                where r.codigo like '%" + nroExpediente + "%' and year(r.fechaAsignacion) = '" + anio + "'";

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
                                Resolucion objResolucion = new Resolucion
                                {
                                    id = Utilitarios.ValidarInteger(dr["id"]),
                                    codigo = Utilitarios.ValidarStr(dr["nroExpedienteResolucion"]),
                                    fechaAsignacion = Utilitarios.ValidarDate(dr["fechaAsignacion"]).ToShortDateString(),
                                    nombreApellido = Utilitarios.ValidarStr(dr["nombreApellido"]),
                                    motivo = Utilitarios.ValidarStr(dr["motivo"]),
                                    codigoEscalafon = Utilitarios.ValidarStr(dr["codigoEscalafon"]),
                                    observacion = Utilitarios.ValidarStr(dr["observacion"]),

                                };
                                list.Add(objResolucion);
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
        public Resolucion ResolucionDetalle(int id)
        {
            Resolucion obj = new Resolucion();
            string consulta = @"select
                                r.id,
                                r.dni,
                                r.tituloProfesional,
                                r.especialidad,
                                r.establecimiento,
                                r.nivelMagisterial,
                                r.jornadaLaboral,
                                r.regimenPension,
                                r.nroIpss,
                                r.fechaIngreso,
                                r.fechaCese,
                                r.codigoEscalafon,
                                dc.anios,
                                dc.dias,
                                dc.meses,
                                dc.cargo,
                                dc.tipoServidor,
                                dc.otros
                                from resolucion as r
                                inner join documento as dc on dc.id = r.idDocumento
                                where r.id = " + id;
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
                                obj.id = Utilitarios.ValidarInteger(dr["id"]);
                                obj.dni = Utilitarios.ValidarStr(dr["dni"]);
                                obj.tituloProfesional = Utilitarios.ValidarStr(dr["tituloProfesional"]);
                                obj.especialidad = Utilitarios.ValidarStr(dr["especialidad"]);
                                obj.establecimiento = Utilitarios.ValidarStr(dr["establecimiento"]);
                                obj.nivelMagisterial = Utilitarios.ValidarStr(dr["nivelMagisterial"]);
                                obj.jornadaLaboral = Utilitarios.ValidarStr(dr["jornadaLaboral"]);
                                obj.regimenPension = Utilitarios.ValidarStr(dr["regimenPension"]);
                                obj.nroIpss = Utilitarios.ValidarStr(dr["nroIpss"]);
                                obj.fechaIngreso = Utilitarios.ValidarDate(dr["fechaIngreso"]).ToShortDateString();
                                obj.fechaCese = Utilitarios.ValidarDate(dr["fechaCese"]).ToShortDateString();
                                obj.codigoEscalafon = Utilitarios.ValidarStr(dr["codigoEscalafon"]);
                                obj.anios = Utilitarios.ValidarInteger(dr["anios"]);
                                obj.meses = Utilitarios.ValidarInteger(dr["meses"]);
                                obj.dias = Utilitarios.ValidarInteger(dr["dias"]);
                                obj.cargo = Utilitarios.ValidarStr(dr["cargo"]);
                                obj.tipoServidor = Utilitarios.ValidarStr(dr["tipoServidor"]);
                                obj.otros = Utilitarios.ValidarStr(dr["otros"]);
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
        public bool ResolucionRegistrar(Resolucion objResolucion)
        {
            bool respuesta = false;

            string consulta = @"INSERT INTO resolucion (
                                    idDocumento,                                    
                                    codigo,
                                    fechaAsignacion,
                                    motivo,
                                    dni,
                                    tituloProfesional,
                                    especialidad,
                                    establecimiento,
                                    nivelMagisterial,
                                    jornadaLaboral,
                                    regimenPension,
                                    nroIpss,
                                    fechaIngreso,
                                    fechaCese,
                                    codigoEscalafon)
                            VALUES (
                                    @p0,
                                    @p1,
                                    @p2,
                                    @p3,
                                    @p4,
                                    @p5,
                                    @p6,
                                    @p7,
                                    @p8,
                                    @p9,
                                    @p10,
                                    @p11,
                                    @p12,
                                    @p13,
                                    @p14)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(objResolucion.idDocumento));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(objResolucion.codigo));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(objResolucion.fechaAsignacion));
                    query.Parameters.AddWithValue("@p3", Utilitarios.ValidarStr(objResolucion.motivo));
                    query.Parameters.AddWithValue("@p4", Utilitarios.ValidarStr(objResolucion.dni));
                    query.Parameters.AddWithValue("@p5", Utilitarios.ValidarStr(objResolucion.tituloProfesional));
                    query.Parameters.AddWithValue("@p6", Utilitarios.ValidarStr(objResolucion.especialidad));
                    query.Parameters.AddWithValue("@p7", Utilitarios.ValidarStr(objResolucion.establecimiento));
                    query.Parameters.AddWithValue("@p8", Utilitarios.ValidarStr(objResolucion.nivelMagisterial));
                    query.Parameters.AddWithValue("@p9", Utilitarios.ValidarStr(objResolucion.jornadaLaboral));
                    query.Parameters.AddWithValue("@p10", Utilitarios.ValidarStr(objResolucion.regimenPension));
                    query.Parameters.AddWithValue("@p11", Utilitarios.ValidarStr(objResolucion.nroIpss));
                    query.Parameters.AddWithValue("@p12", Utilitarios.ValidarDate(objResolucion.fechaIngreso));
                    query.Parameters.AddWithValue("@p13", Utilitarios.ValidarDate(objResolucion.fechaCese));
                    query.Parameters.AddWithValue("@p14", Utilitarios.ValidarStr(objResolucion.codigoEscalafon));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }
            return respuesta;
        }
    }
}