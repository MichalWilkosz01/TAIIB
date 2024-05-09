import { SortDirectionEnum } from "./sort-direction-enum.interface";

export interface ProductQuery {
    name: string | null;
    isActive: boolean | null;
    page: number;
    count: number;
    sortDirection: SortDirectionEnum | null;
}