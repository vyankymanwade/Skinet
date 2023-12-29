import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/IBrand';
import { IPagination } from '../shared/models/IPagination';
import { IProduct } from '../shared/models/IProduct';
import { IType } from '../shared/models/IType';
import { ShopParams } from '../shared/models/ShopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'http://localhost:5001/api/';

  constructor(private http:HttpClient) { }

  getProducts(shopParams:ShopParams){

    let params = new HttpParams();

    if(shopParams.brandId) params = params.append('brandId',shopParams.brandId);
    if(shopParams.typeId) params = params.append('typeId',shopParams.typeId);
    if(shopParams.search) params = params.append('search',shopParams.search);
    params = params.append('sort',shopParams.sort);
    params = params.append('pageIndex',shopParams.pageNumber);
    params = params.append('pageSize',shopParams.pageSize);


    return this.http.get<IPagination<IProduct>>(`${this.baseUrl}Product/Products`,{params});
  }

  getBrands(){
    return this.http.get<IBrand[]>(`${this.baseUrl}Product/brands`);
  }

  getTypes(){
    return this.http.get<IType[]>(`${this.baseUrl}Product/types`);
  }
}
