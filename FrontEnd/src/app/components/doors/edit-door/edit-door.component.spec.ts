import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDoorComponent } from './edit-door.component';

describe('EditDoorComponent', () => {
  let component: EditDoorComponent;
  let fixture: ComponentFixture<EditDoorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditDoorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditDoorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
