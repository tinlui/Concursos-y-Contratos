var idLicitacion = $("#idLicitacion");
var InscritosTabla = $("#tblInscritos");
var idContratista = $("#Contratistas");
var idDesicion = $("#Desicion");
var idDesicionF = $("#Desicion_Falla");

var Ncontrato = $("#NoCont");
var Fcontrato = $("#FechaCont");
var listaOficios = $("#NoOf")

function listaLicitacionOficio() {
   
    if (noProc.val() != "") {
 
    listaOficios.empty();
    $.get('/Contrato/listaLicitacionOficio', { numProceso: noProc.val()},
        function (data) {
            if (data.length > 0) {
                $.each(data, function (index, row) {
                    listaOficios.append("<option value= '" + row.Value + "' >" + row.Text + "</option>")
                });
            } else {
                listaOficios.append("<option value= 0 > No hay registrados </option>")
            }
        }
    )
}
}
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

function NuevoContrato() {
    const Toast = Swal.mixin({
        toast: true,
        position: 'center',
        timer: 1600,
        showConfirmButton: false
    });

    if (Ncontrato.val() != "") {
        $.get('/Contrato/guardaContrato', { NoContrato: Ncontrato.val(), FechaContrato: Fcontrato.val() },
            function (data) {
                if (data == "1") {
                    Toast.fire({
                        icon: 'success',
                        title: 'Guardado!'
                    });
                } else {
                    Toast.fire({
                        icon: 'error',
                        title: data
                    });
                }
            } )
    } else {
        Toast.fire({
            icon: 'warning',
            title: 'No Contrato'
        });
    }
}