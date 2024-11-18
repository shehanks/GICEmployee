import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import CafePage from "./pages/CafePage.js";
import EmployeePage from "./pages/EmployeePage.js";
import AddEditCafePage from "./pages/AddEditCafePage.js";
import AddEditEmployeePage from "./pages/AddEditEmployeePage.js";
import HomePage from "./pages/HomePage.js";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/cafes" element={<CafePage />} />
        <Route path="/employees" element={<EmployeePage />} />
        <Route path="/cafes/edit/:id" element={<AddEditCafePage />} />
        <Route path="/employees/edit/:id" element={<AddEditEmployeePage />} />
      </Routes>
    </Router>
  );
}
export default App;
