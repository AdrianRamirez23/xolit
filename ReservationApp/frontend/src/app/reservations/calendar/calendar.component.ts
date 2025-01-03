import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../reservation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-calendar',
  imports: [CommonModule],
  providers: [ReservationService], // Agrega el servicio aquí si no estás usando `providedIn: 'root'`
  templateUrl: './calendar.component.html',
  styleUrl: './calendar.component.scss'
})
export class CalendarComponent implements OnInit {
  reservations: any[] = [];

  constructor(private reservationService: ReservationService) {}

  ngOnInit(): void {
    this.loadReservations();
  }

  loadReservations(): void {
    this.reservationService.getReservations().subscribe((data) => {
      this.reservations = data;
    });
  }

  editReservation(reservation: any): void {
    // Navegar o abrir modal para editar
  }

  cancelReservation(id: string): void {
    this.reservationService.cancelReservation(id).subscribe(() => {
      this.loadReservations();
    });
  }
}
