import { Component, OnInit } from '@angular/core';

const SAMPLE_BARCHART_DATA: any[] = [ 65,15,34,73 ];

const SAMPLE_BARCHART_LABELS:string[]=['XYZ orders', 'abc clients', 'another tips', ' our blocks'];

const SAMPLE_BARCHART_COLORS:any[]=[{
  backgroundColor:['#123cad', '#333caa', '#112cbf', '#7561bf'],
  borderColor:'#11a'
}];

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  constructor() { }
  public barChartData: any[] = SAMPLE_BARCHART_DATA;
  public barChartLabels: string[] = SAMPLE_BARCHART_LABELS;
  public barChartColors:any[] = SAMPLE_BARCHART_COLORS;
  public barChartType = 'doughnut';
  ngOnInit(): void {
  }

}
