// src/pages/Login.tsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../api/api"; // קריאה לפונקציית login שכתבת
import type { Employee } from "../models/models";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const employee: Employee | null = await login(username, password);
      if (employee) {
        localStorage.setItem("username", JSON.stringify(employee.username));
        localStorage.setItem("userId", JSON.stringify(employee.id));

        navigate(`/user/${employee.username}/shifts`);
      } else {
        alert("שם משתמש או סיסמה שגויים");
      }
    } catch (err) {
      console.error("שגיאה בהתחברות:", err);
      alert("אירעה שגיאה בעת ההתחברות");
    }
  };

  return (
    <div style={{ maxWidth: "300px", margin: "auto", paddingTop: "100px" }}>
      <h2>התחברות</h2>
      <input
        placeholder="שם משתמש"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        style={{ display: "block", width: "100%", marginBottom: "10px" }}
      />
      <input
        placeholder="סיסמה"
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        style={{ display: "block", width: "100%", marginBottom: "10px" }}
      />
      <button onClick={handleLogin} style={{ width: "100%" }}>
        התחבר
      </button>
    </div>
  );
}
