import { Directive, ElementRef, HostListener, Renderer } from '@angular/core';
import { NgClass } from '@angular/common';

@Directive({
    selector: '[sidenavHightLight]'
})
export class SidenavHighLightDirective {

    ev: any;

    @HostListener('click', ['$event']) onclick($event) {
        this.ev = $event;
        this.highlight('active nav-link');
    }

    constructor(private element: ElementRef, private renderer: Renderer) { //current elemet in dom
        this.element.nativeElement.style.class = null;
    }

    private highlight(htmlClass: string) {
        if (this.element.nativeElement.contains(event.target)) {
            this.renderer.setElementAttribute(this.element.nativeElement, 'class', htmlClass); 
          }
    }
}