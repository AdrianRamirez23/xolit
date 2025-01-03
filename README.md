# ReservationApp

Sistema de reservas para gestionar espacios compartidos. Este proyecto incluye un backend desarrollado en .NET y un frontend en Angular, implementados con buenas prácticas de desarrollo y diseño arquitectónico.

---

## **Índice**
1. [Descripción General](#descripción-general)
2. [Decisiones Arquitectónicas](#decisiones-arquitectónicas)
   - [Backend](#backend)
   - [Frontend](#frontend)
3. [Componentes del Sistema](#componentes-del-sistema)
4. [Configuración del Proyecto](#configuración-del-proyecto)
   - [Backend](#configuración-backend)
   - [Frontend](#configuración-frontend)
5. [Flujo de Trabajo](#flujo-de-trabajo)
6. [Consideraciones Técnicas](#consideraciones-técnicas)

---

## **Descripción General**

ReservationApp es una solución web moderna para la gestión de reservas en espacios compartidos. Los usuarios pueden:
- Crear reservas asegurando la no superposición de horarios.
- Cancelar reservas existentes.
- Visualizar todas las reservas filtradas por espacio, usuario y rango de fechas.

Este proyecto fue diseñado con **arquitectura hexagonal** en el backend y un enfoque modular en el frontend para garantizar flexibilidad y escalabilidad.

---

## **Decisiones Arquitectónicas**

### **Backend**
1. **Arquitectura Hexagonal**:
   - **Motivo**: Facilitar el desacoplamiento entre la lógica de negocio, infraestructura y APIs externas.
   - **Ventajas**:
     - Mejora el mantenimiento y las pruebas unitarias.
     - Permite cambiar tecnologías subyacentes sin afectar la lógica de negocio.

2. **Patrones Implementados**:
   - **Repositorio**: Gestiona el acceso centralizado a datos.
   - **Validación con FluentValidation**: Centraliza las reglas de negocio reutilizables.

3. **Base de Datos**:
   - **PostgreSQL**: Elegida por su capacidad avanzada para manejar tipos de datos como `timestamp with time zone`.

---

### **Frontend**
1. **Angular Modular**:
   - **Motivo**: Facilitar la escalabilidad y la colaboración mediante la separación de funcionalidades en módulos.

2. **Diseño Responsivo**:
   - Uso de **Bootstrap** para asegurar una experiencia visual consistente.

3. **Navegación**:
   - Implementada con `RouterModule` y un menú interactivo en `app.component.html`.

---

## **Componentes del Sistema**

### **Backend**
- **Controladores**:
  - `ReservationController`: Crear, listar y cancelar reservas.
  - `SpaceController`: Gestionar espacios.
  - `UserController`: Operaciones relacionadas con usuarios.
- **Repositorios**:
  - `ReservationRepository`: Acceso a datos para reservas.
  - `SpaceRepository`: Acceso a datos para espacios.
  - `UserRepository`: Acceso a datos para usuarios.
- **Validadores**:
  - Validación de reglas de negocio usando FluentValidation.

### **Frontend**
- **Componentes**:
  - `AppComponent`: Componente raíz con menú de navegación.
  - `CalendarComponent`: Muestra reservas existentes en formato calendario.
  - `ReservationFormComponent`: Formulario para crear nuevas reservas.
  - `ReservationFormHomeComponent`: Página de inicio.
- **Servicios**:
  - `ReservationService`: Consume APIs del backend para reservas.

---

## **Configuración del Proyecto**

### **Backend**

1. **Requisitos**:
   - .NET 6 o superior.
   - PostgreSQL.

2. **Pasos**:
   - Clonar el repositorio:
     ```bash
     git clone https://github.com/AdrianRamirez23/xolit.git
     cd ReservationApp/backend
     ```
   - Configurar la base de datos en `appsettings.json`.
   - Ejecutar migraciones:
     ```bash
     dotnet ef database update
     ```
   - Iniciar el servidor:
     ```bash
     dotnet run
     ```

---

### **Frontend**

1. **Requisitos**:
   - Node.js 16 o superior.
   - Angular CLI.

2. **Pasos**:
   - Clonar el repositorio:
     ```bash
       git clone https://github.com/AdrianRamirez23/xolit.git
     cd ReservationApp/frontend
     ```
   - Instalar dependencias:
     ```bash
     npm install
     ```
   - Iniciar la aplicación:
     ```bash
     ng serve
     ```

---

## **Flujo de Trabajo**

1. **Crear Reserva**:
   - Navegar a `/new-reservation` en el frontend.
   - Completar el formulario con el espacio, fecha y hora.
   - Validación en el frontend y backend para evitar conflictos.
   - Reserva guardada en la base de datos y visible en el calendario.

2. **Visualizar Reservas**:
   - Navegar a `/calendar`.
   - Ver reservas existentes con detalles de espacio y usuario.

3. **Cancelar Reserva**:
   - Seleccionar una reserva en el calendario y eliminarla.

---

## **Consideraciones Técnicas**

1. **CORS**:
   - Configurado en el backend para permitir solicitudes desde `http://localhost:4200`.

2. **Validaciones**:
   - Implementadas en el frontend (formularios reactivos) y backend (FluentValidation).

3. **Serialización JSON**:
   - Configurado con `ReferenceHandler.Preserve` para manejar referencias cíclicas entre `Reservation` y `Space`.

4. **Pruebas**:
   - **Backend**: Pruebas unitarias para repositorios y validaciones.
   - **Frontend**: Validación manual del flujo de usuario.


