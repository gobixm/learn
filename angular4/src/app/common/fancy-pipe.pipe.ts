import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
    name: 'fancyPipe'
})
export class FancyPipePipe implements PipeTransform {

    transform(value: any, args?: any): any {
        return '🔥' + value.toString() + '🔥';
    }

}
