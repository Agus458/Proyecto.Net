import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSalonComponent } from './edit-salon.component';

describe('EditSalonComponent', () => {
  let component: EditSalonComponent;
  let fixture: ComponentFixture<EditSalonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditSalonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSalonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
