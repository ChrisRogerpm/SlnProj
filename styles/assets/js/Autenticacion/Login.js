var LoginView = function () {
    var _componentes = function () {
        $("input").keydown(function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $('#btnSesion').click();
            }
        });
        $(document).on('click', '#btnSesion', function () {
            $("#frmNuevo").submit();
            if (_objetoForm_frmNuevo.valid()) {
                let dataForm = $('#frmNuevo').serializeFormJSON();
                let url = basePath + "ValidarLoginJson";
                $.LoadingOverlay("show");
                axios({
                    method: 'post',
                    url: url,
                    data: dataForm
                }).then(function (response) {
                    var mensaje = response.data.mensaje;
                    var token = response.data.token;
                    if (response.data.respuesta) {
                        $("#container-login").hide();
                        localStorage.setItem('token', token);
                        swalInit({
                            title: mensaje,
                            text: 'Ingresando',
                            type: 'success',
                            allowOutsideClick: false
                        }).then(function (result) {
                            if (result.value) {
                                window.location.replace(basePath + response.data.redigirir);
                            }
                        });
                        setTimeout(function () {
                            window.location.replace(basePath + response.data.redigirir)
                        }, 1000);
                    } else {
                        ShowAlert({
                            message: mensaje,
                            type: "warning"
                        });
                    }
                }).finally(function () {
                    $.LoadingOverlay("hide");
                });
            }
        });
    };

    var _metodos = function () {
        ValidarFormulario({
            contenedor: '#frmNuevo',
            nameVariable: 'frmNuevo',
            rules: {
                Usuario: {
                    required: true
                },
                password: {
                    required: true
                },
            },
            messages: {
                Usuario: {
                    required: 'El campo Usuario es requerido'
                },
                password: {
                    required: 'El campo password es requerido'
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
    LoginView.init();
});



