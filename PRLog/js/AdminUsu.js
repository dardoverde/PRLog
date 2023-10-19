function seleccionaMembresia() {

    var meses = parseInt($("#selMembresia").val());
    var fInicio = iniciaFechaInicio();
    var fFin = new Date(fInicio);

    fFin.setMonth(fInicio.getMonth() + meses);
    $('#dteFechaFin').val(fFin.toISOString().split("T")[0]);

    cuentaDias();
}

function iniciaFechaInicio() {
    var fInicio = new Date();
    var fechaInicio = $("#dteFechaInicio").val();
    var fecha = new Date();

    if (fechaInicio.length < 10) {
        $('#dteFechaInicio').val(fInicio.toISOString().split("T")[0]);
    } else {
        fecha = new Date(fechaInicio);
    }

    return fecha;
}

function cuentaDias() {

    var fechaFin = $("#dteFechaFin").val();

    iniciaFechaInicio();

    if (fechaFin.length >= 10) {
        var fInicio = new Date($("#dteFechaInicio").val());
        var fFin = new Date($("#dteFechaFin").val());
        $('#txtDias').val((fFin - fInicio) / (1000 * 60 * 60 * 24));

    }
}

function validaMensualidad() {
    var Valor = $("#txtValor").val();
    console.log('verifica> ' + Valor);
    if (Valor <= 0) {
        console.log('alerta');
        var elemento = document.getElementById("txtValor");
        //elemento.className += " alert alert-danger";

        $('#GuardarMembresia').prop('disabled', true);

    } else {

        $('#GuardarMembresia').prop('disabled', false);
    }
}