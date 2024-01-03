import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  requestCount:number = 0;
  constructor(private ngxSpinnerService:NgxSpinnerService) { }

  busy(){
    this.requestCount++;

    this.ngxSpinnerService.show(undefined,{
      type:'timer',
      bdColor:'rgba(255,255,255,0.7)',
      color:'#333333'
    });
  }

  idle(){
    this.requestCount--;

    if(this.requestCount <= 0){
      this.requestCount = 0;
      this.ngxSpinnerService.hide();
    }
  }
}
