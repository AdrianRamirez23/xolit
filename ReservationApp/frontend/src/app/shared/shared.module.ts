import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationsModule } from '../reservations/reservations.module';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReservationsModule,
    HttpClientModule,
  ]
})
export class SharedModule { }
