import { Component, Input } from '@angular/core';  
import { ProductResponseDto } from '../models/product-response.interface';

@Component({
  selector: '[app-product-row]',
  templateUrl: './product-row.component.html',
  styleUrl: './product-row.component.css'
})
export class ProductRowComponent {
  @Input('app-product-row') product!: ProductResponseDto
}
