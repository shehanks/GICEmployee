import React, { useState, useEffect } from "react";
import { TextField, Button, Box, Typography } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import config from "../config/config.js";

const AddEditCafePage = () => {
  const [cafe, setCafe] = useState({
    name: "",
    description: "",
    location: "",
    logo: null,
  });
  const [initialCafe, setInitialCafe] = useState(null); // To store the initial data
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    if (id && id !== "new") {
      const fetchCafe = async () => {
        const response = await fetch(`${config.apiBaseUrl}/cafe/${id}`);
        const data = await response.json();
        setCafe(data);
        setInitialCafe(data); // Set the initial data
      };

      fetchCafe();
    } else {
      setInitialCafe({
        name: "",
        description: "",
        location: "",
        logo: null,
      }); // For new cafe, set initial state
    }
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const method = id && id !== "new" ? "PUT" : "POST";
    const url =
      id && id !== "new"
        ? `${config.apiBaseUrl}/cafe/${id}`
        : `${config.apiBaseUrl}/cafe`;

    await fetch(url, {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(cafe),
    })
      .then((response) => {
        if (response.ok) {
          alert(
            `Cafe ${id && id !== "new" ? "updated" : "added"} successfully!`
          );
          navigate("/cafes"); // Redirect to cafes page
        } else {
          alert("Failed to save cafe.");
        }
      })
      .catch((err) => console.error("Error saving cafe:", err));
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCafe((prev) => ({ ...prev, [name]: value }));
  };

  const handleCancel = () => {
    if (isDirty() && !window.confirm("You have unsaved changes. Discard them?")) {
      return;
    }
    navigate("/cafes");
  };

  const isDirty = () => {
    // Check if current form state differs from initial state
    return JSON.stringify(cafe) !== JSON.stringify(initialCafe);
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
        {id && id !== "new" ? "Edit Cafe" : "Add New Cafe"}
      </Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Name"
          name="name"
          value={cafe.name}
          onChange={handleChange}
          fullWidth
          required
          inputProps={{ minLength: 6, maxLength: 10 }}
        />
        <TextField
          label="Description"
          name="description"
          value={cafe.description}
          onChange={handleChange}
          fullWidth
          required
          multiline
          rows={4}
          inputProps={{ maxLength: 256 }}
        />
        <TextField
          label="Location"
          name="location"
          value={cafe.location}
          onChange={handleChange}
          fullWidth
          required
        />
        <Box display="flex" justifyContent="space-between" mt={2}>
          <Button type="submit" variant="contained">
            Submit
          </Button>
          <Button onClick={handleCancel} variant="outlined" color="secondary">
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

export default AddEditCafePage;
