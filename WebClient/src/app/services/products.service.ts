import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ProductQuery } from "../models/product-query.interface";
import { Observable } from "rxjs";
import { ProductResponseDto } from "../models/product-response.interface";
import { PageResult } from "../models/page-result";

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(private httpClient: HttpClient) {}

    public get(pagination: ProductQuery): Observable<PageResult<ProductResponseDto>> {
        const params: any = { PageIndex: pagination.pageIndex ?? 1, PageSize: pagination.pageSize ?? 5 };
        
        if (pagination.name) {
            params.name = pagination.name;
        }

        if (pagination.sortDirection != null) {
            params.sortDirection = pagination.sortDirection;
        }

        return this.httpClient.get<PageResult<ProductResponseDto>>('http://localhost:5129/api/Products', { params: params });
    }
}