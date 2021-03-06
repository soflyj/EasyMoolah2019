import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../common/router.animations';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BorrowerService } from 'src/app/service/borrower.service';
import { Question } from 'src/app/model/question.model';
import { HeaderService } from 'src/app/service/header.service';
import { Fincheck } from "src/app/model/fincheck.model";
import { Borrower } from "src/app/model/borrower.model";

@Component({
  selector: 'app-q4',
  templateUrl: './q4.component.html',
  styleUrls: ['../../../assets/css/em_site_theme.css'],
  animations: [routerTransition]
})
export class Q4Component implements OnInit {

  Q4: FormGroup;
  URL = false;
  Debug = false;
  StartTime: Date;
  Answer;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private borrowerService: BorrowerService,
    private fincheck: Fincheck,
    private headerService: HeaderService,
    private borrower: Borrower) { }

  ngOnInit() {
    this.StartTime = new Date();
    this.headerService.mode.next('determinate');
    this.headerService.progress.next(18);

    this.Answer = this.borrowerService.getPreviousAnswer('q4');

    // Not allowed to navigate directly to component
    this.Debug = this.borrowerService.debugMode();
    this.URL = (window.location.href).includes('/application');
    if (!this.URL && !this.Debug) {
      this.router.navigate(['notfound'], { relativeTo: this.route });
    }

    // Reactive validation
    this.Q4 = new FormGroup({
      'formal-debt-review': new FormControl(
        this.Answer,
        [Validators.required]),
    });
  }

  Next() {
    // tslint:disable-next-line:max-line-length
    this.borrowerService.addToQuestionLog(new Question('q4', 'Question', 'Have you applied for or are you under formal debt review?', this.Q4.get('formal-debt-review').value, this.StartTime.toString(), (new Date).toString()));
    this.borrower.IsUnderDebtReview = this.Q4.get('formal-debt-review').value;

    this.router.navigateByUrl('/q5', { skipLocationChange: true });
  }

  Back() {
    this.router.navigateByUrl('/bq3', { skipLocationChange: true });
  }
}
