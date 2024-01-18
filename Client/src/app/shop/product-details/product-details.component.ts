import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/IProduct';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit{
    productId!:number;
    product?:IProduct;
    quantity:number = 1;
    quantityInBasket:number = 0;

    constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute,
        private bcService:BreadcrumbService,
        public basketService:BasketService
      ){
      const id = this.activatedRoute.snapshot.paramMap.get('id');
      if(id){
        this.productId = +id;
      }

      this.bcService.set('@productDetails',' ')

    }

    ngOnInit(): void {
      this.getProductDetails();
    }

    getProductDetails(){
        console.log(this.productId)
        this.shopService.getProductDetails(this.productId).subscribe((data:IProduct) => {
          this.product = data;
          this.bcService.set('@productDetails',this.product.name);
          this.getQuantityOfCurrentItem(this.productId);

          console.log(this.product)
        })
    }

    getQuantityOfCurrentItem(id:number){
      this.basketService.basketSource$.pipe(take(1)).subscribe((data) => {
        const item =  data?.items.find((x) => x.id === id);
        if(item){
          this.quantity = item.quantity;
          this.quantityInBasket = item.quantity;
        }
      })
    }

    incrementQuantity(){
      this.quantity += 1;
    }

    decrementQuantity(){
      this.quantity -= 1;
    }

    updateBasket(){
      if(this.product){
        // add to basket
        if(this.quantity > this.quantityInBasket){
          const itemToAdd = this.quantity - this.quantityInBasket;
          this.quantityInBasket += itemToAdd;
          this.basketService.addItemToBasket(this.product,itemToAdd);
        }else{
          // remove from basket

          const itemToRemove = this.quantityInBasket - this.quantity;
          this.quantityInBasket -= itemToRemove;
          this.basketService.removeItemFromBasket(this.product.id,itemToRemove);
        }
      }
    }

    get getButtonText(){
      return this.quantityInBasket === 0 ? 'Add To Basket' : 'Update Basket'
    }
}
