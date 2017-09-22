import { Component } from '@angular/core';
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

    constructor() {
        this.scrollCallback = this.appendItems.bind(this);
        this.appendItems();
    }

    private appendItems(){
        this.items = this.items.concat(
            _.map(_.range(500), (i:number) => (this.items.length + i).toString())
        );
    }
}