UsuarioSeleccionado = null;
const Usuario = function () {
    const eventos = () => {
        $(document).on('click', '.btnEliminar', function () {
            let state = $(this);
            const id = state.data('id');
            const estado = state.data('estado') === 1 ? 0 : 1;
            UsuarioCambiarEstado(id, estado);
        })
        $(document).on('click', '.btnRestablecer', function () {
            let state = $(this);
            const id = state.data('id');
            const estado = state.data('estado') === 1 ? 0 : 1;
            UsuarioCambiarEstado(id, estado);
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
    const UsuarioCambiarEstado = (id, estado) => {
        const estadoNombre = estado === 1 ? 'ACTIVO' : 'INACTIVO'
        swalInit({
            title: `ESTA SEGURO DE PASAR A ${estadoNombre} AL USUARIO?`,
            text: `EL USUARIO SELECCIONADO ESTARA ${estadoNombre}!`,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'SI, GUARDAR!',
            cancelButtonText: 'NO, CANCELAR!',
            confirmButtonClass: 'btn bg-indigo',
            cancelButtonClass: 'btn btn-danger',
            allowOutsideClick: false,
            buttonsStyling: false
        }).then(function (result) {
            if (result.value) {
                EnviarDataPost({
                    url: 'Usuario/UsuarioCambiarEstadoJson',
                    data: {
                        id: id,
                        estado: estado
                    },
                    callBackSuccess: function () {
                        ListarUsuarios();
                    }
                })
            }
        });
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
                        let editar = `<button 
                                        class="btn bg-success btn-xs btnEditar"
                                        data-popup="tooltip"
                                        title="Editar"
                                        data-placement="top"
                                        data-id="${value.id}"
                                        data-estado="${value.estado}">
                                        <i class="icon icon-pencil"></i></button>`;
                        let eliminar = `<button
                                        class="btn bg-danger btn-xs btnEliminar"
                                        data-popup="tooltip"
                                        title="Eliminar"
                                        data-placement="top"
                                        data-id="${value.id}"
                                        data-estado="${value.estado}">
                                        <i class="icon icon-trash"></i></button>`
                        let restablecer = `<button class="btn bg-dark btn-xs btnRestablecer" data-popup="tooltip" title="Restablecer" data-placement="top" data-id="${value.id}"><i class="icon icon-cog"></i></button>`;
                        if (value.estado === 1) {
                            restablecer = ``;
                        } else {
                            eliminar = ``;
                        }
                        return `${editar} ${eliminar} ${restablecer}`;
                    }
                },
            ],
            tabledrawCallback: function () {
                $('.btnEditar').tooltip();
                $('.btnEliminar').tooltip();
                $('.btnRestablecer').tooltip();
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