import { SortDirectionEnum } from "./sort-direction-enum.interface";

export interface ProductQuery {
    name: string | null;
    isActive: boolean | null;
    pageIndex: number;
    pageSize: number;
    sortDircetion: SortDirectionEnum | null;
}