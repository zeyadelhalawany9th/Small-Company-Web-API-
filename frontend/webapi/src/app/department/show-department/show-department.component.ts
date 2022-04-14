import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-department',
  templateUrl: './show-department.component.html',
  styleUrls: ['./show-department.component.css']
})
export class ShowDepartmentComponent implements OnInit {

  constructor(private service:SharedService) { }

  DepartmentList:any = [];

  ModalTitle:string = "";
  ActivateAddEditDepComp:boolean = false;
  department:any;



  ngOnInit(): void {
    this.refreshDepartmentList();
  }

  addClick()
  {
    this.department =
    {
      DepartmentID: 0,
      DepartmentName: ""
    }

    this.ModalTitle = "Add Department"
    this.ActivateAddEditDepComp = true;


  }

  editClick(item: any)
  {
    this.department = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;

  }

  deleteClick(item: any)
  {
    if(confirm('Are you sure you want to delete the '+item.DepartmentName+' department?'))
    {
      this.service.deleteDepartment(item.DepartmentID).subscribe
      (
        data =>
        {
          alert(data.toString());
          this.refreshDepartmentList();
        }
      )

    }

  }

  closeClick()
  {
    this.ActivateAddEditDepComp = false;
    this.refreshDepartmentList();

  }



  refreshDepartmentList()
  {
    this.service.getDepartmentList().subscribe
    (
      data =>
      {
        this.DepartmentList = data;
      }

    );

  }


}
