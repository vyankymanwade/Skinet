import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/IBrand';
import { IPagination } from '../shared/models/IPagination';
import { IProduct } from '../shared/models/IProduct';
import { IType } from '../shared/models/IType';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'http://localhost:5001/api/';

  constructor(private http:HttpClient) { }

  getProducts(selectedBrandId?:number,selectedTypeId?:number,selectedSortoption?:string){

    let params = new HttpParams();

    if(selectedBrandId) params = params.append('brandId',selectedBrandId);
    if(selectedTypeId) params = params.append('typeId',selectedTypeId);
    if(selectedSortoption) params = params.append('sort',selectedSortoption);

    return this.http.get<IPagination<IProduct>>(`${this.baseUrl}Product/Products`,{params});
  }

  getBrands(){
    return this.http.get<IBrand[]>(`${this.baseUrl}Product/brands`);
  }

  getTypes(){
    return this.http.get<IType[]>(`${this.baseUrl}Product/types`);
  }
}
