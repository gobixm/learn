import {Component, Input, OnInit} from "@angular/core";

class MenuItem{
    title: string;
    clicks: number = 1;
    constructor(title:string){
        this.title = title;
    }

    click(){
        this.clicks++;
    }
}

@Component({
    selector: 'menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.less']
})
export class MenuComponent {
    @Input()
    title: string = '';
    clickCount: number = 1;
    items: MenuItem[] = [];

    constructor() {
        for(let i=0; i<10000; i++){
            this.items.push(new MenuItem(`item ${i}`))
        }
    }

    titleClick(){
        this.clickCount++;
    }
}