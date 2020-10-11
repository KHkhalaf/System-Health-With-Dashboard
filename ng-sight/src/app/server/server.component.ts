import { Component, OnInit ,Input } from '@angular/core';
import { Server } from '../shared/server';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})
export class ServerComponent implements OnInit {

  @Input() serverInput: Server;

  color:string;
  buttonText:string;
  constructor() { }

  ngOnInit(): void {
    this.setToggleStatus();
  }

  setToggleStatus(){
    if(this.serverInput.isOnline){
      this.color = '#66bb6a';
      this.buttonText = 'ShutDown';
    }
    else{
      this.color = '#ff6b6b';
      this.buttonText = 'Start';
    }
  }
  toggleStatus(){
    this.serverInput.isOnline = !this.serverInput.isOnline;
    this.setToggleStatus();
  }
}
