import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../reservation.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { generateGuid } from '../../shared/utils';

@Component({
  selector: 'app-reservations-home',
  imports: [FormsModule, CommonModule],
  providers: [ReservationService], // Agrega el servicio aquí si no estás usando `providedIn: 'root'`
  templateUrl: './reservations-home.component.html',
  styleUrl: './reservations-home.component.scss'
})
export class ReservationFormHomeComponent implements OnInit {
  reservations: any[] = [];
  spaces: any[] = [];
  reservationForm = {
    id : '',
    spaceId: '',
    startDate: '',
    endDate: '',
    userId: 'e6ebaf6b-82ca-4bf3-be74-1306c77b53ba'
  };

  constructor(private reservationService: ReservationService) {}

  ngOnInit(): void {
    this.loadReservations();
    this.loadSpaces();
  }

  loadReservations(): void {
    this.reservationForm.id = generateGuid();
    this.reservationService.getReservations().subscribe((data) => {
      this.reservations = data;
    });
  }

  loadSpaces(): void {
    this.reservationService.getSpaces().subscribe((data) => {
      this.spaces = data;
    });
  }

  createReservation(): void {
    if (!this.reservationForm.spaceId || !this.reservationForm.startDate || !this.reservationForm.endDate) {
      alert('Todos los campos son obligatorios.');
      return;
    }

    const { spaceId, startDate, endDate } = this.reservationForm;
    if (new Date(startDate) >= new Date(endDate)) {
      alert('La fecha de inicio debe ser anterior a la fecha de fin.');
      return;
    }
    this.reservationForm.startDate = new Date(this.reservationForm.startDate).toISOString();
    this.reservationForm.endDate = new Date(this.reservationForm.endDate).toISOString();
    this.reservationService.createReservation(this.reservationForm).subscribe(() => {
      alert('Reserva creada con éxito');
      this.reservationForm = { id: '', spaceId: '', startDate: '', endDate: '', userId: 'e6ebaf6b-82ca-4bf3-be74-1306c77b53ba' };
      this.loadReservations(); // Actualizar la lista de reservas
    });
  }

  cancelReservation(id: string): void {
    this.reservationService.cancelReservation(id).subscribe(() => {
      alert('Reserva cancelada con éxito');
      this.loadReservations();
    });
  }
}