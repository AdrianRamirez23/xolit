import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsHomeComponent } from './reservations-home.component';

describe('ReservationsHomeComponent', () => {
  let component: ReservationsHomeComponent;
  let fixture: ComponentFixture<ReservationsHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationsHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationsHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
