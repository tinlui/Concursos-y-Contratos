var idLicitacion = $("#idLicitacion");
var InscritosTabla = $("#tblInscritos");
var idContratista = $("#Contratistas");
var idDesicion = $("#Desicion");
var idDesicionF = $("#Desicion_Falla");
//$(document).ready(function () {
//    TablaInscritos();
//});
//id dato licitacion para comprobar que existan inscritos y se pueda crear el contrato
function llenarCombosInscritos() {
    
    comboContratista();
    comboDesicion();
    comboDesicionFallo();
}

function comboContratista() {
    idContratista.empty();
    $.get('/Contrato/listaContratistas', function (data) {
        
        if (data.length >0) {
            $.each(data, function (index, row) {
                idContratista.append("<option value= '" + row.Value + "' >" + row.Text + "</option>")
            });
        } else {
            idContratista.append("<option value= 0 > No hay registrados </option>")
          
        }
    });
}
function comboDesicion() {
    idDesicion.empty();
    idDesicion.append("<option value= 0 > <--Desicion---> </option>")
    $.get('/Contrato/listaDesicion', function (data) {
        if (data.length > 0) {
            $.each(data, function (index, row) {
                idDesicion.append("<option value= '" + row.Value + "'>" + row.Text + "</option>")
            });
        } 
    });
}
function comboDesicionFallo() {
    idDesicionF.empty();
    idDesicionF.append("<option value=0><--Fallo--></option>")
    $.get('/Contrato/listaFalla', function (data) {
        if (data.length > 0) {
            $.each(data, function (index, row) {
                idDesicionF.append("<option value='"+row.Value+"'>"+row.Text+"</option>")
            });
        }
    });
}
function prueba() {
    console.log("prueba")
}


function TablaInscritos() {
    console.log("tabla inscritos")
    if (idLicitacion.val() != "") {
        console.log("data  " + idLicitacion.val())
    } else {
        console.log("no data  " + idLicitacion.val())
    }
    var lista = "";
    InscritosTabla.empty();
    if (noProc.val() != "") {

        $.get('/Licitacion/licitacionOficioAut', { noProceso: noProc.val() }, function (data) {
         
            $.each(data, function (index, row) {
                lista += '<tr>';
                lista += '<td>' + row.Oficio + '</td>';
                lista += '<td>' + row.MontoAutorizado + '</td>';
                lista += '</tr>';
            });

            InscritosTabla.prepend(lista);
        });
    } else {

        lista += '<tr >';
        lista += '<td colspan=6  class="alert alert-danger text-center" role="alert" >No hay datos para mostrar</td>';
        lista += '</tr>';
        InscritosTabla.prepend(lista);
    }
}