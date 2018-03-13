import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomentsComponent } from './customents.component';

describe('CustomentsComponent', () => {
  let component: CustomentsComponent;
  let fixture: ComponentFixture<CustomentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
