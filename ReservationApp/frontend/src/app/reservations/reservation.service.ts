import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private apiUrl = 'https://localhost:7122/api/Reservations'; // URL del endpoint de reservas
  private spacesApiUrl = 'https://localhost:7122/api/Spaces'; // URL del endpoint de espacios

  constructor(private http: HttpClient) {}

  /**
   * Obtiene todas las reservas desde el servidor.
   * @returns Observable con la lista de reservas.
   */
  getReservations(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  /**
   * Obtiene la lista de espacios disponibles desde el servidor.
   * @returns Observable con la lista de espacios.
   */
  getSpaces(): Observable<any[]> {
    return this.http.get<any[]>(this.spacesApiUrl);
  }

  /**
   * Crea una nueva reserva.
   * @param reservation Objeto con los datos de la nueva reserva.
   * @returns Observable que confirma la creación.
   */
  createReservation(reservation: any): Observable<void> {
    return this.http.post<void>(this.apiUrl, reservation);
  }

  /**
   * Cancela una reserva existente.
   * @param id ID de la reserva a cancelar.
   * @returns Observable que confirma la cancelación.
   */
  cancelReservation(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
