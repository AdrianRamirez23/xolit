import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  HttpClientModule } from '@angular/common/http';
import { ReservationService } from './reservation.service';
import { ReservationFormHomeComponent } from './reservations-home/reservations-home.component';





@NgModule({
  declarations: [
   
  ],
  imports: [
    CommonModule,
    FormsModule, // Agrega FormsModule aquí
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    ReservationService, // Agrega el servicio si no estás usando `providedIn: 'root'`
  ],
  exports: [
    
  ],
})
export class ReservationsModule { }
