import React, { useEffect, useState } from "react";
import { Button } from "@mui/material";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import { useNavigate, useSearchParams } from "react-router-dom";
import config from "../config/config.js";

const EmployeePage = () => {
  const [employees, setEmployees] = useState([]);
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const cafeId = searchParams.get("cafe"); // Extract the cafe query parameter

  useEffect(() => {
    const fetchEmployees = async () => {
      const url = cafeId
        ? `${config.apiBaseUrl}/employee/getbycafe?cafe=${cafeId}` // Fetch employees by cafe
        : `${config.apiBaseUrl}/employee/getbycafe`; // Fetch all employees if no cafe is provided
      const response = await fetch(url);
      const data = await response.json();
      setEmployees(data);
    };

    fetchEmployees();
  }, [cafeId]);

  const handleAddEmployee = () => {
    navigate("/employees/edit/new");
  };

  const handleEditEmployee = (id) => {
    navigate(`/employees/edit/${id}`);
  };

  const handleDeleteEmployee = async (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this employee?"
    );
    if (confirmDelete) {
      await fetch(`${config.apiBaseUrl}/employee/${id}`, {
        method: "DELETE",
      });
      setEmployees(employees.filter((employee) => employee.id !== id));
    }
  };

  const columns = [
    { headerName: "Employee ID", field: "id" },
    { headerName: "Name", field: "name" },
    { headerName: "Email", field: "emailAddress" },
    { headerName: "Phone", field: "phoneNumber" },
    { headerName: "Days Worked", field: "daysWorked" },
    { headerName: "Cafe", field: "cafeName" },
    {
      headerName: "Actions",
      cellRenderer: (params) => (
        <>
          <Button onClick={() => handleEditEmployee(params.data.id)} disabled>
            Edit
          </Button>
          <Button onClick={() => handleDeleteEmployee(params.data.id)}>
            Delete
          </Button>
        </>
      ),
    },
  ];

  return (
    <div>
      <h1>Employees</h1>
      <Button variant="contained" onClick={handleAddEmployee}>
        Add New Employee
      </Button>
      <div className="ag-theme-alpine" style={{ height: 400, width: "100%" }}>
        <AgGridReact
          rowData={employees}
          columnDefs={columns}
          pagination={false}
          filter={false}
        />
      </div>
    </div>
  );
};

export default EmployeePage;
