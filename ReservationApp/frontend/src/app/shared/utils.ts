export function generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (char) {
      const random = Math.random() * 16 | 0; // Número aleatorio entre 0 y 15
      const value = char === 'x' ? random : (random & 0x3 | 0x8); // Garantiza el formato correcto
      return value.toString(16); // Convierte a hexadecimal
    });
  }
  