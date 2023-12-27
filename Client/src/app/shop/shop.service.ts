import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/IPagination';
import { IProduct } from '../shared/models/IProduct';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'http://localhost:5001/api/';

  constructor(private http:HttpClient) { }

  getProducts(){
    return this.http.get<IPagination<IProduct>>(`${this.baseUrl}Product/Products?pageSize=50`);
  }
}
