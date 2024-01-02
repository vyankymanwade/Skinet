import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'environment/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {
  baseUrl:string = environment.apiUrl;

  validationError:string[] = [];

  constructor(private http:HttpClient) {
    
  }

  get404Error(){
    this.http.get(`${this.baseUrl}Buggy/notfound`).subscribe((data) => {

    },(error) =>{
      console.log(error);
    })
  }

  get400Error(){
    this.http.get(`${this.baseUrl}Buggy/badrequest`).subscribe((data) => {

    },(error) =>{
      console.log(error);
    })
  }

  get500Error(){
    this.http.get(`${this.baseUrl}Buggy/servererror`).subscribe((data) => {

    },(error) =>{
      console.log(error);
    })
  }

  get400ValidationError(){
    this.http.get(`${this.baseUrl}Buggy/validationerror/neu`).subscribe((data) => {

    },(error) =>{
      console.log(error);
      this.validationError = error.errors;
    })
  }
}
