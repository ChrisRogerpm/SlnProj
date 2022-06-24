const Documento = function () {
    const eventos = () => {
        $(document).on('click', '#btnModalExpediente', function () {
            LimpiarFormulario({
                formulario: "#frmExpediente"
            });
            _objetoForm_frmExpediente.resetForm();
            $("#id").val("");
            $("#ModalExpediente").modal();
            $('.Fecha').daterangepicker({
                singleDatePicker: true,
                locale: {
                    format: 'DD/MM/YYYY',
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
        })
        $(document).on('click', '.btnEditar', function () {
            const id = $(this).data('id');
            CargarDataGET({
                url: "Documento/ExpedienteDetalleJson",
                dataForm: {
                    id: id
                },
                callBackSuccess: function (data) {
                    $("#id").val(data.id);
                    $("#anios").val(data.anios);
                    $("#apellido").val(data.apellido);
                    $("#cargo").val(data.cargo);
                    $("#codigoEscalafon").val(data.codigoEscalafon);
                    $("#codigoModular").val(data.codigoModular);
                    $("#dias").val(data.dias);
                    $("#dni").val(data.dni);
                    $("#especialidad").val(data.especialidad);
                    $("#establecimiento").val(data.establecimiento);
                    $("#fecha").val(data.fecha);
                    $("#fechaCese").val(data.fechaCese);
                    $("#fechaIngreso").val(data.fechaIngreso);
                    $("#id").val(data.id);
                    $("#jornadaLaboral").val(data.jornadaLaboral);
                    $("#meses").val(data.meses);
                    $("#motivo").val(data.motivo);
                    $("#nivelMagisterial").val(data.nivelMagisterial);
                    $("#nombre").val(data.nombre);
                    $("#nroExpediente").val(data.nroExpediente);
                    $("#nroIpss").val(data.nroIpss);
                    $("#observacion").val(data.observacion);
                    $("#otros").val(data.otros);
                    $("#regimenPension").val(data.regimenPension);
                    $("#tipoServidor").val(data.tipoServidor);
                    $("#tituloProfesional").val(data.tituloProfesional);
                    $("#ModalExpediente").modal();
                }
            });
        })
        $(document).on('click', '#btnGuardarExpediente', function () {
            const id = $("#id").val();
            if (id === "") {
                GuardarExpediente();
            } else {
                GuardarExpediente({
                    nuevo: false
                });
            }
        })
        $(document).on('click', '#btnBuscar', function () {
            ListarDocumentos();
        })
    }
    const IniciarLibrerias = () => {
        $("#CbAnios").select2();
        $('.Fecha').daterangepicker({
            singleDatePicker: true,
            locale: {
                format: 'DD/MM/YYYY',
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
    const GuardarExpediente = (obj) => {
        var defaults = {
            nuevo: true
        };
        var opciones = $.extend({}, defaults, obj);
        $("#frmExpediente").submit();
        let dataform = $("#frmExpediente").serializeFormJSON();
        dataform = Object.assign(dataform, {
            idUsuario: customData.id
        });
        if (_objetoForm_frmExpediente.valid()) {
            if (opciones.nuevo) {
                EnviarDataPost({
                    url: "Documento/ExpedienteRegistrarJson",
                    data: dataform,
                    callBackSuccess: function () {
                        setTimeout(function () {
                            ListarDocumentos();
                            $("#ModalExpediente").modal("hide");
                        }, 1000)
                    }
                });
            } else {
                const id = $("#id").val();
                dataform = Object.assign(dataform, { id: id });
                EnviarDataPost({
                    url: "Documento/ExpedienteActualizarJson",
                    data: dataform,
                    callBackSuccess: function () {
                        setTimeout(function () {
                            ListarDocumentos();
                            $("#ModalExpediente").modal("hide");
                        }, 1000)
                    }
                });
            }
        }
    }
    const ListarDocumentos = (obj) => {
        var defaults = {
            data: $('#frmFiltro').serializeFormJSON()
        };
        var opciones = $.extend({}, defaults, obj);
        CargarTablaDatatable({
            uniform: true,
            ajaxUrl: "Documento/DocumentoFiltroListarJson",
            table: "#table",
            tableOrdering: false,
            ajaxDataSend: opciones.data,
            tableColumns: [
                { data: "nroExpediente", title: "NRO DE EXPEDIENTE" },
                { data: "fecha", title: "FECHA" },
                { data: "apellido", title: "APELLIDO" },
                { data: "nombre", title: "NOMBRE" },
                { data: "motivo", title: "MOTIVO" },
                { data: "establecimiento", title: "ENVIADO POR" },
                { data: "codigoModular", title: "CODIGO MODULAR" },
                { data: "observacion", title: "OBSERVACIÓN" },
                {
                    data: null,
                    title: "OPCIONES",
                    class: "text-center",
                    "render": function (value) {
                        if (customData.idTipoUsuario === 2) {
                            return `<button class="btn bg-success btn-xs btnEditar" data-popup="tooltip" title="Editar" data-placement="top" data-id="${value.id}"><i class="icon icon-pencil"></i></button>`;
                        }
                        else {
                            return ''
                        }


                    }
                },
            ],
            tabledrawCallback: function () {
                $('.btnEditar').tooltip();
            }
        })
    }
    const ValidarExpediente = function () {
        ValidarFormulario({
            contenedor: '#frmExpediente',
            nameVariable: 'frmExpediente',
            rules: {
                // anios: { required: true },
                // apellido: { required: true },
                // cargo: { required: true },
                // codigoEscalafon: { required: true },
                // codigoModular: { required: true },
                // dias: { required: true },
                // dni: { required: true },
                // especialidad: { required: true },
                // establecimiento: { required: true },
                // fecha: { required: true },
                // fechaCese: { required: true },
                // fechaIngreso: { required: true },
                // id: { required: true },
                // jornadaLaboral: { required: true },
                // meses: { required: true },
                // motivo: { required: true },
                // nivelMagisterial: { required: true },
                // nombre: { required: true },
                // nroExpediente: { required: true },
                // nroIpss: { required: true },
                // observacion: { required: true },
                // otros: { required: true },
                // regimenPension: { required: true },
                // tipoServidor: { required: true },
                // tituloProfesional: { required: true },
            },
            messages: {
                // anios: { required: 'El campo es requerido' },
                // apellido: { required: 'El campo es requerido' },
                // cargo: { required: 'El campo es requerido' },
                // codigoEscalafon: { required: 'El campo es requerido' },
                // codigoModular: { required: 'El campo es requerido' },
                // dias: { required: 'El campo es requerido' },
                // dni: { required: 'El campo es requerido' },
                // especialidad: { required: 'El campo es requerido' },
                // establecimiento: { required: 'El campo es requerido' },
                // fecha: { required: 'El campo es requerido' },
                // fechaCese: { required: 'El campo es requerido' },
                // fechaIngreso: { required: 'El campo es requerido' },
                // id: { required: 'El campo es requerido' },
                // jornadaLaboral: { required: 'El campo es requerido' },
                // meses: { required: 'El campo es requerido' },
                // motivo: { required: 'El campo es requerido' },
                // nivelMagisterial: { required: 'El campo es requerido' },
                // nombre: { required: 'El campo es requerido' },
                // nroExpediente: { required: 'El campo es requerido' },
                // nroIpss: { required: 'El campo es requerido' },
                // observacion: { required: 'El campo es requerido' },
                // otros: { required: 'El campo es requerido' },
                // regimenPension: { required: 'El campo es requerido' },
                // tipoServidor: { required: 'El campo es requerido' },
                // tituloProfesional: { required: 'El campo es requerido' },
            }
        });
    };
    return {
        init: function () {
            IniciarLibrerias();
            eventos();
            ListarDocumentos();
            ValidarExpediente();
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    Documento.init();
});