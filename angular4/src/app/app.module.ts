import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {LoggerService} from "./services/logger.service";

import { AppComponent } from './app.component';
import { ButtonComponent } from './common/button/button.component';
import { ButtonGroupComponent } from './common/button-group/button-group.component';
import { InfiniteScrollDirective } from './common/infinite-scroll.directive';
import { FancyPipePipe } from './common/fancy-pipe.pipe';

@NgModule({
    providers: [
        LoggerService
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule
    ],
    declarations: [
        AppComponent,
        ButtonComponent,
        ButtonGroupComponent,
        InfiniteScrollDirective,
        FancyPipePipe
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {
}