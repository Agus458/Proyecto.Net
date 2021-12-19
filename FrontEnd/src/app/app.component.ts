import { Component } from '@angular/core';
import { ToastService } from './services/toast/toast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AccessApp';

  constructor(
    public toastService: ToastService
  ) { }
}
