function soloNumero(input) {
    // Elimino todo lo que no sea número
    input.value = input.value.replace(/[^0-9]/g, '');
    if (input.value.length > 20) {
        input.value = input.value.slice(0, 20); // Limita a 20 caracteres
    }
}

//visualizar la imagen seleccionada o tomada por el usuario
function previewImage(event) {
    var input = event.target;
    var reader = new FileReader();
    reader.onload = function () {
        var dataURL = reader.result;
        var output = document.getElementById('output');
        output.src = dataURL;
    };
    reader.readAsDataURL(input.files[0]);
}

function validarEmail(email) {
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

// visualizar un mensaje de error
function mostrarError(campo, mensaje) {
    var error = document.getElementById(campo + '-error');
    var input = document.querySelector(`[name="${campo}"]`);

    if (error) {
        error.textContent = mensaje;
    }
    if (input) {
        input.classList.add('input-error'); // Añade la clase de error
    }
}

// Elimina el mensaje de error y la clase 'input-error'
function limpiarError(campo) {
    var error = document.getElementById(campo + '-error');
    var input = document.querySelector(`[name="${campo}"]`);

    if (error) {
        error.textContent = '';
    }
    if (input) {
        input.classList.remove('input-error'); // Elimina la clase de error
    }
}

// Validar el formulario
function validarFormulario(event) {
    var formulario = document.getElementById('registroForm');
    var nombre = formulario.querySelector('input[name="registro.nombre"]');
    var identificador = formulario.querySelector('input[name="registro.codigo_pais"]');
    var telefono = formulario.querySelector('input[name="registro.telefono"]');
    var email = formulario.querySelector('input[name="registro.email"]');
    var contrasena = formulario.querySelector('input[name="registro.contrasena"]');
    var confirmarContrasena = formulario.querySelector('input[name="confirmar_contrasena"]');

    var valido = true;

    // Limpiar mensajes de error y clases de campos
    limpiarError('registro.nombre');
    limpiarError('registro.codigo_pais');
    limpiarError('registro.telefono');
    limpiarError('registro.email');
    limpiarError('registro.contrasena');
    limpiarError('confirmar_contrasena');

    // Validar Nombre
    if (!nombre.value.trim()) {
        mostrarError('registro.nombre', 'El campo Nombre es obligatorio.');
        valido = false;
    }

    // Validar Identificador
    if (!identificador.value.trim()) {
        mostrarError('registro.codigo_pais', 'El campo Identificador es obligatorio.');
        valido = false;
    }

    // Validar Teléfono
    if (!telefono.value.trim()) {
        mostrarError('registro.telefono', 'El campo Teléfono es obligatorio.');
        valido = false;
    }

    // Validar Email
    if (!validarEmail(email.value.trim())) {
        mostrarError('registro.email', 'El campo Email debe contener una dirección de correo válida.');
        valido = false;
    }

    // Validar Contraseña
    if (!contrasena.value.trim()) {
        mostrarError('registro.contrasena', 'El campo Contraseña es obligatorio.');
        valido = false;
    }

    // Validar Confirmar Contraseña
    if (contrasena.value.trim() !== confirmarContrasena.value.trim()) {
        mostrarError('confirmar_contrasena', 'Las contraseñas no coinciden.');
        valido = false;
    }

    if (!valido) {
        event.preventDefault(); // Evita el envío del formulario si no es válido
    }
}

// Añadir evento de validación al enviar el formulario
document.addEventListener('DOMContentLoaded', function () {
    var formulario = document.getElementById('registroForm');
    formulario.addEventListener('submit', validarFormulario);
});