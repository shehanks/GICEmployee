import React, { useEffect, useState } from "react";
import { Button, TextField } from "@mui/material";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import { useNavigate } from "react-router-dom";
import config from "../config/config.js";

const CafePage = () => {
  const [cafes, setCafes] = useState([]);
  const [location, setLocation] = useState("");
  const navigate = useNavigate();

  const fetchCafes = async (filterLocation = "") => {
    const url = filterLocation
      ? `${config.apiBaseUrl}/cafe?location=${filterLocation}`
      : `${config.apiBaseUrl}/cafe`;
    const response = await fetch(url);
    const data = await response.json();
    setCafes(data);
  };

  useEffect(() => {
    fetchCafes();
  }, []);

  const handleAddCafe = () => {
    navigate("/cafes/edit/new");
  };

  const handleEditCafe = (id) => {
    navigate(`/cafes/edit/${id}`);
  };

  const handleDeleteCafe = async (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this cafe?"
    );
    if (confirmDelete) {
      await fetch(`${config.apiBaseUrl}/cafe/${id}`, {
        method: "DELETE",
      });
      setCafes(cafes.filter((cafe) => cafe.id !== id));
    }
  };

  const handleViewEmployees = (cafeId) => {
    navigate(`/employees?cafe=${cafeId}`); // Pass cafeId as a query parameter
  };

  const handleFilterByLocation = () => {
    fetchCafes(location); // Fetch cafes filtered by location
  };

  const columns = [
    {
      headerName: "Logo",
      field: "logo",
      cellRenderer: (params) => (
        <img src={params.value} alt="Logo" style={{ width: 50, height: 50 }} />
      ),
    },
    { headerName: "Name", field: "name" },
    { headerName: "Description", field: "description" },
    {
      headerName: "Employees",
      field: "employeeCount",
      cellRenderer: (params) => (
        <Button
          disabled={!params.value}
          variant="text"
          onClick={() => handleViewEmployees(params.data.id)}
        >
          {params.value || "0"}
        </Button>
      ),
    },
    { headerName: "Location", field: "location" },
    {
      headerName: "Actions",
      field: "actions",
      cellRenderer: (params) => (
        <>
          <Button onClick={() => handleEditCafe(params.data.id)} disabled>
            Edit
          </Button>
          <Button onClick={() => handleDeleteCafe(params.data.id)}>
            Delete
          </Button>
        </>
      ),
    },
  ];

  return (
    <div>
      <h1>Cafes</h1>

      {/* Location Filter */}
      <div style={{ marginBottom: "20px" }}>
        <TextField
          label="Filter by Location"
          variant="outlined"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
          style={{ marginRight: "10px", width: "300px" }}
        />
        <Button variant="contained" onClick={handleFilterByLocation}>
          Filter
        </Button>
      </div>

      <Button variant="contained" onClick={handleAddCafe}>
        Add New Cafe
      </Button>
      <div className="ag-theme-alpine" style={{ height: 400, width: "100%" }}>
        <AgGridReact
          rowData={cafes}
          columnDefs={columns}
          pagination={false}
          filter={false}
        />
      </div>
    </div>
  );
};

export default CafePage;
