import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventosInstitucionComponent } from './eventos-institucion.component';

describe('EventosInstitucionComponent', () => {
  let component: EventosInstitucionComponent;
  let fixture: ComponentFixture<EventosInstitucionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventosInstitucionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventosInstitucionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
