import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/IProduct';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit{

  products:IProduct[] = [];

  constructor(private shopService:ShopService){}

  ngOnInit(): void {
    this.shopService.getProducts().subscribe((data) => {
      this.products = data.data;
    },(error) =>{
      console.log('something went wrong while fetching products')
    })
  }
  
}
