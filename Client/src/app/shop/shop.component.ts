import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/IBrand';
import { IProduct } from '../shared/models/IProduct';
import { IType } from '../shared/models/IType';
import { ShopParams } from '../shared/models/ShopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit{

  products:IProduct[] = [];
  brands:IBrand[] = [];
  types:IType[] = [];

  totalCount : number = 0;

  shopParams:ShopParams = new ShopParams();

  sortOptions = [
    {name:'Alphabetical',value:'name'},
    {name:'Price: Low To High',value:'priceAsc'},
    {name:'Price: High To Low',value:'priceDesc'},
  ]

  constructor(private shopService:ShopService){}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }
  
  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe((data) => {
      this.products = data.data;
      this.shopParams.pageNumber = data.pageIndex;
      this.shopParams.pageSize = data.pageSize;
      this.totalCount = data.count;
    },(error) =>{
      console.log('something went wrong while fetching products')
    })
  }

  getBrands(){
    this.shopService.getBrands().subscribe((data) =>{
      this.brands = [{id:0,name:'All'},...data]; 
    },(error) =>{
      console.log('something went wrong while fetching brands')
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe((data) =>{
      this.types = [{id:0,name:'All'},...data];
    },(error) =>{
      console.log('something went wrong while fetching types')
    })
  }

  onBrandSelected(brandId:number){
    this.shopParams.brandId = brandId;

    this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.shopParams.typeId = typeId;

    this.getProducts();
  }

  onFilterChange(event:any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  onSearch(){
    this.getProducts();
  }

  onReset(){
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
