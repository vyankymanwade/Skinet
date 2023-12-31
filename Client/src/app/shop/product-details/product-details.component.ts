import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

    constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute,
        private bcService:BreadcrumbService
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
          this.bcService.set('@productDetails',this.product.name)
          console.log(this.product)
        })
    }
}
