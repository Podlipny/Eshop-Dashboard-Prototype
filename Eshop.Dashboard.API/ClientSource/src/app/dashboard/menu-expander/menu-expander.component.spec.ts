import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuExpanderComponent } from './menu-expander.component';

describe('MenuExpanderComponent', () => {
  let component: MenuExpanderComponent;
  let fixture: ComponentFixture<MenuExpanderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MenuExpanderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuExpanderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
