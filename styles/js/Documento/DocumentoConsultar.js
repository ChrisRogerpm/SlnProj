ExpedienteSeleccionado = null;
const Documento = function () {
    const eventos = () => {
        $(document).on('click', '#btnBuscar', function () {
            DerivacionDetalle();
        })
        $(document).on('click', '#btnRelaciones', function () {
            if (ExpedienteSeleccionado === null) {
                ShowAlert({
                    type: 'warning',
                    message: 'No se ha seleccionado ningun expediente'
                });
                return false;
            } else {
                //console.log(ExpedienteSeleccionado.listaDerivacion)
                ListaRelaciones(ExpedienteSeleccionado.listaDerivacion);
                $("#ModalRelaciones").modal();
            }
        })
    }
    const IniciarLibrerias = () => {
        $("#CbAnios").select2();
    }

    const ListaRelaciones = (obj) => {
        const contain_relations = $("#contain-relations");
        contain_relations.html("");
        const list = obj;
        let items = "";
        list.map(ele => {
            items += ` <div class="list-feed-item">
                        <a href="#">RELACIÓN:</a> ${ele.derivadoA} - ${ele.estadoExpedienteNombre}
                    </div>`;
        });
        contain_relations.append(`<div class="col-sm-12">
                    <div class="card card-body border-top-warning">
                        <div class="list-feed">
                            ${items}
                        </div>
                    </div>
                </div>`);

    }

    const ListaHistorial = (obj) => {
        return `<div class="col-sm-4">
                    <div class="card card-body border-top-warning">
                        <div class="list-feed">
                            <div class="list-feed-item">
                                <a href="#">EXPEDIENTE:</a> ${obj.nroExpediente}
                            </div>
                            <div class="list-feed-item">
                                <a href="#">CREADO POR:</a> ${obj.creadorPor}
                            </div>
                            <div class="list-feed-item">
                                <a href="#">DERIVADO A:</a> ${obj.derivadoA}
                            </div>
                            <div class="list-feed-item">
                                <a href="#">COMENTARIO:</a> ${obj.comentario}
                            </div>
                            <div class="list-feed-item">
                                <a href="#">FECHA DE CREACIÓN:</a> ${obj.fechaCreacion}
                            </div>
                            <div class="list-feed-item">
                                <a href="#">FECHA DE DERIVACIÓN:</a> ${obj.fechaDerivacion}
                            </div>
                            <div class="list-feed-item">
                                <a href="#">ESTADO EXPEDIENTE:</a> ${obj.estadoExpedienteNombre}
                            </div>
                        </div>
                    </div>
                </div>`;
    }

    const DerivacionDetalle = (obj) => {
        let defaults = {
            data: $('#frmFiltro').serializeFormJSON()
        };
        let opciones = $.extend({}, defaults, obj);
        CargarDataGET({
            url: "Documento/DocumentoExpedienteDetalleJson",
            dataForm: opciones.data,
            callBackSuccess: function (data, dataAll) {
                if (data.id !== 0) {
                    ExpedienteSeleccionado = dataAll;
                } else {
                    ExpedienteSeleccionado = null;
                }
                const listaDerivacion = dataAll.listaDerivacion;
                $("#nroExpediente").val(data.nroExpediente);
                $("#fechaRegistro").val(data.fecha);
                $("#nombreApellido").val(data.nombreUsuario);
                $("#codigoModular").val(data.codigoModular);
                $("#motivo").val(data.motivo);
                $("#establecimiento").val(data.establecimiento);
                $("#observacion").val(data.observacion);

                const contain = $("#timeline-contain");
                const container_timeline = $("#container-timeline");
                container_timeline.html("");
                if (listaDerivacion.length > 0) {
                    contain.show();
                    listaDerivacion.map(ele => {
                        container_timeline.append(ListaHistorial(ele))
                    })
                } else {
                    contain.hide();
                }
            }
        });
    }
    return {
        init: function () {
            IniciarLibrerias();
            eventos();
            DerivacionDetalle();
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    Documento.init();
});