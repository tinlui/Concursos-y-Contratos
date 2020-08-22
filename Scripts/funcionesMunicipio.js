$(function () {

});
var busqueda = $("#municipiobuscar");
busqueda.keyup(function () {
    $("#frmFiltro").trigger("submit");
})
function RegistroEnt() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })
    swalWithBootstrapButtons.fire({
        title: 'Entidad',
        input: 'text',
        showCancelButton: true,
        confirmButtonText: "Guardar",
        cancelButtonText: "Cancelar",
        inputValidator: entidad => {

            if (!entidad) {
                return "Vacio!";
            } else {
                return undefined;
            }
        }
    })
        .then(resultado => {
            if (resultado.value) {
                let nombre = resultado.value.toUpperCase();
                $.get("/Municipio/GuardarEntidad/?ent=" + nombre, function (data) {
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'center',
                        showConfirmButton: false
                    })
                    if (data == "0") {
                        Toast.fire({
                            icon: 'warning',
                            title: 'Ya existe'
                        })
                    } else if (data == "1") {
                        Toast.fire({
                            icon: 'success',
                            title: 'Agregado'
                        });
                        DdlEntidad()
                    } else {
                        Toast.fire({
                            icon: 'error',
                            title: data
                        })

                    }

                })

            }
        });
}
function LimpiarErrores() {
    $('.list-group-item .form-row span').remove();
    $("#IdEntidad").removeClass("border border-warning");
    }
function Guardar() {

    var ddlEntidad = $("#IdEntidad");
    var ddlRegion = $("#IdRegion");
    var municipio=$('#municipio');
    var Entidad = $("#IdEntidad option:selected").val();
    var Region = $("#IdRegion option:selected").val();
    var Municipio = $('#municipio').val();

    if (Entidad <= 0) {
        ddlEntidad.addClass("border border-warning");
        ddlEntidad.after("<span class='badge alert-warning alert-dismissible fade show' role='alert'><i class='fas fa-exclamation'></i> Seleccione una opcion</span>");

    } else if (Region <= 0) {
        ddlRegion.addClass("border border-warning");
        ddlRegion.after("<span class='badge alert-warning alert-dismissible fade show' role='alert'><i class='fas fa-exclamation'></i> Seleccione una opcion</span>");
    } else if (Municipio == "") {
        municipio.addClass("border border-danger");
        municipio.after("<span class='badge alert-danger alert-dismissible fade show' role='alert'><i class='fas fa-exclamation-triangle'></i> Ingrese un nombre de Municipio</span>");
    } else {
        $.get('/Municipio/GuardarMunicipio', { IdEntidad: ddlEntidad.val(), IdRegion: ddlRegion.val(), municipio: municipio.val() }, function (data) {
            const Toast = Swal.mixin({
                toast: true,
                position: 'center',
                showConfirmButton: false
            })
            if (data == "1") {
                Toast.fire({
                    icon: 'success',
                    title: 'Guardado'
                })
                ddlEntidad.empty();
                ddlRegion.empty();
                municipio.innerHTML = "";
                var ad = $(".list-group-item");
                ad.addClass("invisible")
                
            } else {
                
                Toast.fire({
                    icon: 'warning',
                    title: data
                })
              
            }
            
        });
    }
}
function DdlEntidad() {
    var ddl = $('#IdEntidad');
  
    ddl.empty();
    $.get("/Municipio/listaEntidad", function (data) {
        ddl.append("<option value=0><--Entidad--></option>")
     
        $.each(data, function (index, row) {
        
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
        });
    
    });
}
$("#IdEntidad").change(function () {
    LimpiarErrores();
    var ddlR = $('#IdEntidad');
    var region = $('#IdRegion');
   
    region.empty();
    if (ddlR.val() >= 1) {
        $.get("/Municipio/listaRegion", { Entidadid: ddlR.val() }, function (data) {
            region.append("<option value=0><--Region--></option>")

            $.each(data, function (index, row) {
               
                region.append("<option value='" + row.IDREGION + "'>" + row.REGION1 + "</option>")
            });
            region.removeClass("invisible");
        })
    }
});
$("#IdRegion").change(function () {

    $('.list-group-item .form-row span').remove();
    $("#IdRegion").removeClass("border border-warning");
});
//function DdlRegion() {
//    var ddlR = $('#IdRegion');
//    ddlR.empty();
//    $.get("/Municipio/listaRegion", function (data) {
//        ddlR.append("<option value=0><--Region--></option>")

//        $.each(data, function (index, row) {
//            ddlR.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
//        });
//    });
//}

    function Mostrar() {
    DdlEntidad();

    var remover = $(".list-group-item");
    remover.removeClass("invisible")
}

