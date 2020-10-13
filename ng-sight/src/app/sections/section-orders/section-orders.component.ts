import { Component, OnInit } from '@angular/core';
import { Order } from '../../shared/order';
import { SalesDataService } from '../../services/SalesDataService';
import { ServerService } from 'src/app/services/server.service';

@Component({
  selector: 'app-section-orders',
  templateUrl: './section-orders.component.html',
  styleUrls: ['./section-orders.component.css']
})
export class SectionOrdersComponent implements OnInit {

  constructor(private _salesData: SalesDataService) { }

  orders: Order[];
  total = 0;
  page = 1;
  limit = 10;
  loading = false;

  ngOnInit() {
    this.getOrders();
  }

  async getOrders() {
    this._salesData.getOrders(this.page, this.limit);
    while(this._salesData.root ==  undefined) 
      await this._salesData.delay(100);
    this.orders = this._salesData.root.page.data;
    this.total = this._salesData.root.page.total;
    this.loading = false;
  }
  goToPrevious(): void {
    this.page--;
    this.getOrders();
  }

  goToNext(): void {
    this.page++;
    this.getOrders();
  }

  goToPage(n: number): void {
    this.page = n;
    this.getOrders();
  }
}
