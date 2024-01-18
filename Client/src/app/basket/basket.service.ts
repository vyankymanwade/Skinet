import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, QueryList } from '@angular/core';
import { environment } from 'environment/environment';
import { BehaviorSubject } from 'rxjs';
import { Basket, BasketTotal, IBasket, IBasketItem } from '../shared/models/Basket';
import { IProduct } from '../shared/models/IProduct';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl:string = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket | null>(null);
  basketSource$ = this.basketSource.asObservable();


  private basketTotalSource = new BehaviorSubject<BasketTotal | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http:HttpClient) { }

  getBasket(basketId:string){

    let params = new HttpParams();
    params = params.append('basketId',basketId);

    return this.http.get<IBasket>(`${this.baseUrl}basket/basket`,{params}).subscribe((basket) =>{
      this.basketSource.next(basket);
      this.calculateBasketTotal();
    });
  }

  setBasket(basket:IBasket){
    return this.http.post<IBasket>(`${this.baseUrl}basket/add-basket`,basket).subscribe((basket) =>{
      this.basketSource.next(basket);
      this.calculateBasketTotal();
    });
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item:IProduct | IBasketItem,quality = 1){
    if(this.isProduct(item)) item = this.mapProductToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpadteBasket(basket.items,item,quality)

    this.setBasket(basket);
  }

  removeItemFromBasket(id:number,quantity = 1){
    const basket = this.getCurrentBasketValue();

    if(!basket) return;

    const item = basket.items.find((x) => x.id === id);

    if(item){
      item.quantity -= quantity;
      if(item.quantity === 0){
        basket.items = basket.items.filter((x) => x.id !== id);
      }

      if(basket.items.length > 0){
        this.setBasket(basket);
      }else{
        this.deleteBasket(basket);
      }
    }

  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(`${this.baseUrl}basket/delete-basket?basketId=${basket.id}`).subscribe((data) =>{
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basketId');
    });
  }

  addOrUpadteBasket(baskets: IBasketItem[], itemToAdd: IBasketItem, quality: number): IBasketItem[] {
     const item = baskets.find((x) => x.id == itemToAdd.id);

     if(item){
       item.quantity += quality;
     }else{
       itemToAdd.quantity += quality;
       baskets.push(itemToAdd);
     }

     return baskets;
  }

  private createBasket(): IBasket{
    const basket = new Basket();
    localStorage.setItem('basketId',basket.id);
    return basket;
  }

  private mapProductToBasketItem(item:IProduct):IBasketItem{
    return {
      id : item.id,
      brand : item.productBrand,
      productName : item.name,
      pictureUrl : item.pictureUrl,
      price : item.price,
      quantity : 1,
      type : item.productType
    }
  }

  private calculateBasketTotal(){
    const basket = this.getCurrentBasketValue();

    if(!basket) return;

    const shipping = 0;
    const subtotal = basket.items.reduce((subtotal,item) => {
      return (item.price * item.quantity) + subtotal
    },0);

    const total = shipping + subtotal;

    this.basketTotalSource.next({shipping,subtotal,total});
    
  }

  private isProduct(item : IProduct | IBasketItem): item is IProduct{
    return (item as IProduct).productType !== undefined;
  }
}


