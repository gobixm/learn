import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule} from "@angular/forms";

import {LoggerService} from "./services/logger.service";

import {AppComponent} from './app.component';
import {ButtonComponent} from './common/button/button.component';
import {ButtonGroupComponent} from './common/button-group/button-group.component';
import {InfiniteScrollDirective} from './common/infinite-scroll.directive';
import {FancyPipePipe} from './common/fancy-pipe.pipe';
import {KeyValueFormComponent} from './key-value-form/key-value-form.component';

@NgModule({
    providers: [
        LoggerService
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule
    ],
    declarations: [
        AppComponent,
        ButtonComponent,
        ButtonGroupComponent,
        InfiniteScrollDirective,
        FancyPipePipe,
        KeyValueFormComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}