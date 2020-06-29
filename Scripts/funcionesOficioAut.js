///-------------------Oficio Autorizacion-------------------------///////
//var entidad = $("#Entidades");
//var regiones = $("#Regiones");
//    mun.autocomplete({
//    source: function (request, response) {
//        $.get("/OficioAut/GetMunicipioList", { municipio: mun.val() }, function (data) {
//            response($.map(data, function (item) {

//                return { label: item.MUNICIPIO1, value: item.MUNICIPIO1, id: item.IDMUNICIPIO };

//            })
//            )
//        })
//        }
//})
var oficio;
var mun = $("#municipio");
var numOficio = $("#OficioAut");
var fec1 = $("#FecAutorizacion");
var fec2 = $("#FecRecibido");
var numAsig = $("#NumAsignacion");
var numObra = $("#NumObra");
var descObra = $("#DescObra");
var montoAut = $("#MontoAutorizado");
var idmun = $("#idmunicipio");
var idProg = $("#IdPrograma");
var idAut = $("#IdAutoriza");
var montoA = $("#montoAut");
$(function () {
 
    ListadoMontoFin();
  
    //monto.keydown(function (event) {
    //    if (event.keyCode < 48 || event.keyCode > 57) {
    //        if (event.keyCode < 96 || event.keyCode > 105) {
    //            if (event.keyCode != 46 && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 190) {
    //                event.preventDefault();
    //            }
    //        }
    //    }
    //})
});
function VerCM() {
  
    $('.list-group-item.border-0.col-lg-6').removeClass('invisible')
}
fec1.change(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'center',
        showConfirmButton: false
    });
    if (fec2.val() != "") {
        if (fec1.val() > fec2.val()) {
            Toast.fire({
                icon: 'error',
                title: 'No puede ser menor a la Fecha del Oficio!!'
            })
        }
    }
})
fec2.change(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'center',
        showConfirmButton: false
    })
    if (fec1.val() > fec2.val()) {
        Toast.fire({
            icon: 'error',
            title: 'No puede ser menor a la Fecha del Oficio!!'
        })
    }
})
mun.autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/OficioAut/GetMunicipioList",
            dataType: "json",
            type: "POST",
            data: { municipio: mun.val() },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.MUNICIPIO1, value: item.MUNICIPIO1, id: item.IDMUNICIPIO };
                }))
            },
            error: function (response) {
                console.log(response.responseText);
            },
            failure: function (response) {
                console.log(response.responseText)
            }
        });
    },
    select: function (e, i) {

        idmun.val(i.item.id);
      

    }
});
mun.change(function () {

    if (idmun.val() < 1) {
        
        Swal.fire({
            title: 'No existe',
            text: '¿Desea agregar el municipio: ' + mun.val() + ' ?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: '@Html.ActionLink("Si, ir a agregarlo!", "Index", "Municipio", new { area = "" }, new { @class = " text-white mr-2" }) '
        })
      
    } else {
       
    }
   


})

function GuardarOficio() {
   
    if (numOficio.val() == "") {
        numOficio.addClass(" border-warning");
    } else if (fec1.val() == "") {
        fec1.addClass(" border-warning");
    } else if (fec2.val() == "") {
        fec2.addClass("border-warning");
    } else if (numAsig.val() == "") {
        numAsig.addClass("border-warning");
    } else if (descObra.val() == "") {
        descObra.addClass("border-warning");
    } else if (montoAut.val() == "") {
        montoAut.addClass("border-warning");
    } else if (mun.val() == "") {
        mun.addClass("border-warning");
    } else if (idProg.val() == "<--Programa-->") {
        idProg.addClass("border-warning");
    } else if (idAut.val() == "<--Autorización-->") {
        idAut.addClass("border-warning");
    } else {
        $.get('/OficioAut/guardarOficio', {
            OficioAut: numOficio.val(), FecAutorizacion: fec1.val(), FecRecibido: fec2.val(), NumAsignacion: numAsig.val(), NumObra: numObra.val(), DescObra: descObra.val(), IdAutoriza: idAut.val(), MontoAutorizado: montoAut.val(), IdPrograma: idProg.val(), IdMunicipio: idmun.val()
        }, function (data) {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'center',
                    timer: 1000,
                    showConfirmButton: false
                })
                if (data == "1") {
                    montoA.val(montoAut.val());
                   
                    oficio = numOficio.val();
                    IdOficio();
                    var ad = $(".list-group-item.border-0.col-lg-4");
                    ad.removeClass("invisible");
                    LimpiarOficio();
                    Toast.fire({
                        icon: 'success',
                        title: 'Guardado: ' + numOficio.val()
                    })
                  
                } else {

                    Toast.fire({
                        icon: 'warning',
                        title: data
                    })

                }
        });
    }
   
}
function LimpiarOficio() {
    idAut.prop('selectedIndex',0);
    idProg.prop('selectedIndex', 0);
    numOficio.val("");
    fec1.val("");
    fec2.val("");
    numAsig.val("");
    numObra.val("");
    descObra.val("");
    montoAut.val("");
    municipio.value = "";
}

///--------------------------------------------///////



////Fuente Financiamiento

var Fuente = $('#IdFuenteFin');
var idOficio = $('#idOficio');
var idEstructuraFin = $('#idOrigen');
var monto = $('#monto');
var montosTabla = $('#montos');
var verTabla = $('.list-group-item.border-0.col-lg-4 div');
var totalAcumulado = $('#totalAcum');
Fuente.change(function () {
    $('#Origen').innerHTML = "";
    $.get("/OficioAut/ShowOrigen", { idFuenteFin: Fuente.val() }, function (data) {
        $('#Origen').val(data.EstructuraFin);
        idEstructuraFin.val(data.IdEstructuraFin)
       
    })

})
function IdOficio() {
    $.get("/OficioAut/Oficio", { numOficio: oficio }, function (data) {
        idOficio.val(data);
       
    })
}
function ListadoMontoFin() {
    var pct;
    var lista="";
    if (idOficio.val() != "") {
        montosTabla.empty();
        $.get('/OficioAut/MontoxOficio', { oficioId: idOficio.val() }, function (data) {
            $.each(data, function (index, row) {
              
                var acumulado = montoA.val();
                pct = ((parseFloat(row.Monto).toFixed(2) * 100) / parseFloat(acumulado).toFixed(2))
              
         
                lista += '<tr>';
                lista += '<td>' + row.FuenteFin + '</td>';
                lista += '<td>' + row.EstructuraFin + '</td>';
                lista += '<td>' + row.Monto.toFixed(2) + '</td>';
                lista += '<td>' + pct.toFixed(2) +'%' +'</td>';
                lista += '</tr>';
              //  pctMontos(row.EstructuraFin, row.Monto);
            });
            montosTabla.prepend(lista);
        });
        

    }
}


//---------------Calculos-----//
var montoAcumulado;
function calcularMontos(Monto) {
   
    var aux = 0;
    var total = montoA.val();
    var retVal = false;
    
      
  if (totalAcumulado.val() > 0) {
        totalAcumulado.val(parseFloat(totalAcumulado.val()) + parseFloat(Monto))

  }  
  else if (totalAcumulado.val() == total) {

  }
    else {
        montoAcumulado = parseFloat(aux) + parseFloat(Monto)
        totalAcumulado.val(montoAcumulado)
        console.log(totalAcumulado.val())
    }
    
    if (totalAcumulado.val() > total) {
        totalAcumulado.val(parseFloat(totalAcumulado.val() - parseFloat(Monto)))
      retVal = true;
      
    } 
    return retVal;
}



//---------------Guardar-----//

function GuardarMonto() {
    var retVal = calcularMontos(monto.val())

    if (retVal == false) {
        $.get('/OficioAut/guardarMonto', { IdOficioSAut: idOficio.val(), IdFuenteFin: Fuente.val(), IdEstructuraFin: idEstructuraFin.val(), Monto: monto.val() },
            function (data) {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'center',
                    timer:1000,
                    showConfirmButton: false
                })
                if (data == "1") {
                    Toast.fire({
                        icon: 'success',
                        title: 'Guardado'
                    })
                    verTabla.removeClass('invisible');
                    ListadoMontoFin();
                } else {
                    Toast.fire({
                        icon: 'warning',
                        title: data
                    })
                }
            });
    } else {
        monto.val("");
        const Toast = Swal.mixin({
            toast: true,
            position: 'center',
            showConfirmButton: false
        })
        Toast.fire({
            icon: 'info',
            title:'Verifique el monto asignado!'
        })
    }
  
}

    //////
    //$(document).ready(function () {

    //    entidad.change(function () {
    //        regiones.empty();

    //        if (entidad.val() >= 1) {
    //            $.get("/OficioAut/GetRegionList", { EntidadId: entidad.val() }, function (data) {
    //                regiones.append("<option value=0><--Seleccionar--></option>")
    //                $.each(data, function (index, row) {
    //                    regiones.append("<option value='" + row.IDREGION + "'>" + row.REGION1 + "</option>")
    //                });
    //                regiones.removeClass("invisible");
    //            });
    //        } else {
    //            regiones.addClass("invisible");
    //            regiones.innerHTML = "<--Seleccionar-->";
    //        }
    //    });
    //});
    //regiones.change(function () {
    //    if (regiones.val() >= 1) {
    //        mun.removeClass("invisible");
    //    } else {
    //        mun.addClass("invisible");
    //        mun.innerHTML = "";
    //    }
    //});
    //mun.on("keyup", function () {
    //    if (mun.length != 0) {
    //        $.get("/OficioAut/GetMunicipioList", { RegionID: regiones.val(), municipio: mun.val() }, function (data) {
    //            $.each(data, function (index, row) {
    //                console.log(row.IDMUNICIPIO + "" + row.MUNICIPIO1)

    //            });
    //        });
    //    }
    //});