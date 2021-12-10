import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagoVentanaComponent } from './pago-ventana.component';

describe('PagoVentanaComponent', () => {
  let component: PagoVentanaComponent;
  let fixture: ComponentFixture<PagoVentanaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagoVentanaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagoVentanaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
