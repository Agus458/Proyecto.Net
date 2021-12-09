import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditNovedadesComponent } from './edit-novedades.component';

describe('EditNovedadesComponent', () => {
  let component: EditNovedadesComponent;
  let fixture: ComponentFixture<EditNovedadesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditNovedadesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditNovedadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
