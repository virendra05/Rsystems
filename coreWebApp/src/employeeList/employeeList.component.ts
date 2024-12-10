import { Component, OnInit, Input, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { ApiResponse, GetEmployees } from '../models/employee.model';
import { EmployeeService } from '../services/employee.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {  PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-employeeList',
  templateUrl: './employeeList.component.html',
  styleUrls: ['./employeeList.component.scss']
})
export class EmployeeListComponent implements OnInit {
  @Input() refreshDataEvent: number = 0;
  employees: GetEmployees[] = [];
  totalPages:number=1;
  totalRecords:number=0;
  searchText: string = "";
  sortColumns: string[] = ['Email'];
  sortOrder: number = 1; 
  pageIndex: number = 1;
  pageSize: number = 10;
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'email', 'phoneNumber','edit', 'delete'];
  @Output() editEvent = new EventEmitter<GetEmployees>();

  constructor(private employeeService: EmployeeService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['refreshDataEvent']) {
      this.totalPages=1;
      this.totalRecords=0;
      this.pageIndex=1;
      this.pageSize=10;
      this.loadEmployees();
    }
  }

  loadEmployees() {
    this.employeeService.getEmployees(this.searchText, this.sortColumns, this.sortOrder, this.pageIndex, this.pageSize)
      .subscribe({
        next: (data: ApiResponse) => {
          if (data && data.employeeList.length > 0) {
            this.employees = data.employeeList;
            this.totalPages=data.totalPages;
            this.totalRecords=data.totalRecords;
          } else {
            this.snackBar.open('No data available.', 'Close', { duration: 5000 });
          }
        },
        error: () => {
          this.snackBar.open('Error occurred while loading data.', 'Close', { duration: 5000 });
        }
      });
  }
  

  editEmployee(id: number) {
    const employee = this.employees.find((employee: GetEmployees) => employee.id === id);
    if (employee) {
      this.editEvent.emit(employee);
    }
  }


  deleteEmployee(id: number) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        this.loadEmployees();
        this.snackBar.open("Deleted Successfully", 'Close', { duration: 5000 });
      },
      error: () => {
        this.snackBar.open('Error occurred while deleting the employee', 'Close', { duration: 5000 });
      }
    });
  }

  pageEvent(event: PageEvent): void {
    this.pageIndex = event.pageIndex + 1; 
    this.pageSize = event.pageSize;
    this.loadEmployees(); 
  } 
  
}
