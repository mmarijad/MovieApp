import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-form-select',
  templateUrl: './form-select.component.html',
  styleUrls: ['./form-select.component.css'],
  styles:[':host ::ng-deep select:first-child{display:block;}']
})
export class FormSelectComponent implements ControlValueAccessor {
  @Input() label: string;
  @Input() comparationId: number;
  @Input() options=[];

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

  compareFn( optionOne, optionTwo ) : boolean {
    return optionOne.id === optionTwo.id;
  }
}
