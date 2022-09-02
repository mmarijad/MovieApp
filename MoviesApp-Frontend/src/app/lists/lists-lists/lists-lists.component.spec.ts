import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListsListsComponent } from './lists-lists.component';

describe('ListsListsComponent', () => {
  let component: ListsListsComponent;
  let fixture: ComponentFixture<ListsListsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListsListsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListsListsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
