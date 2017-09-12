import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';

import {LoggerService} from "./services/logger.service";

import { AppComponent } from './app.component';
import { ButtonComponent } from './common/button/button.component';
import { ButtonGroupComponent } from './common/button-group/button-group.component';

@NgModule({
    providers: [
        LoggerService
    ],
    imports: [
        BrowserModule
    ],
    declarations: [
        AppComponent,
        ButtonComponent,
        ButtonGroupComponent
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {
}