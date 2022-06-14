ExpedienteSeleccionado = null;
const Documento = function () {
    const eventos = () => {
        $(document).on('click', '#btnBuscar', function () {
            ListarDocumentos();
        })
        $(document).on('ifChecked', '#table input:radio', function () {
            let id = $(this).val();
            ExpedienteDetalle(id);
        });
        $(document).on('click', '#btnDerivarExpediente', function () {
            if (ExpedienteSeleccionado !== null) {
                $("#frmDerivar").submit();
                let dataform = $("#frmDerivar").serializeFormJSON();
                dataform = Object.assign(dataform, {
                    idDocumento: ExpedienteSeleccionado.id
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
    const ExpedienteDetalle = (id) => {
        CargarDataGET({
            url: "Documento/ExpedienteDetalleJson",
            dataForm: {
                id: id
            },
            callBackSuccess: function (data) {
                ExpedienteSeleccionado = data;
                $("#nroExpedienteDetalle").val(data.nroExpediente);
                $("#fecha").val(data.fecha);
                $("#apellido").val(data.apellido);
                $("#nombre").val(data.nombre);
                $("#establecimiento").val(data.establecimiento);
                $("#motivo").val(data.motivo);
                $("#usuario").val(data.nombreUsuario);
            }
        });
    }
    const DerivarExpediente = (obj) => {
        var defaults = {
            data: null
        };
        var opciones = $.extend({}, defaults, obj);
        if (opciones.data != null) {
            EnviarDataPost({
                url: "Derivacion/DerivacionRegistrarJson",
                data: opciones.data,
                callBackSuccess: function () {
                    setTimeout(function () {
                        RefrescarVentana();
                    }, 1000)
                }
            });
        }
    }
    const ListarDocumentos = (obj) => {
        var defaults = {
            data: $('#frmFiltro').serializeFormJSON()
        };
        var opciones = $.extend({}, defaults, obj);
        ExpedienteSeleccionado = null;
        CargarTablaDatatable({
            uniform: true,
            ajaxUrl: "Documento/DocumentoFiltroListarJson",
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
                { data: "nroExpediente", title: "NRO DE EXPEDIENTE" },
                { data: "fecha", title: "FECHA" },
                { data: "apellido", title: "APELLIDO" },
                { data: "nombre", title: "NOMBRE" },
                { data: "motivo", title: "MOTIVO" },
                { data: "establecimiento", title: "ENVIADO POR" },
                { data: "codigoModular", title: "CODIGO MODULAR" },
                { data: "observacion", title: "OBSERVACIÓN" },
            ],
            tabledrawCallback: function () {
                $('.btnEditar').tooltip();
                $(".icheck-inline").iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-red',
                    increaseArea: '25%'
                });
            }
        })
    }
    const ValidarExpediente = function () {
        ValidarFormulario({
            contenedor: '#frmDerivar',
            nameVariable: 'frmDerivar',
            rules: {
                ListaidTipoDerivacion: { required: true },
                accionRealizar: { required: true },
                comentario: { required: true },
                documentoEmitido: { required: true },
                estadoExpediente: { required: true },
                fechaDerivacion: { required: true },
            },
            messages: {
                ListaidTipoDerivacion: { required: 'El campo es requerido' },
                accionRealizar: { required: 'El campo es requerido' },
                comentario: { required: 'El campo es requerido' },
                documentoEmitido: { required: 'El campo es requerido' },
                estadoExpediente: { required: 'El campo es requerido' },
                fechaDerivacion: { required: 'El campo es requerido' },
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