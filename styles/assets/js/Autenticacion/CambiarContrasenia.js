var CambiarContraseniaView = function () {
    var _componentes = function () {
        $(document).on('click', '.btnGuardar', function () {
            $("#frmNuevo").submit();
            if (_objetoForm_frmNuevo.valid()) {
                var dataForm = $('#frmNuevo').serializeFormJSON();
                EnviarDataPost({
                    url: 'CambiarContraseniaJson',
                    data: dataForm,
                    callBackSuccess:function () {
                        RedirigirUrl("Inicio");
                    }
                })
            }
        });
    };

    var _metodos = function () {
        ValidarFormulario({
            contenedor: '#frmNuevo',
            nameVariable: 'frmNuevo',
            rules: {
                password: {
                    required: true,
                },
                NuevaContrasenia: {
                    required: true,
                },
                VerificarContrasenia: {
                    required: true,
                },
            },
            messages: {
                password: {
                    required: 'El campo password es requerido',
                },
                NuevaContrasenia: {
                    required: 'El campo Nueva Contrasenia es requerido',
                },
                VerificarContrasenia: {
                    required: 'El campo Verificar Contrasenia es requerido',
                },
            }
        });

    };

    return {
        init: function () {
            _componentes();
            _metodos();
        }
    }


}();

document.addEventListener('DOMContentLoaded', function () {
    CambiarContraseniaView.init();
});



