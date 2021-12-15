import { Component, OnInit, ViewChild, ElementRef} from '@angular/core';
import { render } from 'creditcardpayments/creditCardPayments';
@Component({
  selector: 'app-paypal',
  templateUrl: './paypal.component.html',
  styleUrls: ['./paypal.component.css']
})
export class PaypalComponent implements OnInit {
  @ViewChild('paypal', { static: true }) paypalElement: ElementRef;

  
  constructor(
   
  ) { render({
    id: "#payments",
    currency: "USD",
    value: "100.00",
    onApprove:(details) => {
      alert("Transaction Sucessfull")
    }

  }
  );
}

  ngOnInit(): void {
  }

}
