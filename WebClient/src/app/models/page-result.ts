export interface PageResult<T> {
    count: number;
    pageIndex: number;
    pageSize: number;
    totalPages: number;
    items: T[];
}