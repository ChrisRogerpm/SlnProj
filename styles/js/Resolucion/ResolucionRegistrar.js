ExpedienteSeleccionado = null;
const Resolucion = function () {
    const Eventos = () => {
        $(document).on('click', '#btnBuscar', function () {
            ExpedienteDetalle();
        })
        $(document).on('click', '#btnRegistrarResolucion', function () {
            if (ExpedienteSeleccionado !== null) {
                $("#frmResolucion").submit();
                let dataform = $("#frmResolucion").serializeFormJSON();
                const codigoCustom = `${dataform.codigo} - ${dataform.codigoAnio}`
                dataform = Object.assign(dataform, {
                    idDocumento: ExpedienteSeleccionado.idDocumento,
                    codigo: codigoCustom
                });
                if (_objetoForm_frmResolucion.valid()) {
                    ResolucionRegistrar({
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
    const ExpedienteDetalle = () => {
        CargarDataGET({
            url: "Derivacion/DerivacionDetalleJson",
            dataForm: $("#frmFiltro").serializeFormJSON(),
            callBackSuccess: function (data) {
                ExpedienteSeleccionado = data;                
                $("#fechaExpediente").val(data.fechaDerivacion)
                $("#nroDerivacionExpediente").val(data.nroDerivacion)
                $("#codigoEscalafonExpediente").val(data.documento.codigoEscalafon)
                $("#nombreApellidoExpediente").val(data.nombreApellido)
                $("#motivoExpediente").val(data.documento.motivo)
                $("#establecimientoExpediente").val(data.documento.establecimiento)
                $("#observacionExpediente").val(data.documento.observacion)

                $("#motivo").val(data.documento.motivo);
                $("#dni").val(data.documento.dni);
                $("#tituloProfesional").val(data.documento.tituloProfesional);
                $("#especialidad").val(data.documento.especialidad);
                $("#establecimiento").val(data.documento.establecimiento);
                $("#nivelMagisterial").val(data.documento.nivelMagisterial);
                $("#jornadaLaboral").val(data.documento.jornadaLaboral);
                $("#regimenPension").val(data.documento.regimenPension);
                $("#nroIpss").val(data.documento.nroIpss);
                $("#codigoEscalafon").val(data.documento.codigoEscalafon);

            }
        });
    }
    const ResolucionRegistrar = (obj) => {
        var defaults = {
            data: null
        };
        var opciones = $.extend({}, defaults, obj);
        if (opciones.data != null) {
            EnviarDataPost({
                url: "Resolucion/ResolucionRegistrarJson",
                data: opciones.data,
                callBackSuccess: function () {
                    setTimeout(function () {
                        RefrescarVentana();
                    }, 1000)
                }
            });
        }
    }
    const ValidarExpediente = function () {
        ValidarFormulario({
            contenedor: '#frmResolucion',
            nameVariable: 'frmResolucion',
            rules: {
                codigo: { required: true },
                codigoAnio: { required: true },
                codigoEscalafon: { required: true },
                dni: { required: true },
                especialidad: { required: true },
                establecimiento: { required: true },
                fechaAsignacion: { required: true },
                fechaCese: { required: true },
                fechaIngreso: { required: true },
                jornadaLaboral: { required: true },
                motivo: { required: true },
                nivelMagisterial: { required: true },
                nrpIpss: { required: true },
                regimenPension: { required: true },
                tituloProfesional: { required: true },
            },
            messages: {
                codigo: { required: 'EL campo es requerido' },
                codigoAnio: { required: 'EL campo es requerido' },
                codigoEscalafon: { required: 'EL campo es requerido' },
                dni: { required: 'EL campo es requerido' },
                especialidad: { required: 'EL campo es requerido' },
                establecimiento: { required: 'EL campo es requerido' },
                fechaAsignacion: { required: 'EL campo es requerido' },
                fechaCese: { required: 'EL campo es requerido' },
                fechaIngreso: { required: 'EL campo es requerido' },
                jornadaLaboral: { required: 'EL campo es requerido' },
                motivo: { required: 'EL campo es requerido' },
                nivelMagisterial: { required: 'EL campo es requerido' },
                nrpIpss: { required: 'EL campo es requerido' },
                regimenPension: { required: 'EL campo es requerido' },
                tituloProfesional: { required: 'EL campo es requerido' },
            }
        });
    };
    return {
        init: function () {
            IniciarLibrerias();
            Eventos();
            ValidarExpediente();
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    Resolucion.init();
});