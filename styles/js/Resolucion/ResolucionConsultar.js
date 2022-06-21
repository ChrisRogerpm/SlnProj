ExpedienteSeleccionado = null;
const Resolucion = function () {
    const eventos = () => {
        $(document).on('click', '#btnBuscar', function () {
            ListarResolucions();
        })
        $(document).on('ifChecked', '#table input:radio', function () {
            let id = $(this).val();
            ResolucionDetalle(id);
        });
        $(document).on('click', '#btnDerivarExpediente', function () {
            if (ExpedienteSeleccionado !== null) {
                $("#frmDerivar").submit();
                let dataform = $("#frmDerivar").serializeFormJSON();
                dataform = Object.assign(dataform, {
                    idResolucion: ExpedienteSeleccionado.id
                });
                if (_objetoForm_frmDerivar.valid()) {
                    DerivarExpediente({
                        data: dataform
                    });
                }
            } else {
                ShowAlert({
                    type: 'warning',
                    message: 'No se ha seleccionado ningun expediente'
                })
                return false;
            }
        });
    }
    const IniciarLibrerias = () => {
        $("#CbAnios").select2();
        CargarDataSelect({
            url: "TipoDerivacion/TipoDerivacionListarJson",
            idSelect: "#CbTipoDerivacion",
            dataId: "id",
            dataValor: "nombre",
            valorSelect: 1,
            withoutplaceholder: true
        });
        $('.Fecha').daterangepicker({
            singleDatePicker: true,
            locale: {
                format: 'YYYY-MM-DD',
                "daysOfWeek": [
                    "Dom",
                    "Lun",
                    "Mar",
                    "Mie",
                    "Jue",
                    "Vie",
                    "Sab"
                ],
            }
        });
    }
    const ResolucionDetalle = (id) => {
        CargarDataGET({
            url: "Resolucion/ResolucionDetalleJson",
            dataForm: {
                id: id
            },
            callBackSuccess: function (data) {
                console.log(data)
                //ExpedienteSeleccionado = data;
                //$("#nroResolucionDetalle").val(data.nroExpediente);
                //$("#fecha").val(data.fecha);
                //$("#apellido").val(data.apellido);
                //$("#nombre").val(data.nombre);
                //$("#establecimiento").val(data.establecimiento);
                //$("#motivo").val(data.motivo);
                //$("#usuario").val(data.nombreUsuario);
                $("#dni").val(data.dni);
                $("#tituloProfesional").val(data.tituloProfesional);
                $("#especialidad").val(data.especialidad);
                $("#establecimiento").val(data.establecimiento);
                $("#nivelMagisterial").val(data.nivelMagisterial);
                $("#jornadaLaboral").val(data.jornadaLaboral);
                $("#regimenPension").val(data.regimenPension);
                $("#nroIpss").val(data.nroIpss);
                $("#fechaIngreso").val(data.fechaIngreso);
                $("#fechaCese").val(data.fechaCese);
                $("#codigoEscalafon").val(data.codigoEscalafon);
                $("#anios").val(data.anios);
                $("#meses").val(data.meses);
                $("#dias").val(data.dias);
                $("#cargo").val(data.cargo);
                $("#tipoServidor").val(data.tipoServidor);
                $("#otros").val(data.otros);
            }
        });
    }

    const ListarResolucions = (obj) => {
        var defaults = {
            data: $('#frmFiltro').serializeFormJSON()
        };
        var opciones = $.extend({}, defaults, obj);
        ExpedienteSeleccionado = null;
        CargarTablaDatatable({
            uniform: true,
            ajaxUrl: "Resolucion/ResolucionListarConsultarJson",
            table: "#table",
            tableOrdering: false,
            ajaxDataSend: opciones.data,
            tablePaging: false,
            tableInfo: false,
            tableColumns: [
                {
                    data: null,
                    title: "OPCIONES",
                    class: "text-center",
                    "render": function (value) {
                        return `<div class="icheck-inline" style="margin-top: 5px;"><input type="radio" value="${value.id}" name="expediente" data-checkbox="icheckbox_square-blue"></div>`;

                    }
                },
                { data: "codigo", title: "NRO DE RESOLUCIÓN" },
                { data: "fechaAsignacion", title: "FECHA" },
                { data: "nombreApellido", title: "NOMBRE Y APELLIDO" },
                { data: "motivo", title: "MOTIVO" },
                { data: "codigoEscalafon", title: "CÓDIGO ESCALAFÓN" },
                { data: "observacion", title: "OBSERVACIÓN" },
            ],
            tabledrawCallback: function () {
                $(".icheck-inline").iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-red',
                    increaseArea: '25%'
                });
            }
        })
    }

    return {
        init: function () {
            IniciarLibrerias();
            eventos();
            ListarResolucions();
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    Resolucion.init();
});