import { Order } from  './Order'; 

export interface Page{
    data:Order[];
    total:number;
}