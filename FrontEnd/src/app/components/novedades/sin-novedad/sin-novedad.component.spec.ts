import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SinNovedadComponent } from './sin-novedad.component';

describe('SinNovedadComponent', () => {
  let component: SinNovedadComponent;
  let fixture: ComponentFixture<SinNovedadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SinNovedadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SinNovedadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
