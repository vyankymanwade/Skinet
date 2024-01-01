import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit{
    productId!:number;
    product?:IProduct;

    constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute){
      const id = this.activatedRoute.snapshot.paramMap.get('id');
      if(id){
        this.productId = +id;
      }
    }

    ngOnInit(): void {
      this.getProductDetails();
    }

    getProductDetails(){
        console.log(this.productId)
        this.shopService.getProductDetails(this.productId).subscribe((data:IProduct) => {
          this.product = data;
          console.log(this.product)
        })
    }
}
