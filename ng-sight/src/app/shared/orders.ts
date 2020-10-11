import { Order } from './Order';

export const orders:Order[]=[
    { id:1, 
      customer: { id:1, 
                  name:'khalid', 
                  email:'kh@gmail.com', 
                  state:'bavaria' }, 
      total:185, 
      placed:new Date(2018,11,5), 
      fulfilled:new Date(2020,2,4),
      status:'DONE'
    },
    { id:2, 
        customer: { id:2, 
                    name:'doried', 
                    email:'dr@gmail.com', 
                    state:'homs' }, 
        total:76, 
        placed:new Date(2018,11,5), 
        fulfilled:new Date(2020,2,4),
        status:'PENDING'
      },
      { id:3, 
        customer: { id:3, 
                    name:'lasien', 
                    email:'la@gmail.com', 
                    state:'palastin' }, 
        total:170, 
        placed:new Date(2018,11,5), 
        fulfilled:new Date(2020,2,4),
        status:'DONE'
      },
      { id:4, 
        customer: { id:4, 
                    name:'Diaa', 
                    email:'Diaa@gmail.com', 
                    state:'daraa' }, 
        total:145, 
        placed:new Date(2018,11,5), 
        fulfilled:new Date(2020,2,4),
        status:'DONE'
      },
      { id:5, 
        customer: { id:5, 
                    name:'ahmad', 
                    email:'ah@gmail.com', 
                    state:'libia' }, 
        total:105, 
        placed:new Date(2018,11,5), 
        fulfilled:new Date(2020,2,4),
        status:'PENDING'
      }
  ];