import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MostrarProductosPrecioComponent } from './mostrar-productos-precio.component';

describe('MostrarProductosPrecioComponent', () => {
  let component: MostrarProductosPrecioComponent;
  let fixture: ComponentFixture<MostrarProductosPrecioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MostrarProductosPrecioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MostrarProductosPrecioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
