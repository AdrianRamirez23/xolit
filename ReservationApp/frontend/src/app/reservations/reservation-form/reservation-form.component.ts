import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../reservation.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { generateGuid } from '../../shared/utils';

@Component({
  selector: 'app-reservation-form',
  imports: [ReactiveFormsModule, CommonModule],
  providers: [ReservationService], // Agrega el servicio aquí si no estás usando `providedIn: 'root'`
  templateUrl: './reservation-form.component.html',
  styleUrl: './reservation-form.component.scss'
})
export class ReservationFormComponent implements OnInit {
  reservationForm: FormGroup;
  spaces: any[] = [];

  constructor(private fb: FormBuilder, private reservationService: ReservationService) {
    this.reservationForm = this.fb.group({
      id : '',
      spaceId: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', [Validators.required]],
       userId: 'e6ebaf6b-82ca-4bf3-be74-1306c77b53ba'
    });
  }

  ngOnInit(): void {
    this.loadSpaces();
  }

  loadSpaces(): void {
    
    this.reservationService.getSpaces().subscribe((data) => {
      this.spaces = data;
    });
  }

  onSubmit(): void {
    if (this.reservationForm.valid) {
      this.reservationForm.value.id = generateGuid();
      this.reservationForm.value.startDate = new Date(this.reservationForm.value.startDate).toISOString();
      this.reservationForm.value.endDate = new Date(this.reservationForm.value.endDate).toISOString();
      this.reservationService.createReservation(this.reservationForm.value).subscribe(() => {
        alert('Reserva creada con éxito');
        this.reservationForm.reset();
      });
    }
  }
}
