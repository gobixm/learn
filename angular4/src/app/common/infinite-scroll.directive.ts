///<reference path="../../../node_modules/@angular/core/src/metadata/lifecycle_hooks.d.ts"/>
import {
    Directive, Input, OnDestroy, OnInit, TemplateRef, ViewContainerRef, ElementRef,
    AfterViewInit
} from '@angular/core';

import {Observable, Subscription} from 'rxjs';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/pairwise';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/exhaustMap';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/startWith';

interface ScrollPosition {
    height: number;
    top: number;
    clientHeight: number;
}

const DEFAULT_SCROLL_POSITION: ScrollPosition = {
    height: 0,
    top: 0,
    clientHeight: 0
};

@Directive({
    selector: '[infiniteScroll]'
})

export class InfiniteScrollDirective implements AfterViewInit {
    private scrollEvent$: Observable<any>;
    private userScrolledDown$: Observable<any>;
    private requestOnScroll$: any;

    @Input()
    scrollCallback: any;

    @Input()
    immediateCallback = true;

    @Input()
    scrollPercent = 70;

    constructor(private element: ElementRef) {

    }

    ngAfterViewInit(): void {
        this.registerScrollEvent();
        this.streamScrollEvents();
        this.requestCallbackOnScroll();
    }

    private registerScrollEvent() {
        this.scrollEvent$ = Observable.fromEvent(this.element.nativeElement, 'scroll');
    }

    private streamScrollEvents() {
        this.userScrolledDown$ = this.scrollEvent$
            .map((e: any): ScrollPosition => ({
                height: e.target.scrollHeight,
                top: e.target.scrollTop,
                clientHeight: e.target.clientHeight
            }))
            .pairwise()
            .filter(positions => this.isUserScrollingDown(positions) && this.isScrollExpectedPercent(positions[1]));
    }

    private requestCallbackOnScroll() {
        this.requestOnScroll$ = this.userScrolledDown$;

        if (this.immediateCallback) {
            this.requestOnScroll$ = this.requestOnScroll$
                .startWith([DEFAULT_SCROLL_POSITION, DEFAULT_SCROLL_POSITION]);
        }

        this.requestOnScroll$
            .subscribe(() => {
                this.scrollCallback();
            });
    }

    private isUserScrollingDown = (positions: ScrollPosition[]) => {
        return positions[0].top < positions[1].top;
    };

    private isScrollExpectedPercent = (position: ScrollPosition) => {
        return ((position.top + position.clientHeight) / position.height) > (this.scrollPercent / 100);
    };
}
