//jQuery time

// BOTONES DE NEXT Y PREV
var current_fs, next_fs, previous_fs; //fieldsets
var left, opacity, scale; //fieldset properties which we will animate
var animating; //flag to prevent quick multi-click glitches
var nextForm = $(".next");
nextForm.on('click', function (e) {
	e.preventDefault();
});
var prevForm = $(".previous");
var progress = $("#progressbar li");


/// VALIDACION DE CAMPOS A GUARDAR

	var botones = document.querySelectorAll('.btn');

	for (var i = 0; i < botones.length; i++) {
		botones[i].addEventListener('click', CamposValidacion);
		
	}


var formGenerales = $('#Generales');
//var formRegistro = $('#Registro');
//var formRepresenta = $('#Representa');
//var chkMoral = $('#IsMoral');

function CamposValidacion(evento) {
//	// cada vez que clickas un boton. Automaticamente se invoca con un parametro
//	// que es el Evento. el cual tiene una propiedad (entre otras) llamada target
//	// que es el elemento que dispara el evento. Luego buscas el id en target.
	  
	switch (evento.target.id) {
		case "DataGeneral":
			formGenerales.find('.form-control-sm').each(function () {
				var elemento = this;
				//		console.log("elemento.id=" + elemento.id + ", elemento.value=" + elemento.value); 
				if (elemento.id == "Rfc") {
					if (elemento.value == "") {
						console.log("Llenar RFC");
					}
				} else if (elemento.id == "Telefono") {
					if (elemento.value == "") {
						console.log("LLenar TELEFONO")
					}
					else {
						//guardar
						//antes de pasar verifica si es moral
						//si es moral despliega modal para llenar datos
						//verifica que llene datos moral
						//retorna valor para pasar a  siguiente formulario
						//si no es moral retorna valor para pasar al siguiente formulario
						nextForm.trigger('click', 1);

					}
				}
			});
			break;
		case "DataRegistro":
			formRegistro.find('.form-control-sm').each(function () {
				var elemento = this;
				if (elemento.id == "REGCONTRALORIAFOLIO") {
					if (elemento.value == "") {
						console.log("Llenar No. Folio");
					
					}
				} else if (elemento.id == "CAPITAL") {
					if (elemento.value == "") {
						console.log("LLenar Capital");
						
					}
				} else {
					//guardar
				}
			});
	}
}


nextForm.click(function (e, data) {
	
	e.preventDefault();
	if (data == 1) {
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
    }

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
