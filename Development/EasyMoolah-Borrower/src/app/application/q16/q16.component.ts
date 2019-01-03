import { Component, OnInit, NgZone } from '@angular/core';
import { routerTransition } from '../../common/router.animations';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BorrowerService } from 'src/app/service/borrower.service';
import { BorrowerApplicationLog } from 'src/app/model/borrowerapplicationLog.model';

@Component({
    selector: 'app-q16',
    templateUrl: './q16.component.html',
    styleUrls: ['../../../assets/css/em_site_theme.css'],
    animations: [routerTransition]
})
export class Q16Component implements OnInit {

    Q16: FormGroup;
    URL = false;
    StartTime: Date;
    saIdParser: any;
    require: any;
    idnumber: string;
    isIdNumberValid: boolean;

    constructor(public zone: NgZone,
        private router: Router,
        private route: ActivatedRoute,
        private borrowerService: BorrowerService) { }

    ngOnInit() {
        this.StartTime = new Date();

        // Not allowed to navigate directly to component
        // this.URL = (window.location.href).includes('/application');
        // if (!this.URL) {
        //     this.router.navigate(['notfound'], { relativeTo: this.route });
        // }
        
        this.idnumber = '';
        this.Q16 = new FormGroup({
            //'idnumber': new FormControl('', [Validators.required, this.CheckSAIdNumber.bind(this)])
            'idnumber': new FormControl('', [Validators.required])
        });

    }

    CheckSAIdNumber(control: FormControl): { [s: string]: boolean } {
         this.saIdParser = this.require('south-african-id-parser');
         this.isIdNumberValid = this.saIdParser.validate(control.value);        
        // console.log(this.saIdParser(control.value));
        if (!this.isIdNumberValid) {
            return { 'IdNumberValid': true };
        } else {
        return null;
        }
    }

    Next() {
        // tslint:disable-next-line:max-line-length
        this.borrowerService.addBorrowerApplicationLog(new BorrowerApplicationLog('Question', 'ID Number', this.Q16.value, this.StartTime.toString(), (new Date).toString()));

        this.router.navigateByUrl('/processing', { skipLocationChange: true });
    }

    Back() {
        this.router.navigateByUrl('/bq15', { skipLocationChange: true });
    }
}
