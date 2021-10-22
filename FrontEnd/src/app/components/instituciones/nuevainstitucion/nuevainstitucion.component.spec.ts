import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NuevainstitucionComponent } from './nuevainstitucion.component';

describe('NuevainstitucionComponent', () => {
  let component: NuevainstitucionComponent;
  let fixture: ComponentFixture<NuevainstitucionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NuevainstitucionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NuevainstitucionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
