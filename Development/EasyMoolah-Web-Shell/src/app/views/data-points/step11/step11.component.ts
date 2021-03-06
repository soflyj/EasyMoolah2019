import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HeaderService } from '../../../services/header.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataPointModel } from 'src/app/models/data-point.model';
import { DataPointService } from 'src/app/services/data-point.service';
import { CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-step11',
  templateUrl: './step11.component.html',
  styleUrls: ['../../../../assets/css/em_site_theme.css']
})
export class Step11Component implements OnInit {

  stepForm: FormGroup;
  dataPoint: DataPointModel = new DataPointModel();
  question: string;
  answer: string = null;
  guid: any;
  startTime

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private headerService: HeaderService,
    private dataPointService: DataPointService,
    private commonService: CommonService) {
    this.question = 'With which bank do you have an account?';
  }

  ngOnInit() {

    this.activatedRoute.params.subscribe((params: any) => {
      this.guid= params.guid;
    });
    this.startTime = new Date();
    this.headerService.mode.next('determinate');
    this.headerService.progress.next(60);

    if (this.dataPointService.getPreviousDataPointState(11) != null) {
      this.answer = this.dataPointService.getPreviousDataPointState(11)[0];
    }

    if (this.guid != this.commonService.GetGUID()) {
      this.router.navigate(['not-found'], { relativeTo: this.activatedRoute })
    }

    // Reactive validation
    this.stepForm = new FormGroup({
      'bank': new FormControl(
        this.answer,
        [Validators.required]
      ),
    });
  }

  Next() {
    this.dataPoint.Question = [];
    this.dataPoint.Answer = [];
    
    this.dataPoint.Id = 11;
    this.dataPoint.Question.push(this.question);
    this.dataPoint.Answer.push(this.stepForm.get('bank').value);
    this.dataPoint.StartTime = this.startTime;
    this.dataPoint.EndTime = new Date();
    this.dataPointService.addDataPoint(this.dataPoint);
  }

  Back() {

  }
}
