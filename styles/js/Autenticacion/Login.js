const Documento = function () {
    const Eventos = () => {
        $(document).on('click', '#btnSesion', function () {
            const dataform = $("#frmLogin").serializeFormJSON();
            ValidarLogin({
                data: dataform
            });
        })
    }
    const ValidarLogin = (obj) => {
        let defaults = {
            data: obj.data
        };
        let opciones = $.extend({}, defaults, obj);
        $("#frmLogin").submit();
        if (_objetoForm_frmLogin.valid()) {
            EnviarDataPost({
                url: "Usuario/ValidarLoginJson",
                data: opciones.data,
                callBackSuccess: function () {
                    setTimeout(function () {
                        RefrescarVentana();
                    }, 1000)
                }
            });
        }
    }
    const IniciarLibrerias = () => {
    }
    const ValidarCampos = function () {
        ValidarFormulario({
            contenedor: '#frmLogin',
            nameVariable: 'frmLogin',
            rules: {
                usuario: { required: true },
                password: { required: true },
            },
            messages: {
                usuario: { required: 'El campo es requerido' },
                password: { required: 'El campo es requerido' },
            }
        });
    };
    return {
        init: function () {
            IniciarLibrerias();
            Eventos();
            ValidarCampos();
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    Documento.init();
});