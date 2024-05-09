import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ProductQuery } from "../models/product-query.interface";
import { Observable } from "rxjs";
import { ProductResponseDto } from "../models/product-response.interface";

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(private httpClient: HttpClient) {}

    public get(pagination: ProductQuery): Observable<ProductResponseDto[]> {
        const params = { page: pagination.page ?? 0, count: pagination.count ?? 10 };
        return this.httpClient.get<ProductResponseDto[]>('http://localhost:5129/api/Products', { params: params });
    }
}