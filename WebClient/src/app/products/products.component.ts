import { Component } from '@angular/core';
import { ProductResponseDto } from '../models/product-response.interface';
import { ProductService } from '../services/products.service';
import { SortDirectionEnum } from '../models/sort-direction-enum.interface';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
clickInactive() {
  this.isActive = false;
  this.selectedSortValue = "inactive"
  this.loadProducts();
}
clickActive() {
  this.isActive = true;
  this.selectedSortValue = "active"
  this.loadProducts();
}
clickBoth() {
  this.isActive = null;
  this.selectedSortValue = "both";
  this.loadProducts();
}
clickDescending() {
  this.sortDirection = SortDirectionEnum.Descending;
  this.loadProducts(); 
}
  selectedDirectionValue: string | null = null;
  selectedSortValue: string = "both";


searching() {
  this.selectedDirectionValue = null;
  this.sortDirection = null;
  this.isActive = null;
  this.loadProducts();
}
  

clickAscending() {
  this.sortDirection = SortDirectionEnum.Ascending;
  this.loadProducts(); 
}

redirectToProductDetails(productId: number): void {
  this.router.navigate(['/product', productId]);
}
  
  public name: string = '';
  public isActive: boolean | null = null;
  public currentPage: number = 1;
  public count: number = 10;
  public sortDirection: SortDirectionEnum | null = null;

  public data: ProductResponseDto[] = [];
  public dataSize: number = 0;
  public pageSize: number = 5;
  public pageOptions: number[] = [5, 10, 20];
  public totalItems: number = 0;
  public totalPages: number = 0;
  
  constructor(private productService: ProductService, private router: Router) {
    this.loadProducts();
  }

  handlePageEvent(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex + 1;
    this.loadProducts();
  }
  
  loadProducts() {
    this.productService.get({ name: this.name, isActive: this.isActive, pageIndex: this.currentPage, 
                              pageSize: this.pageSize, sortDirection: this.sortDirection })
    .subscribe({
      next: (res) => {
        if (res == null) {
          this.data = [];
          this.totalItems = 0;
          this.totalPages = 0;
          return;
        }
        console.log(res);
        this.data = res.items;
        this.totalItems = res.count;
        this.totalPages = res.totalPages;
      }
    })
  }
}
