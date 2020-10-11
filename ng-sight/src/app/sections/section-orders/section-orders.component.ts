import { Component, OnInit } from '@angular/core';
import { Order } from '../../shared/Order';
import { orders } from '../../shared/orders';

@Component({
  selector: 'app-section-orders',
  templateUrl: './section-orders.component.html',
  styleUrls: ['./section-orders.component.css']
})
export class SectionOrdersComponent implements OnInit {

  constructor() { }

  orders:Order[]=orders;

  ngOnInit(): void {
  }

}
