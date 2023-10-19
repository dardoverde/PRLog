var acs = {
    trigger: null,
    starProcessingTrigger: function () {
        if (this.trigger !== null) inicioProceso(this.trigger);
    },
    stopProcessingTrigger: function () {
        if (this.trigger !== null) paradoProceso(this.trigger);
    },
    //MANDAR POR DEFAULT UN token?
    Ajax: function (args) {
        /// <summary>Función que ejecuta una petición AJAX.</summary> y maneja cosas standar
        if (args.trigger !== undefined) { this.trigger = args.trigger; acs.starProcessingTrigger(); }
        $.ajax({
            type: args.type == undefined ? "POST" : args.type,
            url: SITEROOT + args.url,
            contentType: "application/json; charset=utf-8",
            data: args.data == undefined ? {} : args.data,
            dataType: "json",
            timeout: args.timeout == undefined ? 300000 : args.timeout,
            beforeSend: args.beforeSend == undefined ? function () { } : args.beforeSend(),
            async: args.async == undefined ? true : args.async,
            success: function (r) { acs.manejaSuccessAjax(args.success, args.successError, r); },
            error: function (err) { acs.manejaErrorAjax(args.error, args.offlineError, err); },

        });
    },

    manejaSuccessAjax: function (manejador, manejadorFalso, r) {

        //Conversión a json en caso de venir
        var conv = isJSON(r.data);
        if (conv !== false) r.data = conv;

        if (r.exitoso) {
            if (manejador != undefined) { manejador(r); }
        }
        else {
            var msgMobil = $.nd2Toast !== undefined;
            if (msgMobil === true) {
                new $.nd2Toast({ message: r.data, class: 'nd2-toast-error', ttl: 8000 });
            } else {
                alert('Operación incompleta: ' + r.data);
            }
            console.log("Error desde el servidor: ");
            console.log(r);
            if (manejadorFalso !== undefined) { manejadorFalso(r); }
        }
        acs.stopProcessingTrigger();
    },

    manejaErrorAjax: function (manejador, manejadorOffline, err) {

        var msgMobil = $.nd2Toast !== undefined;
        var ejecutarManejador = true;
        console.log("Logueo de errores: ");
        console.log(err);

        if (err != null && "status" in err) {
            var especificacion = "";
            switch (err.status) {
                case 0:
                    if (err.statusText === "timeout") { especificacion = ""; }
                    mostrar = false;
                    if (msgMobil === true) {
                        new $.nd2Toast({ message: "Error de red", class: 'nd2-toast-error', ttl: 8000 });
                        if (manejadorOffline !== undefined) {
                            manejadorOffline();
                            ejecutarManejador = false;
                        }
                    } else {
                        alert('Timeout Error al conectar.');
                    }
                    break;
                case 404:
                    if (msgMobil === true) {
                        new $.nd2Toast({ message: "No encontrado" + " " + err.status + " " + err.statusText, class: 'nd2-toast-error', ttl: 8000 });
                    } else {
                        alert("No encontrado" + " " + err.status + " " + err.statusText, 'Error al conectar.');
                    }

                    break;
                case 500:
                    if (msgMobil === true) {
                        new $.nd2Toast({ message: "Problema de servidor (Error 500)" + " " + err.status + " " + err.statusText, class: 'nd2-toast-error', ttl: 8000 });
                    } else {
                        console.log("Problema de servidor (Error 500)" + " " + err.status + " " + err.statusText, 'Error al conectar.', { positionClass: "toast-bottom-right" });
                    }
                    break;
            }
        } else {
            if (msgMobil === true) {
                new $.nd2Toast({ message: "Error indeterminado al conectar con ajax", class: 'nd2-toast-error', ttl: 8000 });
            }
            else {
                alert('Error indeterminado al conectar con ajax Error al conectar.');
            }

        }

        // el cliente quiere manejar el error
        if (manejador != undefined && ejecutarManejador) { manejador(err); }

        acs.stopProcessingTrigger();
    },
};

//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

function isJSON(valor) {
    try {
        return $.parseJSON(valor);
    } catch (e) {
        return false;
    }
}

function getOutDate(d) {//as Date
    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = d.getFullYear() + '/' +
        (month < 10 ? '0' : '') + month + '/' +
        (day < 10 ? '0' : '') + day;

    if (output == '1969/12/31') return '';
    return output;
}

function getOutDateTime(d) {//d as Date
    var dt = getOutDate(d);
    if (dt === '') { return ''; }
    var min = d.getMinutes();
    var hour = d.getHours();

    var time = (hour < 10 ? '0' : '') + hour + ":" +
        (min < 10 ? '0' : '') + min;
    return dt + ' ' + time;
}

function getDateFromJson(data) {
    var fecha;

    if (dateIsEmpty(data))
        return '';
    else {
        if (typeof data !== 'undefined') {
            fecha = new Date(parseInt(data.substr(6)));
            return getOutDate(fecha);
        } else
            return data;
    }
}

function dateIsEmpty(data) {//
    var cincuentayalgo = new Date(1950, 1, 2);
    var numeroFecha;
    var fecha;

    if (typeof data !== 'undefined' && data !== null) {
        numeroFecha = parseInt(data.substr(6));
        fecha = new Date(numeroFecha);

        if (fecha.getTime() < cincuentayalgo.getTime()) {
            return true;
        } else {
            return false;
        }
    } else {
        return true;
    }
}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

//================================================== GENERAL ==================================//
function BtnLoad(id_) {
    this.id = id_;
    this.clases = "";
    this.Start = function () {
        this.clases = $("#" + this.id + " i").attr('class');
        $("#" + this.id + " i").removeClass()
        $("#" + this.id + " i").addClass("fa fa-spinner fa-spin");
        $("#" + this.id).attr("disabled", true);
    };
    this.Stop = function () {
        $("#" + this.id).attr("disabled", false);
        $("#" + this.id + " i").removeClass()
        $("#" + this.id + " i").addClass(this.clases);
    };
}


var SPANISHDATATABLE = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
}

function showLoading() {
    console.log('callLoading');

    $("#divLoading").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true //Display loader!
    });
}

function hideLoading() {
    console.log('hideLoading');

    $('#divLoading').modal('hide');
}


function getHtmlLink(dataName, dataValue, clase, modal, texto, onclick) {

    if (onclick == undefined) onclick = '';

    var lnk = '<a href="#"';
    lnk += "data-" + dataName + "='" + dataValue + "' ";
    lnk += `class="${clase}" `;
    lnk += `onclick="${onclick}" `;
    lnk += 'data-toggle="modal" ';
    lnk += 'data-target="#' + modal + '"> ';
    lnk += texto + '</a>';
    return lnk;
}

// sleep time expects milliseconds
function sleep(time) {
    return new Promise((resolve) => setTimeout(resolve, time));
}