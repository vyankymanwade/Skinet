import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { OrderSummaryComponent } from './order-summary/order-summary.component';


@NgModule({
  declarations: [
    OrderSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  exports:[PaginationModule,OrderSummaryComponent]
})
export class SharedModule { }
