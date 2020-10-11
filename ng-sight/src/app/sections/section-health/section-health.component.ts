import { Component, OnInit } from '@angular/core';
import { Server } from '../../shared/Server';
import { servers } from '../../shared/servers';

@Component({
  selector: 'app-section-health',
  templateUrl: './section-health.component.html',
  styleUrls: ['./section-health.component.css']
})
export class SectionHealthComponent implements OnInit {

  constructor() { }
  servers:Server[]=servers;
  ngOnInit(): void {
  }

}
