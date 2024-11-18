import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  Box,
  Typography,
  Radio,
  RadioGroup,
  FormControlLabel,
  FormLabel,
  Select,
  MenuItem,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";

const AddEditEmployeePage = () => {
  const { id } = useParams(); // For editing an existing employee
  const navigate = useNavigate();

  const [employee, setEmployee] = useState({
    name: "",
    email: "",
    phone: "",
    gender: 1,
    cafeId: "",
  });
  const [initialEmployee, setInitialEmployee] = useState(null);
  const [cafes, setCafes] = useState([]); // Dropdown options

  useEffect(() => {
    // Fetch cafes for dropdown
    fetch("http://localhost:5109/api/cafe")
      .then((response) => response.json())
      .then((data) => setCafes(data))
      .catch((err) => console.error("Error fetching cafes:", err));

    if (id && id !== "new") {
      // Fetch employee details for editing
      fetch(`http://localhost:5109/api/employee/${id}`)
        .then((response) => response.json())
        .then((data) => {
          setEmployee(data);
          setInitialEmployee(data);
        })
        .catch((err) => console.error("Error fetching employee:", err));
    } else {
      const newEmployee = {
        name: "",
        email: "",
        phone: "",
        gender: 1,
        cafeId: "",
      };
      setInitialEmployee(newEmployee);
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEmployee({
      ...employee,
      [name]: name === "gender" ? parseInt(value, 10) : value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const method = id && id !== "new" ? "PUT" : "POST";
    const url =
      id && id !== "new"
        ? `http://localhost:5109/api/employee/${id}`
        : "http://localhost:5109/api/employee";

    const payload = {
      name: employee.name,
      emailAddress: employee.email,
      phoneNumber: employee.phone,
      gender: employee.gender,
      cafeId: employee.cafeId || null
    };

    fetch(url, {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    })
      .then((response) => {
        if (response.ok) {
          alert(
            `Employee ${id && id !== "new" ? "updated" : "added"} successfully!`
          );
          navigate("/employees");
        } else {
          alert("Failed to save employee.");
        }
      })
      .catch((err) => console.error("Error saving employee:", err));
  };

  const handleCancel = () => {
    if (
      isDirty() &&
      !window.confirm("You have unsaved changes. Discard them?")
    ) {
      return;
    }
    navigate("/employees");
  };

  const isDirty = () => {
    // Check if current form state differs from initial state
    return JSON.stringify(employee) !== JSON.stringify(initialEmployee);
  };

  return (
    <Box
      sx={{
        maxWidth: 600,
        margin: "20px auto",
        padding: 2,
        border: "1px solid #ccc",
        borderRadius: "8px",
        backgroundColor: "#f9f9f9",
      }}
    >
      <Typography variant="h4" sx={{ marginBottom: 3, textAlign: "center" }}>
        {id && id !== "new" ? "Edit Employee" : "Add Employee"}
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Name"
          name="name"
          value={employee.name}
          onChange={handleChange}
          fullWidth
          required
          inputProps={{ minLength: 6, maxLength: 10 }}
          margin="normal"
        />
        <TextField
          label="Email Address"
          name="email"
          type="email"
          value={employee.email}
          onChange={handleChange}
          fullWidth
          required
          margin="normal"
        />
        <TextField
          label="Phone Number"
          name="phone"
          value={employee.phone}
          onChange={handleChange}
          fullWidth
          required
          inputProps={{
            pattern: "^[89]\\d{7}$",
            title:
              "Phone number must start with 8 or 9 and have exactly 8 digits.",
          }}
          margin="normal"
        />
        <FormLabel sx={{ marginTop: 2 }}>Gender</FormLabel>
        <RadioGroup
          name="gender"
          value={employee.gender}
          onChange={handleChange}
          row
        >
          <FormControlLabel value={1} control={<Radio />} label="Male" />
          <FormControlLabel value={2} control={<Radio />} label="Female" />
        </RadioGroup>
        <FormLabel sx={{ marginTop: 2 }}>Assigned Cafe</FormLabel>
        <Select
          name="cafeId"
          value={employee.cafeId || ""}
          onChange={handleChange}
          fullWidth
          required={false} // No longer required
        >
          <MenuItem value="">None</MenuItem>{" "}
          {/* Option to not select any cafe */}
          {cafes.map((cafe) => (
            <MenuItem key={cafe.id} value={cafe.id}>
              {cafe.name}
            </MenuItem>
          ))}
        </Select>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            marginTop: 3,
          }}
        >
          <Button type="submit" variant="contained" color="primary">
            Save
          </Button>
          <Button variant="outlined" color="secondary" onClick={handleCancel}>
            Cancel
          </Button>
        </Box>
      </form>
      {isDirty() && (
        <Typography color="error" mt={2}>
          You have unsaved changes.
        </Typography>
      )}
    </Box>
  );
};

export default AddEditEmployeePage;
