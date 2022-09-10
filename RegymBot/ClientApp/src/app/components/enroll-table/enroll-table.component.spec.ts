import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollTableComponent } from './enroll-table.component';

describe('FeedbackTableComponent', () => {
  let component: EnrollTableComponent;
  let fixture: ComponentFixture<EnrollTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrollTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
