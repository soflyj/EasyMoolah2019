import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HeaderService } from '../../../services/header.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataPointModel } from 'src/app/models/data-point.model';
import { DataPointService } from 'src/app/services/data-point.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-step10',
  templateUrl: './step10.component.html',
  styleUrls: ['../../../../assets/css/em_site_theme.css']
})
export class Step10Component implements OnInit {

  stepForm: FormGroup;
  dataPoint: DataPointModel = new DataPointModel();
  question: string;
  answer: string = null;
  guid: any;
  startTime;
  monthlyexpense_slider: string;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private headerService: HeaderService,
    private dataPointService: DataPointService,
    private commonService: CommonService) {
    this.question = 'What is your total monthly expense?';
  }


  ngOnInit() {

    this.activatedRoute.params.subscribe((params: any) => {
      this.guid= params.guid;
    });
    this.startTime = new Date();
    this.headerService.mode.next('determinate');
    this.headerService.progress.next(54);

    this.monthlyexpense_slider = '50000';

    if (this.dataPointService.getPreviousDataPointState(10) != null) {
      this.answer = this.dataPointService.getPreviousDataPointState(10)[0];
    }

    if (this.guid != this.commonService.GetGUID()) {
      this.router.navigate(['not-found'], { relativeTo: this.activatedRoute })
    }

    // Reactive validation
    this.stepForm = new FormGroup({
      'monthlyexpense_slider': new FormControl(
        this.monthlyexpense_slider,
        [Validators.required])
    });
  }

  Next() {
    this.dataPoint.Question = [];
    this.dataPoint.Answer = [];
    
    this.dataPoint.Id = 10;
    this.dataPoint.Question.push(this.question);
    this.dataPoint.Answer.push(this.stepForm.get('monthlyexpense_slider').value);
    this.dataPoint.StartTime = this.startTime;
    this.dataPoint.EndTime = new Date();
    this.dataPointService.addDataPoint(this.dataPoint);
  }

  Back() {
  }
}
