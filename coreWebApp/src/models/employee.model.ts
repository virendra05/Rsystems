export interface Employee {
  id: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
}

export interface GetEmployees {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
}

export interface ApiResponse {
  employeeList: GetEmployees[];
  totalPages:number;
  totalRecords:number;
  isSuccess: boolean;
  errors: [];
}
