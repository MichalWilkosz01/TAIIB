import { Component } from '@angular/core';
import { ProductResponseDto } from '../models/product-response.interface';
import { ProductService } from '../services/products.service';
import { SortDirectionEnum } from '../models/sort-direction-enum.interface';
import {   Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';




@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {

redirectToProductDetails(productId: number): void {
  //this.router.navigate(['/details', productId]);
  this.router.navigate(['/product', productId]);
}
  
  public name: string | null = null;
  public isActive: boolean | null = null;
  public page: number = 0;
  public count: number = 10;
  public sortDirection: SortDirectionEnum | null = null;
  disabled = true;
  showFirstLastButtons = false;
  pageSizeOptions = [5, 10, 25];
  showPageSizeOptions = false;
  hidePageSize = true;

  public data: ProductResponseDto[] = [];
  public choosenProduct: ProductResponseDto | null = null;

  pageEvent: PageEvent | null = null;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.count = e.pageSize;
    this.page = e.pageIndex;
  }


  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  constructor(private productService: ProductService, private router: Router) {
    productService.get({ name: this.name, isActive: this.isActive, page: this.page, count: this.count, sortDirection: this.sortDirection })
    .subscribe({
      next: (res) => {
        console.log(res);
        this.data = res;
      }
    })
  }
}
