import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
    name: 'capitalize'
})
export class CapitalizePipe implements PipeTransform {
   
    constructor() {
    }
   
    public transform(value: string) : string {
        return value[0].toUpperCase() + value.substring(1).toLowerCase();
    }
}