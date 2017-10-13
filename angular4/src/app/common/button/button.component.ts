import {Component, Input, OnInit} from '@angular/core';
import {LoggerService} from "../../services/logger.service";
import {trigger, state, style, animate, transition} from '@angular/animations';

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.less'],
    animations: [
        trigger('buttonState', [
            state('inactive', style({
                transform: 'scale(1)'
            })),
            state('active', style({
                transform: 'scale(1.1)'
            })),
            transition('inactive => active', animate('100ms ease-in')),
            transition('active => inactive', animate('100ms ease-out'))
        ])
    ]
})
export class ButtonComponent implements OnInit {
    private _logger: LoggerService;
    public state: string = 'inactive';

    constructor(logger: LoggerService) {
        this._logger = logger;
    }

    @Input()
    caption: string;

    @Input()
    hint: string;

    @Input()
    clicked: Function;

    click() {
        this._logger.log('button clicked');
        this.state = this.state === 'active' ? 'inactive' : 'active';
        this.clicked && this.clicked();
    }

    ngOnInit() {
    }

}
