UsuarioSeleccionado = null;
const Usuario = function () {
    const eventos = () => {
        $(document).on('click', '#btnGuardar', function () {

        })
        $(document).on('click', '.btnEditar', function () {

        })
        $(document).on('click', '#btnNuevo', function () {
            LimpiarFormulario({
                formulario: "#frmUsuario"
            });
            _objetoForm_frmUsuario.resetForm();
            $("#id").val("");
            $("#ModalUsuario").modal();
        })
        $(document).on('click', '.btnEditar', function () {
            const id = $(this).data('id');
            CargarDataGET({
                url: "Usuario/UsuarioDetalleJson",
                dataForm: {
                    id: id
                },
                callBackSuccess: function (data) {
                    $("#id").val(data.id);
                    $("#idTipoUsuario").val(data.idTipoUsuario);
                    $("#nombreCompleto").val(data.nombreCompleto);
                    $("#password").val(data.password);
                    $("#usuario").val(data.usuario);
                    $("#ModalUsuario").modal();
                }
            });
        })
        $(document).on('click', '#btnGuardarUsuario', function () {
            const id = $("#id").val();
            if (id === "") {
                GuardarUsuario();
            } else {
                GuardarUsuario({
                    nuevo: false
                });
            }
        })
    }
    const IniciarLibrerias = () => {
    }
    const GuardarUsuario = (obj) => {
        var defaults = {
            nuevo: true
        };
        var opciones = $.extend({}, defaults, obj);
        $("#frmUsuario").submit();
        let dataform = $("#frmUsuario").serializeFormJSON();        
        if (_objetoForm_frmUsuario.valid()) {
            if (opciones.nuevo) {
                EnviarDataPost({
                    url: "Usuario/UsuarioRegistrarJson",
                    data: dataform,
                    callBackSuccess: function () {
                        setTimeout(function () {
                            ListarUsuarios();
                            $("#ModalUsuario").modal("hide");
                        }, 1000)
                    }
                });
            } else {
                const id = $("#id").val();
                dataform = Object.assign(dataform, { id: id });
                EnviarDataPost({
                    url: "Usuario/UsuarioEditarJson",
                    data: dataform,
                    callBackSuccess: function () {
                        setTimeout(function () {
                            ListarUsuarios();
                            $("#ModalUsuario").modal("hide");
                        }, 1000)
                    }
                });
            }
        }
    }
    const ListarUsuarios = (obj) => {
        var defaults = {
            data: $('#frmFiltro').serializeFormJSON()
        };
        var opciones = $.extend({}, defaults, obj);
        UsuarioSeleccionado = null;
        CargarTablaDatatable({
            uniform: true,
            ajaxUrl: "Usuario/UsuarioListarJson",
            table: "#table",
            tableOrdering: false,
            ajaxDataSend: opciones.data,
            tablePaging: false,
            tableInfo: false,
            tableColumns: [
                { data: "tipoUsuarioNombre", title: "TIPO DE USUARIO" },
                { data: "nombreCompleto", title: "NOMBRE COMPLETO" },
                { data: "usuario", title: "USUARIO" },
                { data: "estadoNombre", title: "ESTADO" },
                {
                    data: null,
                    title: "OPCIONES",
                    class: "text-center",
                    "render": function (value) {
                        return `<button class="btn bg-success btn-xs btnEditar" data-popup="tooltip" title="Editar" data-placement="top" data-id="${value.id}"><i class="icon icon-pencil"></i></button>`;

                    }
                },
            ],
            tabledrawCallback: function () {
            }
        })
    }
    const ValidarUsuario = function () {
        ValidarFormulario({
            contenedor: '#frmUsuario',
            nameVariable: 'frmUsuario',
            rules: {
                idTipoUsuario: { required: true },
                nombreCompleto: { required: true },
                password: { required: true },
                usuario: { required: true },
            },
            messages: {
                idTipoUsuario: { required: 'El campo es requerido' },
                nombreCompleto: { required: 'El campo es requerido' },
                password: { required: 'El campo es requerido' },
                usuario: { required: 'El campo es requerido' },
            }
        });
    };
    return {
        init: function () {
            IniciarLibrerias();
            eventos();
            ListarUsuarios();
            ValidarUsuario();
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    Usuario.init();
});