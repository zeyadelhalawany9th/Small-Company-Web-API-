import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';


@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.css']
})
export class AddEditEmployeeComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() employee:any;

  EmployeeID:string = "";
  EmployeeName:string = "";
  EmployeeDepartment:string = "";
  EmployeeDateOfJoining:string = "";
  EmployeePhotoFileName:string = "";
  EmployeePhotoFinalFilePath:string = "";

  DepartmentsList:any = [];



  ngOnInit(): void {

    this.loadDepartmentsList();

  }


  loadDepartmentsList()
  {
    this.service.getAllDepartmentNames().subscribe
    (
      (data:any) =>
      {
        this.DepartmentsList = data;

        this.EmployeeID = this.employee.EmployeeID;
        this.EmployeeName = this.employee.EmployeeName;
        this.EmployeeDepartment = this.employee.EmployeeDepartment;
        this.EmployeeDateOfJoining = this.employee.EmployeeDateOfJoining;
        this.EmployeePhotoFileName = this.employee.EmployeePhotoFileName;
        this.EmployeePhotoFinalFilePath = this.service.PhotoUrl + this.EmployeePhotoFileName;

      }
    );

  }

  addEmployee()
  {

    var val = {EmployeeID:this.EmployeeID,
               EmployeeName:this.EmployeeName,
               EmployeeDepartment:this.EmployeeDepartment,
               EmployeeDateOfJoining:this.EmployeeDateOfJoining,
               EmployeePhotoFileName:this.EmployeePhotoFileName
              };

    this.service.addEmployee(val).subscribe
    (
      res =>
      {
        alert(res.toString());
      }
    );


  }

  updateEmployee()
  {
    var val = {EmployeeID:this.EmployeeID,
               EmployeeName:this.EmployeeName,
               EmployeeDepartment:this.EmployeeDepartment,
               EmployeeDateOfJoining:this.EmployeeDateOfJoining,
               EmployeePhotoFileName:this.EmployeePhotoFileName
              };

    this.service.updateEmployee(val).subscribe
    (
      res =>
      {
        alert(res.toString());
      }
    );

  }


  uploadPhoto(event: any)
  {
    var file = event.target.files[0];

    const formData:FormData = new FormData();
    formData.append('uploadedFile', file, file.name);

    this.service.uploadPhoto(formData).subscribe
    (
      (data:any) =>
      {
        this.EmployeePhotoFileName = data.toString();
        this.EmployeePhotoFinalFilePath = this.service.PhotoUrl + this.EmployeePhotoFileName;

      }
    );

  }

}
