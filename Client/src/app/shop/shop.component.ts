import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/IBrand';
import { IProduct } from '../shared/models/IProduct';
import { IType } from '../shared/models/IType';
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

  selectedBrandId?:number = 0;
  selectedTypeId?:number = 0;
  selectedSortOption:string = 'name';

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
    this.shopService.getProducts(this.selectedBrandId,this.selectedTypeId,this.selectedSortOption).subscribe((data) => {
      this.products = data.data;
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
    this.selectedBrandId = brandId;

    this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.selectedTypeId = typeId;

    this.getProducts();
  }

  onFilterChange(event:any){
    this.selectedSortOption = event.target.value;
    this.getProducts();
  }
}
