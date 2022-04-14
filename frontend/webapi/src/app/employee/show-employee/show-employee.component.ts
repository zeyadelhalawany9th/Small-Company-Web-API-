import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-employee',
  templateUrl: './show-employee.component.html',
  styleUrls: ['./show-employee.component.css']
})
export class ShowEmployeeComponent implements OnInit {

  constructor(private service:SharedService) { }

  EmployeeList:any = [];

  ModalTitle:string = "";
  ActivateAddEditEmpComp:boolean = false;
  employee:any;



  ngOnInit(): void {
    this.refreshEmployeeList();
  }

  addClick()
  {
    this.employee =
    {
      EmployeeID: 0,
      EmployeeName: "",
      EmployeeDepartment: "",
      EmployeeDateOfJoining: "",
      EmployeePhotoFileName: "anonymous.png"
    }

    this.ModalTitle = "Add Employee"
    this.ActivateAddEditEmpComp = true;


  }

  editClick(item: any)
  {
    this.employee = item;
    this.ModalTitle = "Edit Employee";
    this.ActivateAddEditEmpComp = true;

  }

  deleteClick(item: any)
  {
    if(confirm('Are you sure you want to delete '+item.EmployeeName+' from the list of employees?'))
    {
      this.service.deleteEmployee(item.EmployeeID).subscribe
      (
        data =>
        {
          alert(data.toString());
          this.refreshEmployeeList();
        }
      )

    }

  }

  closeClick()
  {
    this.ActivateAddEditEmpComp = false;
    this.refreshEmployeeList();

  }



  refreshEmployeeList()
  {
    this.service.getEmployeeList().subscribe
    (
      data =>
      {
        this.EmployeeList = data;
      }

    );

  }


}
