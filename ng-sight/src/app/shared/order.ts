import { Customer } from  './customer'; 

export interface Order{
    id:number;
    customer:Customer;
    orderTotal:number;
    placed:Date;
    Completed:Date;
    status:string;
}