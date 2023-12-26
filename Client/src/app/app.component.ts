import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Client';

  products:any = [];
  constructor(private http:HttpClient){

  }

  ngOnInit(): void {
      this.http.get('http://localhost:5001/api/Product/products').subscribe((data:any) =>{
        this.products = data?.data;
      },(error) =>{
        console.log('something went wrong while fetching products')
      })
  }
}
