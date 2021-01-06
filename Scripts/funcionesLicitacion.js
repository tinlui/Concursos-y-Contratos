//Datos licitacion
var idLic = $("#idLicitacion")
var lisTipoPro = $("#tipoProcedimiento");
var noProc = $("#noproc");
var listAño = $("#año");
var listSolicitante = $("#solicitante");
var chkPlanos = $("#planos");
var chkEspecificaciones = $("#especificaciones");
var notas = $("#notas");
var listnObra = $("#nObra");
var listtipoObra = $("#tipoObra");
var listespObra = $("#espObra");
var direccion = $("#direccion");
var idDireccion = $("#idDir");
var listOfAut = $("#listOfAut");

var botones = $(".btn-group .btn");
var tblOfAut = $("#TblOficios");

var listmunlic = $("#municipioLicitacion");
var dirlic = $("#dir");
/*Anticipo*/
var anticipo = $('#anticipo');
var antInicio = $('#anticipoInicio');
var antMateriales = $('#anticipoMateriales');
var iva = $('#iva');
var capCont = $('#inpCapCont');
/*Anticipo*/

/*Convocatoria*/
var proceso = $("#Elaboracion");
var procesoAut = $("#presAut");
var convocatoriaDo = $("#perOficial");
var envioCn = $("#compranet");
var publicacion = $("#publicacion");
var fLimite1 = $("#inInvitaciones");
var fLimite2 = $("#fInvitaciones");
var recepcionLicitar = $("#recDoc");
/*Convocatoria*/

/*Visita Programada*/
var visitaF = $("#fVisita");
var visitaH = $("#hVisita");
var dirVisita = $("#dirVisita");
var idDirVisita = $("#idDirVisita");
var jAcF = $("#jaFecha");
var jAcH = $("#jaHora");
var aTecF = $("#ateFecha");
var aTecH = $("#ateHora");
var aFallaF = $("#afFecha");
var aFallaH = $("#afHora");
/*Visita Programada*/

$(document).ready(function () {
    var URLsearch = "";
    URLsearch   = window.location.search;

    listados();
  
   
    if (URLsearch == "") {
        TablaOfAut();
    } else {
   ////Datos Licitacion
        var url="/Licitacion/Recuperar/" + URLsearch     
        $.get(url, function (data) {
            $("#tipoProcedimiento option[value='" + data[0].IdProcedimiento + "']").attr("selected", true);
            noProc.val(data[0].NoProceso);
            $("#año option[value='" + data[0].AÑo + "']").attr("selected", true);
            $("#solicitante option[value='" + data[0].IdSolicitante + "']").attr("selected", true);
            if (data[0].Planos == 1) {
                chkPlanos.prop('checked', true);
            }
            if (data[0].Especificaciones == 1) {
                chkEspecificaciones.prop('checked', true);
            }
            notas.val(data[0].Notas);
            $("#nObra option[value='" + data[0].IdNivelObra + "']").attr("selected", true);
            $("#tipoObra option[value='" + data[0].IdTipoObra + "']").attr("selected", true);
            $("#espObra option[value='" + data[0].IdEspObra + "']").attr("selected", true);
            idLic.val(data[0].IdDatosLicitacion);
            SelectDireccion(data[0].IdDireccion);
            getAnticipo(data[0].IdDatosLicitacion);
            getConvocatoria(data[0].IdDatosLicitacion);
            getVisita(data[0].IdDatosLicitacion);
            TablaOfAut();
        });       
    }
    listadoOfAut();
});
//function WordFill() { poner en contrato
 
//    $.get("/GeneradorPdf/WordLicitacion", { id: idLic.val() });
//}
function getAnticipo(id) {
   
    $.get("/Licitacion/RecAnticipo", { id: id }, function (data) {
        if (data != "") {
            anticipo.val(data[0].Anticipo);
            antInicio.val(data[0].AnticipoInicio);
            antMateriales.val(data[0].AnticipoMateriales);
            iva.val(data[0].Iva);
            capCont.val(data[0].CapitalMinimo);
        } 
        });
  
    
} 
function getConvocatoria(id) {
    $.get("/Licitacion/RecConvocatoria", { id: id }, function (data) {
        if (data != "") {
            proceso.val(parseJsonDate(data[0].Proceso));
            procesoAut.val(parseJsonDate(data[0].ProcesoAut));
            convocatoriaDo.val(parseJsonDate(data[0].ConvocatoriaDo));
            envioCn.val(parseJsonDate(data[0].EnvioCn));
            publicacion.val(parseJsonDate(data[0].Publicacion));
            fLimite1.val(parseJsonDate(data[0].FLimite1));
            fLimite2.val(parseJsonDate(data[0].FLimite2));
            recepcionLicitar.val(parseJsonDate(data[0].RecepcionLicitar));
        }
    });
}
function getVisita(id) {
    $.get("/Licitacion/RecVisita", { id: id }, function (data) {
        if (data != "") {
            visitaF.val(parseJsonDate(data[0].VisitaF));
            visitaH.val(data[0].VisitaH);
            //dirVisita 
            //idDirVisita 
            jAcF.val(parseJsonDate(data[0].JacFecha));
            jAcH.val(data[0].JacHora);
            aTecF.val(parseJsonDate(data[0].AtecFecha));
            aTecH.val(data[0].AtecHora);
            aFallaF.val(parseJsonDate(data[0].ActaFallaFecha));
            aFallaH.val(data[0].ActaFallaHora);
        }
    });
}
function parseJsonDate(jsonDateString) {
    return moment(jsonDateString).format("YYYY-MM-DD").toUpperCase();
}
////////////////////////////////////////
///dropdownlist's
function listadoOfAut() {
    listOfAut.empty();
    $.get('/Licitacion/listaOfAutoriza', function (data) {
        if (data.length == 0) {
            listOfAut.append("<option value=0>No hay sin asignar</option>")
        } else {
        $.each(data, function (index, row) {
            listOfAut.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        })
        }
    })
}
function years() {
    var i;
    for (i = 2010; i <= 2025; i++) {
        listAño.append("<option value=" + i + ">" + i + "</option>");
    }
}
function listadoTProc() {
    $.get('/Licitacion/listaTipoProc', function (data) {
        $.each(data, function (index, row) {

            lisTipoPro.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        })
    })
}
function listadoSolicitante() {
    $.get('/Licitacion/listaTipoCont', function (data) {
        $.each(data, function (index, row) {
            listSolicitante.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        });
    });
}
function listadoNivelObra() {
    $.get('/Licitacion/listaNivelObra', function (data) {
        $.each(data, function (index, row) {
            listnObra.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        });
    });
}
function listadotipoObra() {
    $.get('/Licitacion/listaTipoObra', function (data) {
        $.each(data, function (index, row) {
            listtipoObra.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        });
    });
}
function listadoMunicipio() {
    listmunlic.empty();
    $.get('/Licitacion/listarMunicipio', function (data) {
        $.each(data, function (index, row) {

            listmunlic.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        })
    })
}
$('#myTab a#home-tab').on('click', function () {
    TablaOfAut();
    listadoOfAut();
})

function listadoEspObra() {
    $.get('/Licitacion/listaEspObra', function (data) {
        $.each(data, function (index, row) {
            listespObra.append("<option value=" + row.Value + ">" + row.Text + "</option>")
        });
    });
}

function listados() {
    years();
    listadoTProc();
    listadoSolicitante();
    listadoNivelObra();
    listadotipoObra();
    listadoEspObra();
}
/////// termina dropddown's

$.fn.requerido = function () {
    var input = $(this);
    ayuda = '';

    if (input.attr('required')) {
        mostrar_error(input);
    }

    input.on('keyup', function () {

        if (input.val().length > 0) {
            if (input.next()[0].className == 'invalid-feedback d-block') {

                input.next().remove();
            }
            input.removeClass('border-danger');
        } else {

            mostrar_error(input)
        }
    });

    function mostrar_error(input) {

        if (ayuda == '') {
            ayuda += '<span class="invalid-feedback d-block">';
            ayuda += 'Este campo es Requerido';
            ayuda += '</span>';
            input.after(ayuda);
            input.addClass('border-danger');
        } else if (input.next()[0].className == 'invalid-feedback d-block') {
            
        } else {
            ayuda = '';
            ayuda += '<span class="invalid-feedback d-block">';
            ayuda += 'Este campo es Requerido';
            ayuda += '</span>';
            input.after(ayuda);
            input.addClass('border-danger');
        }
    }
}
noProc.requerido();

function TablaOfAut() {
    
    var lista = "";
    tblOfAut.empty();
    if (noProc.val() != "") {

        $.get('/Licitacion/licitacionOficioAut', { noProceso: noProc.val() }, function (data) {
            lista += '<tr>';
            lista += '<td>Oficio</td>';
            lista += '<td>Monto </td>';
            lista += '</tr>';
            $.each(data, function (index, row) {
                lista += '<tr>';
                lista += '<td>' + row.Oficio + '</td>';
                lista += '<td>' + row.MontoAutorizado + '</td>';
                lista += '</tr>';
            });

            tblOfAut.prepend(lista);
        });
    } else {

        lista += '<tr>';
        lista += '<td  class="alert alert-info" role="alert" >No hay datos para mostrar</td>';
        lista += '</tr>';
        tblOfAut.prepend(lista);
    }
}
// direccion
direccion.autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/Licitacion/GetDirecccionList",
            dataType: "json",
            type: "POST",
            data: { dir: direccion.val() },
            success: function (data) {
                
                response($.map(data, function (item) {
                    return { value: item.Text, label: item.Text, id: item.Value }
                }))
            },
            error: function () {
                console.log("error");
            },
            failure: function () {
                console.log("failure")
            }
        });
    },
    select: function (e, i) {
        idDireccion.val(i.item.id);

    }
});

$("ul#ui-id-1").on("click", function () {
   
    if (idDireccion.val() < 1) {

        listadoMunicipio();
        $("#agregarDir").click();
    }
})

function SelectDireccion(id) {
    
    $.get("/Licitacion/GetIdDireccion", { id: id }, function (data) {
        
        direccion.val(data[0].Text);
        idDireccion.val(data[0].Value);
        dirVisita.val(data[0].Text);
        idDirVisita.val(data[0].Value);
    })
}
dirVisita.autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/Licitacion/GetDirecccionList",
            dataType: "json",
            type: "POST",
            data: { dir: dirVisita.val() },
            success: function (data) {
                
                response($.map(data, function (item) {
                    return { value: item.Text, label: item.Text, id: item.Value }
                }))
            },
            error: function () {
                console.log("error");
            },
            failure: function () {
                console.log("failure")
            }
        });
    },
    select: function (e, i) {
        idDirVisita.val(i.item.id);

    }
})


///Guardar
function guardaLicitacion() {
    if (chkPlanos.prop('checked') == true) {
        chkPlanos.val(1);
    } else {
        chkPlanos.val(0);
    }
    if (chkEspecificaciones.prop('checked') == true) {
        chkEspecificaciones.val(1);
    } else {
        chkEspecificaciones.val(0);
    }
    const Toast = Swal.mixin({
        toast: true,
        position: 'center',
        timer: 1900,
        showConfirmButton: false
    });

    //despues de guardar que aparezcan los botones de opcion de abajo sino que muestre el error
    if (noProc.val() != "") {
        $.get('/Licitacion/guardaLicitacion', {
            IdProcedimiento: lisTipoPro.val(), NoProceso: noProc.val(), AÑo: listAño.val(), IdSolicitante: listSolicitante.val(), Planos: chkPlanos.val(),
            Especificaciones: chkEspecificaciones.val(), Notas: notas.val(), IdNivelObra: listnObra.val(), IdTipoObra: listtipoObra.val(), IdEspObra: listespObra.val(), IdDireccion: idDireccion.val()
        }, function (data) {

                if (data == "1") {
                    dirVisita.val(direccion.val());
                    idDirVisita.val(idDireccion.val());
                Toast.fire({
                    icon: 'success',
                    title: 'Guardado'
                });
            } else {
                Toast.fire({
                    icon: 'warning',
                    title: data
                });
            }
        });
    } else {
        Toast.fire({
            icon: 'warning',
            title: 'llenar el campo requerido'
        });
    }
}
function asignarOficios() {
    if (noProc.val() != "") {
        $.get('/Licitacion/guardaOficioContrato', { oficioaut: listOfAut.val(), licitacion: noProc.val() },
            function (data) {
            const Toast = Swal.mixin({
                toast: true,
                position: 'center',
                timer: 1000,
                showConfirmButton: false
            });
            if (data == "1") {
                Toast.fire({
                    icon: 'success',
                    title: 'Asignado: ' + listOfAut.val()
                });
                TablaOfAut()
            } else {

                Toast.fire({
                    icon: 'warning',
                    title: data
                });
            }
        });

    } else {
        const Toast = Swal.mixin({
            toast: true,
            position: 'center',
            timer: 1500,
            showConfirmButton: false
        });
        Toast.fire({
            icon: 'warning',
            title: 'Capture el numero de licitacion'
        });
    }
}
function guardaDireccion() {
    if (listmunlic.val() != "null" && dirlic.val() != "") {
        $.get('/Licitacion/guardaDireccion', { direccion: dirlic.val(), idmun: listmunlic.val() }, function (data) {
        })
    }
}
function guardaAnticipo() {
   console.log( tblOfAut.val())
    const Toast = Swal.mixin({
        toast: true,
        position: 'center',
        timer: 1500,
        showConfirmButton: false
    });

    
    if (noProc.val() == "") {
        Toast.fire({
            icon: 'warning',
            title: 'Capture No.Proceso'
        });
    } else {

        $.get('/Licitacion/VerificaNoProceso', { noproc: noProc.val() }, function (data) {

            if (data == "1") {
                $.get('/Licitacion/guardaAnticipo', {
                    noproc: noProc.val(), Anticipo: anticipo.val(), AnticipoInicio: antInicio.val(), AnticipoMateriales: antMateriales.val(),
                    Iva: iva.val(), CapitalMinimo: capCont.val()
                }, function (data) {
                  
                    if (data == "1") {
                        Toast.fire({
                            icon: 'success',
                            title: 'Agregado'
                        });
                        TablaOfAut()
                    } else {

                        Toast.fire({
                            icon: 'warning',
                            title: data
                        });
                    }
                });
            } else {
                Toast.fire({
                    icon: 'info',
                    title: 'Verifique el  No.Proceso'
                });
            }
        })

          

    }    
}
function guardaConvocatoria() {
   
    $.get('/Licitacion/guardaConvocatoria', {
        noproc: noProc.val(), Proceso: proceso.val(), ProcesoAut: procesoAut.val(), ConvocatoriaDo: convocatoriaDo.val(), EnvioCn: envioCn.val(),
        Publicacion: publicacion.val(), FLimite1: fLimite1.val(), FLimite2: fLimite2.val(), RecepcionLicitar: recepcionLicitar.val()
    }, function (data) {
            const Toast = Swal.mixin({
                toast: true,
                position: 'center',
                timer: 1000,
                showConfirmButton: false
            });
            if (data == "1") {
                Toast.fire({
                    icon: 'success',
                    title: 'Agregado'
                });
                TablaOfAut()
            } else {

                Toast.fire({
                    icon: 'warning',
                    title: data
                });
            }
    });
}
function guardaVisitaProg() {
    $.get('/Licitacion/guardaVisita', {
        noproc: noProc.val(), VisitaF: visitaF.val(), VisitaH: visitaH.val(), IdDireccion: idDirVisita.val(), JacFecha: jAcF.val(), JacHora: jAcH.val(),
        AtecFecha: aTecF.val(), AtecHora: aTecH.val(), ActaFallaFecha: aFallaF.val(), ActaFallaHora: aFallaH.val()
    }, function (data) {
        const Toast = Swal.mixin({
            toast: true,
            position: 'center',
            timer: 1000,
            showConfirmButton: false
        });
            if (data == "1") {
                Toast.fire({
                    icon: 'success',
                    title: 'Agregado'
                });
                TablaOfAut()
            } else {

                Toast.fire({
                    icon: 'warning',
                    title: data
                });
            }
    });
    
}

