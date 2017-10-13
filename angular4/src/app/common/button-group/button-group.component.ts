import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'app-button-group',
    templateUrl: './button-group.component.html',
    styleUrls: ['./button-group.component.less'],
    host: {'class': 'button-group'}
})
export class ButtonGroupComponent implements OnInit {

    constructor() {
    }

    ngOnInit() {
    }

}
