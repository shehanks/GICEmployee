import React from "react";
import { Link } from "react-router-dom";
import { Button, Box, Typography } from "@mui/material";

const HomePage = () => {
  return (
    <Box sx={{ textAlign: "center", padding: 4 }}>
      <Typography variant="h4" gutterBottom>
        Welcome to the Employee and Cafe Management
      </Typography>
      <Box>
        <Link to="/cafes">
          <Button variant="contained" sx={{ marginRight: 2 }}>
            View Cafes
          </Button>
        </Link>
        <Link to="/employees">
          <Button variant="contained">View Employees</Button>
        </Link>
      </Box>
    </Box>
  );
};

export default HomePage;
