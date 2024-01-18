import { Component, OnInit } from '@angular/core';
import { faCoffee } from '@fortawesome/free-solid-svg-icons';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/models/Basket';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit{

  faCoffee=faCoffee;

  constructor(public basketService:BasketService){

  }

  ngOnInit(): void {
          
  }

  getCount(items:IBasketItem[]){
    return items.reduce((sum,item) => sum + item.quantity,0)
  }

}
