import { Component } from '@angular/core';
import { ProductResponseDto } from '../models/product-response.interface';
import { ProductService } from '../services/products.service';
import { SortDirectionEnum } from '../models/sort-direction-enum.interface';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  public name: string | null = null;
  public isActive: boolean | null = null;
  public page: number = 0;
  public count: number = 10;
  public sortDirection: SortDirectionEnum | null = null;


  public data: ProductResponseDto[] = [];
  public choosenProduct: ProductResponseDto | null = null;


  constructor(private productService: ProductService) {
    productService.get({ name: this.name, isActive: this.isActive, page: this.page, count: this.count, sortDirection: this.sortDirection })
    .subscribe({
      next: (res) => {
        console.log(res);
        this.data = res;
      }
    })
  }
}
