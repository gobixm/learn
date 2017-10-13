import {Component, Input, OnInit} from '@angular/core';

class Property {

    constructor(name: string, value: any) {
        this.name = name;
        this.value = value;
    }

    name: string;
    value: any;
}

@Component({
    selector: 'key-value-form',
    templateUrl: './key-value-form.component.html',
    styleUrls: ['./key-value-form.component.less']
})
export class KeyValueFormComponent implements OnInit {

    properties: Property[] = [];

    constructor() {
        this.submit = this.submit.bind(this);
    }

    ngOnInit() {
        Object.keys(this.json).forEach((key: any) => {
            this.properties.push(new Property(key, this.json[key]));
        });
    }

    @Input()
    json: any;

    submit() {
        this.properties.forEach(prop => {
            this.json[prop.name] = prop.value;
        });
    }
}
