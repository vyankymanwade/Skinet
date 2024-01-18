import { Component } from '@angular/core';
import { IBasketItem } from '../shared/models/Basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent {

  constructor(public basketService:BasketService){}

  increaseQuantity(item:IBasketItem){
    this.basketService.addItemToBasket(item);
  }

  removeItem(id:number,quantity:number){
    this.basketService.removeItemFromBasket(id,quantity);
  }
}
