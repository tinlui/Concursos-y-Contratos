//jQuery time
//--------------------------------------------//
var consulta = $('#contratistaConsulta');
var idC = $('#idCon');
// Generales
var nombre = $('#Nombre');
var rfc = $('#Rfc');
var curp = $('#Curp');
var telefono = $('#Telefono');
var correo = $('#Correo');
var calle = $('#Calle');
var noExt = $('#NoExterior');
var noInt = $('#NoInterior');
var colonia = $('#Colonia');
var cp = $('#Cp');
var idMun = $('#IdMunicipio');
var año = $('#Year');
var moral = $('#IsMoral');
var btnGuardarNombre = $('.btn.btn-success.mt-2');

	// moral
var acta = $('#ActaConstitutiva');
var fechaActa = $('#FechaActa');
var notarioNum = $('#NotarioNum');
var notarioNom = $('#NotarioNombre');
var regPublico = $('#RegPublico');
var idMun2 = $('#IdMunicipio2');
var nombreCont = $('#NombreCont');

//	Registro
var regContraloriaFolio = $('#RegContraloriaFolio');
var regContraloriaIngreso = $('#RegContraloriaIngreso');
var fechaExpedicion = $('#FechaExpedicion');
var fechaVigencia = $('#FechaVigencia');
var idEspecialidad = $('#IdEspecialidad');
var idTipo = $('#IdTipo');
var capital = $('#Capital');
var idInfoCapital = $('#IdInfoCapital');
var fechaInf = $('#FechaInf');
var activo = $('#Activo');
var nombreReg = $('#NombreRegCont');

// Representante
var representada = $("#Representada");
var puesto = $("#Puesto");
var idAcredita = $("#Acredita");
var numAcredita = $("#NAcredita");
var idIdentificacion = $("#IdIdentificacion");
var numIden = $("#NIden");
var nombreRep = $("#NombreRep");

// Poder
var poderRep = $('#PoderRep');
var fecha = $('#Fecha');
var notarioNo = $('#NotarioNo');
var notarioNombre = $('#NotarioNomb');
var municipio = $('#IdMunPoder');
var NombrePod = $('#NombrePod');
// BOTONES DE NEXT Y PREV
var current_fs, next_fs, previous_fs; //fieldsets
var left, opacity, scale; //fieldset properties which we will animate
var animating; //flag to prevent quick multi-click glitches
var nextForm = $(".next");
var prevForm = $(".previous");
var progress = $("#progressbar li");

//--------------------------------------------//
//				Formularios validacion		//
//--------------------------------------------//
var formGenerales = $('#Generales');
var formRegistro = $('#Registro');
//var formRepresenta = $('#Representa');


/// Validacion de Campos

///fin
$.fn.requerido = function () {
	
	var input = $(this);
	
	ayuda = '';

	if (input.attr('required')) {
		mostrar_error(input);
	}

	input.on('keyup', function () {
		if (input.val().length > 0) {
			//input.next().remove();
			input.removeClass('border-danger');
			if (input.attr('id') == "Nombre") {
				btnGuardarNombre.removeClass('invisible');
				rfc.addClass('border-warning');
				idMun.addClass('border-warning');

			}
		} else {
			mostrar_error(input);
		}
	});

	function mostrar_error(input) {
		//if (ayuda == '') {
			//ayuda = '';
			//ayuda += ' <div class="invalid-feedback">';
			//ayuda += ' Este campo es Requerido';
			//ayuda += '</div>';
		//	input.after(ayuda);
		input.addClass('border-danger');
		if (input.attr('id') == "Nombre") {
			btnGuardarNombre.addClass('invisible');
			rfc.removeClass('border-warning');
			idMun.removeClass('border-warning');
		} 
	};
}

idMun.on('change', function () {
	btnGuardarNombre.addClass('invisible');
	rfc.removeClass('border-warning');
	idMun.removeClass('border-warning');
	if (rfc.val() != "") {
		if ($('#IdMunicipio option:selected').val() != "<--Selected-->") {
			$("#DataGeneral").removeClass('invisible');
		}
	}

});


//--------------------------------------------//
//				Campos validacion		//
//--------------------------------------------//
nombre.requerido();
consulta.autocomplete({
	source: function (request, response) {
		$.ajax({
			url: '/Contratistas/GetConsultaContratista',
			dataType: 'json',
			type: 'POST',
			data: { contratistaCon: consulta.val() },
			success: function (data) {
				response($.map(data, function (item) {
				return { label: item.NOMBRE, value: item.NOMBRE, id: item.IDCONTRATISTA }
				}))
			},
			error: function (response) {
				console.log(response.responseText);
			},
			failure: function (response) {
				console.log(response.responseText)
			}
		})
	},
	select: function (e, i) {
		idC.val(i.item.id);
    }
});
function FormularioValidacion(evento) {
//	// cada vez que clickas un boton. Automaticamente se invoca con un parametro
//	// que es el Evento. el cual tiene una propiedad (entre otras) llamada target
//	// que es el elemento que dispara el evento. Luego buscas el id en target.
	  
	switch (evento.target.id) {
		case "DataGeneral":
			//GuardarContratista()
			//nextForm.trigger('click', true);

			//		formGenerales.find('.form-control-sm').each(function () {
			//			var elemento = this;
			//		console.log("elemento.id=" + elemento.id + ", elemento.value=" + elemento.value); 
			//si estan los datos bien guarda
			//si no regresa regresa los errores en campos
			//antes de pasar al siguiente formulario verifica si es moral
			//si es moral despliega modal para llenar datos
			//verifica que llene datos moral
			//retorna valor para pasar a  siguiente formulario
			//si no es moral retorna valor para pasar al siguiente formulario
			//		});
			break;
		case "DataRegistro":
			GuardaRegistro()
			break;
			//nextForm.trigger('click', true);
	//	//	formRegistro.find('.form-control-sm').each(function () {
				//var elemento = this;
				//if (elemento.id == "REGCONTRALORIAFOLIO") {
				//	if (elemento.value == "") {
				//		console.log("Llenar No. Folio");
					
				//	}
				//} else if (elemento.id == "CAPITAL") {
				//	if (elemento.value == "") {
				//		console.log("LLenar Capital");
						
				//	}
				//} else {
				//	GuardaRegistro();
				//}
	//		});
	}
}
//--------------------------------------------//
//				Formularios validacion		//
//--------------------------------------------//
function GuardarNombre() {
	$.get('/Contratistas/GuardarNombreContratista', { Nombre: nombre.val()}, function (data) {
	})
}
function GuardarContratista() {
	var retorno = false;
	$.get('/Contratistas/GuardarContratista',
		{
			Nombre: nombre.val(), Rfc: rfc.val(), Curp: curp.val(), Telefono: telefono.val(), Correo: correo.val(), Calle: calle.val(), NoExterior: noExt.val(), NoInterior: noInt.val(), Colonia: colonia.val(), Cp: cp.val(), IdMunicipio: idMun.val(), Año: año.val()
		}, function (data) {
		const Toast = Swal.mixin({
			toast: true,
			position: 'center',
			timer: 1700,
			showConfirmButton: false
		})
		if (data == "1") {
			if (moral.prop('checked')) {
				
				$('#NombreCont').val(nombre.val());
				$('#staticBackdrop').modal('show');
			} else {
				Toast.fire({
					icon: 'success',
					title: 'Guardado'
				});	
				nombreReg.val(nombre.val());
				DdlRegistro();
				$('.next').trigger('click');
			}
			nombre.val = "";
			btnGuardarNombre.addClass('invisible');
			nombre.addClass('border-danger');
		} else {
			console.log("dentro")
			Toast.fire({
				icon: 'warning',
				title: data
			})
			
        }
	});
}
function GuardaMoral() {
	$.get('/Contratistas/GuardarMoral',
	{
		ActaConstitutiva: acta.val(), FechaActa: fechaActa.val(), NotarioNum: notarioNum.val(), NotarioNombre: notarioNom.val(), RegPublico: regPublico.val(), IdMunicipio: idMun2.val(), NombreCont: nombreCont.val()
	}, function(data) {
		const Toast = Swal.mixin({
			toast: true,
			position: 'center',
			timer: 1700,
			showConfirmButton: false
		})
		if (data == "1") {
			Toast.fire({
				icon: 'success',
				title: 'Guardado'
			});
			$('#staticBackdrop').modal('hide')
			DdlRegistro();
			nextForm.trigger('click');
			
		} else {
			Toast.fire({
				icon: 'warning',
				title: data
			});
			
        }
    })
}
function GuardaRegistro() {
	var chkactivo = false;
	if (activo.prop('checked')) {
		chkactivo = true	
	}
	$.get('/Contratistas/GuardarRegistro', {
		NombreCont: nombreReg.val(), RegContraloriaFolio: regContraloriaFolio.val(), RegContraloriaIngreso: regContraloriaIngreso.val(), FechaExpedicion: fechaExpedicion.val(), FechaVigencia: fechaVigencia.val(),
		Capital: capital.val(), IdInfoCapital: idInfoCapital.val(), FechaInf: fechaInf.val(), IdTipo: idTipo.val(), IdEspecialidad: idEspecialidad.val(),  Activo: chkactivo
	},
		function (data) {
			const Toast = Swal.mixin({
				toast: true,
				position: 'center',
				timer: 1700,
				showConfirmButton:false
			})
			if (data == "1") {
				Toast.fire({
					icon: 'success',
					title: 'Guardado'
				});
				nombreRep.val(nombreReg.val());
				DdlIdentificacion();

				$('#inv2').trigger('click');
			} else {
				Toast.fire({
					icon: 'warning',
					title: data
				});
				
            }
		})
}
function GuardaRepresentante() {
	
	$.get('/Contratistas/GuardarRepresentante', {
		Representada: representada.val(), Puesto: puesto.val(), NIden: numIden.val(), Acredita: idAcredita.val(), NAcredita: numAcredita.val(), IdIdentificacion: idIdentificacion.val(), nombreCont: nombreRep.val()
	},
		function (data) {
			const Toast = Swal.mixin({
				toast: true,
				position: 'center',
				timer: 1700,
				showConfirmButton: false
			});
			if (data == "1") {
				Toast.fire({
					icon: 'success',
					title: 'Guardado'
				});
				NombrePod.val(representada.val())
				$('#inv3').trigger('click');
			} else {
				Toast.fire({
					icon: 'warning',
					title: data
				});
            }
        }
		);
}
function GuardaPoder() {
	$.get('/Contratistas/GuardarPoder', {
		PoderRep: poderRep.val(), Fecha: fecha.val(), NotarioNo: notarioNo.val(), NotarioNombre: notarioNombre.val(), IdMunPoder: municipio.val(), NombrePod: NombrePod.val()
	},
		function (data) {
			const Toast = Swal.mixin({
				toast: true,
				position: 'center',
				timer: 1700,
				showConfirmButton: false
			});
			if (data == "1") {
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
        }
	)
}
//--------------------------------------------//
//				Botonoes Next y Prev		//
//--------------------------------------------//

//nextForm.on('click', function (e) {
//	e.preventDefault();
//});

//var botones = document.querySelectorAll('.btn');
//for (var i = 0; i < botones.length; i++) {
//	botones[i].addEventListener('click', FormularioValidacion);

//}

nextForm.click(function (e) {
	
	
		if (animating) return false;
		animating = true;

		current_fs = $(this).parent();
		next_fs = $(this).parent().next();
		//activate next step on progressbar using the index of next_fs
		progress.eq($("fieldset").index(next_fs)).addClass("active");

		//show the next fieldset
		next_fs.show();
		//hide the current fieldset with style
		current_fs.animate({ opacity: 0 }, {
			step: function (now, mx) {
				//as the opacity of current_fs reduces to 0 - stored in "now"
				//1. scale current_fs down to 80%
				scale = 1 - (1 - now) * 0.2;
				//2. bring next_fs from the right(50%)
				left = (now * 50) + "%";
				//3. increase opacity of next_fs to 1 as it moves in
				opacity = 1 - now;
				current_fs.css({
					'transform': 'scale(' + scale + ')',
					'position': 'absolute'
				});
				next_fs.css({ 'left': left, 'opacity': opacity });
			},
			duration: 800,
			complete: function () {
				current_fs.hide();
				animating = false;
			},
			//this comes from the custom easing plugin
			easing: 'easeInOutBack'
		});


});

prevForm.click(function () {
	if (animating) return false;
	animating = true;

	current_fs = $(this).parent();
	previous_fs = $(this).parent().prev();

	//de-activate current step on progressbar
	progress.eq($("fieldset").index(current_fs)).removeClass("active");

	//show the previous fieldset
	previous_fs.show();
	//hide the current fieldset with style
	current_fs.animate({ opacity: 0 }, {
		step: function (now, mx) {
			//as the opacity of current_fs reduces to 0 - stored in "now"
			//1. scale previous_fs from 80% to 100%
			scale = 0.8 + (1 - now) * 0.2;
			//2. take current_fs to the right(50%) - from 0%
			left = ((1 - now) * 50) + "%";
			//3. increase opacity of previous_fs to 1 as it moves in
			opacity = 1 - now;
			current_fs.css({ 'left': left });
			previous_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
		},
		duration: 800,
		complete: function () {
			current_fs.hide();
			animating = false;
		},
		//this comes from the custom easing plugin
		easing: 'easeInOutBack'
	});
});

$(".submit").click(function () {
	return false;
})


//--------------------------------------------//
//				Dropdown		//
//--------------------------------------------//
function DdlIdentificacion() {
	idAcredita.empty();
	idIdentificacion.empty();
	$.get("/Contratistas/listarIdentificacion", function (data) {
		$.each(data, function (index, row) {
			idAcredita.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
			idIdentificacion.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
        })
	})
}
function DdlRegistro() {


	idEspecialidad.empty();
	idTipo.empty();
	idInfoCapital.empty();
	$.get("/Contratistas/listarEspecialidad", function (data) {
		
		$.each(data, function (index, row) {

			idEspecialidad.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
		});

	});

	$.get("/Contratistas/listarTipo", function (data) {

		$.each(data, function (index, row) {

			idTipo.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
		});

	});
	$.get("/Contratistas/listarCapital", function (data) {

		$.each(data, function (index, row) {

			idInfoCapital.append("<option value='" + row.Value + "'>" + row.Text + "</option>")
		});

	});


}