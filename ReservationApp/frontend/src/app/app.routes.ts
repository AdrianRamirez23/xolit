import { Routes } from '@angular/router';
import { CalendarComponent } from './reservations/calendar/calendar.component';
import { ReservationFormComponent } from './reservations/reservation-form/reservation-form.component';
import { ReservationFormHomeComponent } from './reservations/reservations-home/reservations-home.component';

export const routes: Routes  = [
    { path: '', component: ReservationFormHomeComponent },
    { path: 'calendar', component: CalendarComponent },
    { path: 'new-reservation', component: ReservationFormComponent },
  ];