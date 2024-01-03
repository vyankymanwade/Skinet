import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ShopComponent } from "./shop.component";

const routes:Route[] = [
    {
        path:'',
        component:ShopComponent
    },
    {
        path:':id',
        component:ProductDetailsComponent,
        data:{breadcrumb:{alias:'productDetails'}}
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class ShopRoutingModule { }