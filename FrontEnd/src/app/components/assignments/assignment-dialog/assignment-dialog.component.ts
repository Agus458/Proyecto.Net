import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-assignment-dialog',
  templateUrl: './assignment-dialog.component.html',
  styleUrls: ['./assignment-dialog.component.css']
})
export class AssignmentDialogComponent implements OnInit {

  @Output() closeEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  close() {
    this.closeEvent.emit();
  }

}
