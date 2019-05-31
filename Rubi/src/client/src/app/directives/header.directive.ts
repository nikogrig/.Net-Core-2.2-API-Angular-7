import { Directive, ElementRef, HostListener, Renderer } from '@angular/core';
import { RouterEvent, RouterLinkActive } from '@angular/router';

@Directive({
    selector: '[headerHightLight]'
})
export class HeaderHighLightDirective {
    @HostListener('mouseleave') onMouseLeave() {
        this.highlight('nav-link');
    };

    @HostListener('mouseenter') onMouseEnter() {
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