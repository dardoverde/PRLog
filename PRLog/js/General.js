////function openNav() {
////    document.getElementById("mySidenav").style.width = "250px";
////    document.getElementById("main").style.marginLeft = "250px";
////    document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
////}

////function closeNav() {
////    document.getElementById("mySidenav").style.width = "0";
////    document.getElementById("main").style.marginLeft = "0";
////    document.body.style.backgroundColor = "white";
////}

function alertError(mensaje) {
    Swal.fire({
        title: "Error",
        text: mensaje,
        icon: "error"
    });
}

function getRow() {
    var row = document.createElement("tr");
    for (i = 0; i < arguments.length; i++) {
        row.append(getColumna(arguments[i]));
    }
    return row;
}
function getColumna(dato) {
    var columna = document.createElement("td");
    columna.append(dato);
    return columna;
}


function getButton(etiqueta, funcion, tooltip) {
    btn = document.createElement("button");
    btn.classList.toggle("btn");
    btn.classList.toggle("btn-light");
    btn.classList.toggle("btn-sm");
    btn.classList.toggle("btn-secondary");
    btn.title = tooltip;
    btn.setAttribute("onClick", funcion);

    btn.innerHTML = etiqueta;

    return btn;
}


function getBotones() {
    var div = document.createElement("div");
    div.classList.add("ml-auto");
    div.classList.add("btn-group");
    div.classList.add("btn-group-toggle");

    var btnStr;
    for (i = 0; i < arguments.length; i++) {
        btnStr = arguments[i].split('//');
        div.append(getButton(btnStr[0], btnStr[1], btnStr[2]));
    }

    return div;
}

function getButtonCustom(etiqueta, caracteristicas) {
    var btn = document.createElement("button");
    //console.log(caracteristicas);
    $.each(caracteristicas.split("//"), function (i, item) {
        //console.log(item.split("=")[0]+ ' ' + item.split("=")[1]);
        btn.setAttribute(item.split("=")[0], item.split("=")[1]);
    });

    btn.innerHTML = etiqueta;

    return btn;
}


function getParrafo(texto) {
    var p = document.createElement("p");
    p.innerHTML = texto;
    return p;
}

function getAvatar(iniciales) {
    var div = document.createElement("div");
    div.classList.add("avatar");
    div.classList.add("small");
    div.classList.add("col-md-1");
    var span = document.createElement("span");
    span.innerHTML = iniciales;
    div.appendChild(span);
    return div;
}

function getBr() {
    return document.createElement("br");
}

function getDivider() {
    var hr = document.createElement("hr");
    hr.classList.add("featurette-divider");
    return hr;
}


function LogOut() {
    //console.log('Salir del usuario');
    //alert("logout");
    acs.Ajax({
        url: "/Login/Logout",

        success: function (r) {
            window.location.replace("/Login/Login");
        },
        successError: function (r) {
            console.log(r);
            window.location.replace("/Login/Login");

        },
        error: function (r) {
            console.log(r);
            window.location.replace("/Login/Login");

        }
    });

}




