import {Component, Input, OnInit} from '@angular/core';
import {LoggerService} from "../../services/logger.service";

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.less']
})
export class ButtonComponent implements OnInit {
    private _logger: LoggerService;

    constructor(logger: LoggerService) {
        this._logger = logger;
    }

    @Input()
    caption: string;

    @Input()
    hint: string;

    click() {
        this._logger.log('button clicked');
    }

    ngOnInit() {
    }

}
