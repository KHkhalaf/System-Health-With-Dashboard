import { Component, OnInit } from '@angular/core';
import { SAMPLE_LINECHART_COLORS } from '../../shared/chart.colors';

const SAMPLE_LINECHART_DATA: any[]=[
  { data:[65,15,34,73,92,42,30],label:'semintal Sales' },
  { data:[25,36,49,53,83,13,60],label:'Quantity Orders' },
  { data:[66,38,91,59,23,34,40],label:'solver Problems' }
];

const SAMPLE_LINECHART_LABELS:string[]=['jan','feb','mar','apr','jun','jul','aug'];



@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {

  constructor() { }
  public lineChartData: any[] = SAMPLE_LINECHART_DATA;
  public lineChartLabels: string[] = SAMPLE_LINECHART_LABELS;
  public lineChartColors = SAMPLE_LINECHART_COLORS;
  public lineChartType = 'line';
  public lineChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  ngOnInit(): void {
  }

}
