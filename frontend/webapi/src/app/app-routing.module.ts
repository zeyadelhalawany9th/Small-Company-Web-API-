import { DepartmentComponent } from './department/department.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmployeeComponent} from './employee/employee.component';


const routes: Routes = [

  {path:'employee', component:EmployeeComponent},
  {path:'Employee', component:EmployeeComponent},
  {path:'department', component:DepartmentComponent},
  {path:'Department', component:DepartmentComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
