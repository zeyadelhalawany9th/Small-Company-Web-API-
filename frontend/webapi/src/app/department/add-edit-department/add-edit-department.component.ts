import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-department',
  templateUrl: './add-edit-department.component.html',
  styleUrls: ['./add-edit-department.component.css']
})
export class AddEditDepartmentComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() department:any;

  DepartmentID:string = "";
  DepartmentName:string = "";


  ngOnInit(): void {

    this.DepartmentID = this.department.DepartmentID;
    this.DepartmentName = this.department.DepartmentName;
  }

  addDepartment()
  {

    var val = {DepartmentID:this.DepartmentID,
               DepartmentName:this.DepartmentName};

    this.service.addDepartment(val).subscribe
    (
      res =>
      {
        alert(res.toString());
      }
    );


  }

  updateDepartment()
  {
    var val = {DepartmentID:this.DepartmentID,
               DepartmentName:this.DepartmentName};

    this.service.updateDepartment(val).subscribe
    (
      res =>
      {
        alert(res.toString());
      }
    );

  }

}
