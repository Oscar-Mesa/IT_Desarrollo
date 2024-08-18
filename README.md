# **Aplicativo para Registro de Usuarios y Gestión Administrativa**

Los requerimientos fueron separados por responsabilidades en un tablero de **Trello**, lo que permitió un mejor flujo de trabajo y conocer el estado de cada actividad.

<p align="center">
  <img src="https://github.com/user-attachments/assets/3a0847e1-58a1-4da0-badf-c32ade871f52" alt="trello" />
</p>
---

### **Diagrama Entidad-Relación**
La primera responsabilidad fue definir un **diagrama entidad-relación** para saber cómo trabajar con las entidades y cómo sería su respectivo mapeo junto a las demás, utilizando la técnica **Code First Migrations** dentro de **Entity Framework**.

<p align="center">
  <img src="https://github.com/user-attachments/assets/6ea6a094-d047-4478-b1dc-6ea68c3b9f24" alt="der" />
</p>

---

### **Estructura del Proyecto**
La solución cuenta con dos proyectos para una separación de responsabilidades más adecuada.

<p align="center">
  <img src="https://github.com/user-attachments/assets/ae01c972-0cec-4296-a5d8-0b05f4b1482d" alt="vs" />
</p>

---

### **Back End**
En el **Back End**, se implementó una **API Rest** desarrollada en **ASP.NET Core Web API**, con algunos endpoints protegidos por **JWT**, lo que asegura que solo ciertos usuarios pueden acceder a los datos almacenados, aumentando la seguridad. La documentación de la API se realizó con **Swagger** para facilitar la legibilidad y acceso.

<p align="center">
  <img src="https://github.com/user-attachments/assets/e4789ff7-25c7-402f-8d81-0ac27032f918" alt="swagger" />
</p>

La base de datos utilizada es **PostgreSQL**.
<p align="center">
  <img src="https://github.com/user-attachments/assets/17a7d3a3-0519-4c65-9189-cabebd6550c3" alt="swagger" />
</p>
---

### **Front End**
El **Front End** cuenta con 6 vistas desarrolladas en **ASP.NET Core (MVC)**, utilizando **Bootstrap**, **Razor Pages**, **CSS** y **JavaScript**. La primera vista es la de inicio de sesión.

<p align="center">
  <img src="https://github.com/user-attachments/assets/ee417cc0-3002-4607-bb6c-aa4cfc8ad513" alt="login" />
</p>

**Nota**: Para agilizar el proceso, no se hicieron bosquejos previos en **Figma**, y se utilizaron diseños genéricos para cumplir con los plazos estipulados.

<p align="center">
  <img src="https://github.com/user-attachments/assets/0c96076d-7a79-47dc-b519-a6d81a60f470" alt="figma" />
</p>

---

### **Registro de Usuarios**
En la vista de registro, el usuario puede omitir las preguntas sin asterisco (como "Apellido").

<p align="center">
  <img src="https://github.com/user-attachments/assets/090579e4-f100-480f-9130-6894dc1ec0ed" alt="registro" 
</p>

### **Registro Exitoso**
Una vez que el formulario pasa los filtros de aceptación, la vista de registro exitoso se muestra al usuario.

<p align="center">
  <img src="https://github.com/user-attachments/assets/2f33f69d-b6e7-4149-9575-19268dd1cb6e" alt="registroexitoso" />
</p>

### **Correo de Confirmación**
Al registrarse, se envía un correo de confirmación y agradecimiento al usuario.

<p align="center">
  <img src="https://github.com/user-attachments/assets/b5c292fe-2276-4cf8-afae-a73f101a765d" alt="correo" />
</p>

---

### **Perfil de Usuario**
Después de iniciar sesión, el usuario puede ver su perfil con todos sus datos y foto, además de las preguntas de gustos personales respondidas en el formulario.

<p align="center">
  <img src="https://github.com/user-attachments/assets/ffc1a754-137d-4dcb-8803-e7425b48576c" alt="perfil1" />
  <img src="https://github.com/user-attachments/assets/c52ea2ca-0b80-4147-b216-074a8f54270b" alt="perfil2" />
</p>

---

### **Panel Administrativo**
El panel administrativo permite visualizar todos los usuarios registrados y sus datos. Las preguntas de gustos personales se mantienen persistentes cuando el administrador realiza modificaciones. Además, los cambios realizados en las preguntas personalizadas se reflejan automáticamente en el formulario de registro.

<p align="center">
  <img src="https://github.com/user-attachments/assets/556fdb16-e0d4-4c7e-a7e9-82618557b219" alt="adm1" />
  <img src="https://github.com/user-attachments/assets/6f99d6ea-7671-4e4a-b785-78adf3267286" alt="adm2" />
</p>

---

### **Vista de Seguridad**
Se incluye una vista de seguridad para bloquear el acceso a usuarios sin los permisos adecuados.

<p align="center">
  <img src="https://github.com/user-attachments/assets/e0b228aa-ef9f-462d-8b13-895c49adca22" alt="noauth" />
</p>

---




