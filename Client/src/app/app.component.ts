import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from './basket/basket.service';
import { IPagination } from './shared/models/IPagination';
import { IProduct } from './shared/models/IProduct';
import { ShopService } from './shop/shop.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private basketService:BasketService){

  }

  ngOnInit(): void {
    const basketId = localStorage.getItem("basketId");
    if(basketId){
      this.basketService.getBasket(basketId);      
    }
  }

}
