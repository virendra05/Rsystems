import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee, ApiResponse } from '../models/employee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  API_URL = 'https://localhost:7159/api/';

  constructor(private http: HttpClient) { }

  saveEmployeeData(employeeData: Employee): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.API_URL}Employee`, employeeData);
  }

  getEmployees(searchText: string, sortColumns: string[], sortOrder: number, pageIndex: number, pageSize: number) {
    let sortColumnParam = sortColumns.join(",");
    let url = this.API_URL + "Employee";
    let params = {
      searchText: searchText,
      sortColumn: sortColumnParam,
      sortOrder: sortOrder.toString(),
      pageIndex: pageIndex.toString(),
      pageSize: pageSize.toString()
    };
    return this.http.get<ApiResponse>(url, { params });
  }


  updateEmployeeData(employeeData: Employee): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(`${this.API_URL}Employee`, employeeData);
  }

  deleteEmployee(id: number) {
    let url = this.API_URL + "Employee?id=" + id;
    let params = { id };
    return this.http.delete<ApiResponse>(url, { params });
  }
}
