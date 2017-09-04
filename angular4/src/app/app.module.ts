import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';

import {LoggerService} from "./services/logger.service";

import { AppComponent } from './app.component';
import { ButtonComponent } from './common/button/button.component';

@NgModule({
    providers: [
        LoggerService
    ],
    imports: [
        BrowserModule
    ],
    declarations: [
        AppComponent,
        ButtonComponent
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {
}