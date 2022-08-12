import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PriceServiceModuleComponent } from './price-service-module.component';

describe('PriceServiceModuleComponent', () => {
  let component: PriceServiceModuleComponent;
  let fixture: ComponentFixture<PriceServiceModuleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PriceServiceModuleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PriceServiceModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
