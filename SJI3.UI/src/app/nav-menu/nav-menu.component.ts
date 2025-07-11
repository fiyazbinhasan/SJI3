import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
  @Input() userData: any;
  @Input() isAuthenticated = false;
  @Output() logoffEvent = new EventEmitter();
  @Output() loginEvent = new EventEmitter();
  @Output() logTokenEvent = new EventEmitter();
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logoff() {
    this.logoffEvent.emit();
  }

  login() {
    this.loginEvent.emit();
  }

  logToken() {
    this.logTokenEvent.emit();
  }
}
