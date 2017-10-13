import {Component} from '@angular/core';

const _ = require('lodash');

import '../assets/css/styles.less';

@Component({
    selector: 'my-app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.less']
})
export class AppComponent {
    scrollCallback: any;
    items: string[] = [];
    menuTitle: string = 'Menu title';
    settings: {} = {
        key: "value",
        number: 42
    };

    constructor() {
        this.scrollCallback = this.appendItems.bind(this);
        this.appendItems();
        this.checkSettings = this.checkSettings.bind(this);
    }

    checkSettings() {
        console.log(this.settings);
    }

    private appendItems() {
        this.items = this.items.concat(
            _.map(_.range(500), (i: number) => (this.items.length + i).toString())
        );
    }
}