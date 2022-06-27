using Project.Funciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Documento
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string nroExpediente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string codigoModular { get; set; }
        public string motivo { get; set; }
        public string observacion { get; set; }
        public string fecha { get; set; }
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
        public int anios { get; set; }
        public int meses { get; set; }
        public int dias { get; set; }
        public string cargo { get; set; }
        public string tipoServidor { get; set; }
        public string otros { get; set; }

        string _conexion = string.Empty;

        public Documento()
        {
            _conexion = ConfigurationManager.ConnectionStrings["ModeloDatos"].ConnectionString;
        }
        public List<Documento> DocumentoListar()
        {
            List<Documento> list = new List<Documento>();

            string consulta = @"select * from documento";

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
                                Documento objDocumento = new Documento
                                {
                                    id = Utilitarios.ValidarInteger(dr["id"]),
                                    nroExpediente = Utilitarios.ValidarStr(dr["nroExpediente"]),
                                    nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                    apellido = Utilitarios.ValidarStr(dr["apellido"]),
                                    codigoModular = Utilitarios.ValidarStr(dr["codigoModular"]),
                                    motivo = Utilitarios.ValidarStr(dr["motivo"]),
                                    observacion = Utilitarios.ValidarStr(dr["observacion"]),
                                    fecha = Utilitarios.ValidarDate(dr["fecha"]).ToShortDateString(),
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
                                    anios = Utilitarios.ValidarInteger(dr["anios"]),
                                    meses = Utilitarios.ValidarInteger(dr["meses"]),
                                    dias = Utilitarios.ValidarInteger(dr["dias"]),
                                    cargo = Utilitarios.ValidarStr(dr["cargo"]),
                                    tipoServidor = Utilitarios.ValidarStr(dr["tipoServidor"]),
                                    otros = Utilitarios.ValidarStr(dr["otros"]),

                                };
                                list.Add(objDocumento);
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
        public List<Documento> DocumentoFiltroListar(string nroExpediente, string anio)
        {
            List<Documento> list = new List<Documento>();

            string consulta = @"select
                                d.*                                
                                from documento as d                                 
                                where year(d.fecha) = @p0 and d.nroExpediente like " + "'%" + nroExpediente + "%'";

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(anio));
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Documento objDocumento = new Documento
                                {
                                    id = Utilitarios.ValidarInteger(dr["id"]),
                                    nroExpediente = Utilitarios.ValidarStr(dr["nroExpediente"]),
                                    nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                    apellido = Utilitarios.ValidarStr(dr["apellido"]),
                                    codigoModular = Utilitarios.ValidarStr(dr["codigoModular"]),
                                    motivo = Utilitarios.ValidarStr(dr["motivo"]),
                                    observacion = Utilitarios.ValidarStr(dr["observacion"]),
                                    fecha = Utilitarios.ValidarDate(dr["fecha"]).ToShortDateString(),
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
                                    anios = Utilitarios.ValidarInteger(dr["anios"]),
                                    meses = Utilitarios.ValidarInteger(dr["meses"]),
                                    dias = Utilitarios.ValidarInteger(dr["dias"]),
                                    cargo = Utilitarios.ValidarStr(dr["cargo"]),
                                    tipoServidor = Utilitarios.ValidarStr(dr["tipoServidor"]),
                                    otros = Utilitarios.ValidarStr(dr["otros"]),

                                };
                                list.Add(objDocumento);
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

        public List<Documento> DocumentoDerivadoListar(string nroExpediente, string anio)
        {
            List<Documento> list = new List<Documento>();

            string consulta = @"select
                                d.*                                
                                from documento as d                                 
                                where 
                                YEAR(d.fecha) = @p0 AND 
                                d.nroExpediente like " + "'%" + nroExpediente + "%' " +
                                "d.id not in (select dr.idDocumento from derivacion as dr group by dr.idDocumento)";
            ;

            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(anio));
                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Documento objDocumento = new Documento
                                {
                                    id = Utilitarios.ValidarInteger(dr["id"]),
                                    nroExpediente = Utilitarios.ValidarStr(dr["nroExpediente"]),
                                    nombre = Utilitarios.ValidarStr(dr["nombre"]),
                                    apellido = Utilitarios.ValidarStr(dr["apellido"]),
                                    codigoModular = Utilitarios.ValidarStr(dr["codigoModular"]),
                                    motivo = Utilitarios.ValidarStr(dr["motivo"]),
                                    observacion = Utilitarios.ValidarStr(dr["observacion"]),
                                    fecha = Utilitarios.ValidarDate(dr["fecha"]).ToShortDateString(),
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
                                    anios = Utilitarios.ValidarInteger(dr["anios"]),
                                    meses = Utilitarios.ValidarInteger(dr["meses"]),
                                    dias = Utilitarios.ValidarInteger(dr["dias"]),
                                    cargo = Utilitarios.ValidarStr(dr["cargo"]),
                                    tipoServidor = Utilitarios.ValidarStr(dr["tipoServidor"]),
                                    otros = Utilitarios.ValidarStr(dr["otros"]),

                                };
                                list.Add(objDocumento);
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

        public Documento DocumentoDetalle(int id)
        {
            Documento obj = new Documento();
            string consulta = @"select 
                                d.*,
                                u.nombreCompleto 
                                from documento as d 
                                inner join usuario as u on u.id = d.idUsuario
                                where d.id = @p0";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarInteger(id));

                    using (var dr = query.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                obj.id = Utilitarios.ValidarInteger(dr["id"]);
                                obj.nombreUsuario = Utilitarios.ValidarStr(dr["nombreCompleto"]);
                                obj.nroExpediente = Utilitarios.ValidarStr(dr["nroExpediente"]);
                                obj.nombre = Utilitarios.ValidarStr(dr["nombre"]);
                                obj.apellido = Utilitarios.ValidarStr(dr["apellido"]);
                                obj.codigoModular = Utilitarios.ValidarStr(dr["codigoModular"]);
                                obj.motivo = Utilitarios.ValidarStr(dr["motivo"]);
                                obj.observacion = Utilitarios.ValidarStr(dr["observacion"]);
                                obj.fecha = Utilitarios.ValidarDate(dr["fecha"]).ToShortDateString();
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
        public Documento DocumentoExpedienteDetalle(string nroExpediente, string anio)
        {
            Documento obj = new Documento();
            string consulta = @"select 
                                top 1
                                d.*,
                                u.nombreCompleto 
                                from documento as d 
                                inner join usuario as u on u.id = d.idUsuario
                                where d.nroExpediente = '" + nroExpediente + "' and year(d.fecha) = " + anio;
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
                                obj.nombreUsuario = Utilitarios.ValidarStr(dr["nombreCompleto"]);
                                obj.nroExpediente = Utilitarios.ValidarStr(dr["nroExpediente"]);
                                obj.nombre = Utilitarios.ValidarStr(dr["nombre"]);
                                obj.apellido = Utilitarios.ValidarStr(dr["apellido"]);
                                obj.codigoModular = Utilitarios.ValidarStr(dr["codigoModular"]);
                                obj.motivo = Utilitarios.ValidarStr(dr["motivo"]);
                                obj.observacion = Utilitarios.ValidarStr(dr["observacion"]);
                                obj.fecha = Utilitarios.ValidarDate(dr["fecha"]).ToShortDateString();
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
        public bool DocumentoRegistrar(Documento objDetalle)
        {
            bool respuesta = false;

            string consulta = @"INSERT INTO documento (
                                    idUsuario,                                    
                                    nroExpediente,
                                    nombre,
                                    apellido,
                                    codigoModular,
                                    motivo,
                                    observacion,
                                    fecha,
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
                                    codigoEscalafon,
                                    anios,
                                    meses,
                                    dias,
                                    cargo,
                                    tipoServidor,
                                    otros)
                            VALUES (
                                    @p24,                                    
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
                                    @p14,
                                    @p15,
                                    @p16,
                                    @p17,
                                    @p18,
                                    @p19,
                                    @p20,
                                    @p21,
                                    @p22,
                                    @p23)";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(objDetalle.nroExpediente));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(objDetalle.nombre));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(objDetalle.apellido));
                    query.Parameters.AddWithValue("@p3", Utilitarios.ValidarStr(objDetalle.codigoModular));
                    query.Parameters.AddWithValue("@p4", Utilitarios.ValidarStr(objDetalle.motivo));
                    query.Parameters.AddWithValue("@p5", Utilitarios.ValidarStr(objDetalle.observacion));
                    query.Parameters.AddWithValue("@p6", Utilitarios.ValidarDate(objDetalle.fecha));
                    query.Parameters.AddWithValue("@p7", Utilitarios.ValidarStr(objDetalle.dni));
                    query.Parameters.AddWithValue("@p8", Utilitarios.ValidarStr(objDetalle.tituloProfesional));
                    query.Parameters.AddWithValue("@p9", Utilitarios.ValidarStr(objDetalle.especialidad));
                    query.Parameters.AddWithValue("@p10", Utilitarios.ValidarStr(objDetalle.establecimiento));
                    query.Parameters.AddWithValue("@p11", Utilitarios.ValidarStr(objDetalle.nivelMagisterial));
                    query.Parameters.AddWithValue("@p12", Utilitarios.ValidarStr(objDetalle.jornadaLaboral));
                    query.Parameters.AddWithValue("@p13", Utilitarios.ValidarStr(objDetalle.regimenPension));
                    query.Parameters.AddWithValue("@p14", Utilitarios.ValidarStr(objDetalle.nroIpss));
                    query.Parameters.AddWithValue("@p15", Utilitarios.ValidarDate(objDetalle.fechaIngreso));
                    query.Parameters.AddWithValue("@p16", Utilitarios.ValidarDate(objDetalle.fechaCese));
                    query.Parameters.AddWithValue("@p17", Utilitarios.ValidarStr(objDetalle.codigoEscalafon));
                    query.Parameters.AddWithValue("@p18", Utilitarios.ValidarInteger(objDetalle.anios));
                    query.Parameters.AddWithValue("@p19", Utilitarios.ValidarInteger(objDetalle.meses));
                    query.Parameters.AddWithValue("@p20", Utilitarios.ValidarInteger(objDetalle.dias));
                    query.Parameters.AddWithValue("@p21", Utilitarios.ValidarStr(objDetalle.cargo));
                    query.Parameters.AddWithValue("@p22", Utilitarios.ValidarStr(objDetalle.tipoServidor));
                    query.Parameters.AddWithValue("@p23", Utilitarios.ValidarStr(objDetalle.otros));
                    query.Parameters.AddWithValue("@p24", Utilitarios.ValidarInteger(objDetalle.idUsuario));
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
            }
            return respuesta;
        }

        public bool DocumentoActualizar(Documento objDetalle)
        {
            bool respuesta = false;

            string consulta = @"UPDATE documento
                                SET
                                nroExpediente = @p0,
                                nombre = @p1,
                                apellido = @p2,
                                codigoModular = @p3,
                                motivo = @p4,
                                observacion = @p5,
                                fecha = @p6,
                                dni = @p7,
                                tituloProfesional = @p8,
                                especialidad = @p9,
                                establecimiento = @p10,
                                nivelMagisterial = @p11,
                                jornadaLaboral = @p12,
                                regimenPension = @p13,
                                nroIpss = @p14,
                                fechaIngreso = @p15,
                                fechaCese = @p16,
                                codigoEscalafon = @p17,
                                anios = @p18,
                                meses = @p19,
                                dias = @p20,
                                cargo = @p21,
                                tipoServidor = @p22,
                                otros = @p23
                                WHERE id = @p24";
            try
            {
                using (var con = new SqlConnection(_conexion))
                {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@p0", Utilitarios.ValidarStr(objDetalle.nroExpediente));
                    query.Parameters.AddWithValue("@p1", Utilitarios.ValidarStr(objDetalle.nombre));
                    query.Parameters.AddWithValue("@p2", Utilitarios.ValidarStr(objDetalle.apellido));
                    query.Parameters.AddWithValue("@p3", Utilitarios.ValidarStr(objDetalle.codigoModular));
                    query.Parameters.AddWithValue("@p4", Utilitarios.ValidarStr(objDetalle.motivo));
                    query.Parameters.AddWithValue("@p5", Utilitarios.ValidarStr(objDetalle.observacion));
                    query.Parameters.AddWithValue("@p6", Utilitarios.ValidarDate(objDetalle.fecha));
                    query.Parameters.AddWithValue("@p7", Utilitarios.ValidarStr(objDetalle.dni));
                    query.Parameters.AddWithValue("@p8", Utilitarios.ValidarStr(objDetalle.tituloProfesional));
                    query.Parameters.AddWithValue("@p9", Utilitarios.ValidarStr(objDetalle.especialidad));
                    query.Parameters.AddWithValue("@p10", Utilitarios.ValidarStr(objDetalle.establecimiento));
                    query.Parameters.AddWithValue("@p11", Utilitarios.ValidarStr(objDetalle.nivelMagisterial));
                    query.Parameters.AddWithValue("@p12", Utilitarios.ValidarStr(objDetalle.jornadaLaboral));
                    query.Parameters.AddWithValue("@p13", Utilitarios.ValidarStr(objDetalle.regimenPension));
                    query.Parameters.AddWithValue("@p14", Utilitarios.ValidarStr(objDetalle.nroIpss));
                    query.Parameters.AddWithValue("@p15", Utilitarios.ValidarDate(objDetalle.fechaIngreso));
                    query.Parameters.AddWithValue("@p16", Utilitarios.ValidarDate(objDetalle.fechaCese));
                    query.Parameters.AddWithValue("@p17", Utilitarios.ValidarStr(objDetalle.codigoEscalafon));
                    query.Parameters.AddWithValue("@p18", Utilitarios.ValidarInteger(objDetalle.anios));
                    query.Parameters.AddWithValue("@p19", Utilitarios.ValidarInteger(objDetalle.meses));
                    query.Parameters.AddWithValue("@p20", Utilitarios.ValidarInteger(objDetalle.dias));
                    query.Parameters.AddWithValue("@p21", Utilitarios.ValidarStr(objDetalle.cargo));
                    query.Parameters.AddWithValue("@p22", Utilitarios.ValidarStr(objDetalle.tipoServidor));
                    query.Parameters.AddWithValue("@p23", Utilitarios.ValidarStr(objDetalle.otros));
                    query.Parameters.AddWithValue("@p24", Utilitarios.ValidarStr(objDetalle.id));
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