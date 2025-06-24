import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Table, TableHead, TableRow, TableCell, TableBody, Paper, Button, Typography
} from "@mui/material";
import { type Shift } from "../models/models";
import { getShiftsByEmployee, createSwapRequest } from "../api/api";

export default function MyShifts() {
  const navigate = useNavigate();
  const [shifts, setShifts] = useState<Shift[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedShiftId, setSelectedShiftId] = useState<number | null>(null);
  const [comment, setComment] = useState("");

  const userId = JSON.parse(localStorage.getItem("userId") || "null");

  useEffect(() => {
    if (!userId) {
      navigate("/login");
      return;
    }

    const loadShifts = async () => {
      try {
        const data = await getShiftsByEmployee(userId);
        setShifts(data);
      } catch (err) {
        console.error("שגיאה בטעינת משמרות:", err);
        alert("שגיאה בטעינת המשמרות");
      } finally {
        setLoading(false);
      }
    };

    loadShifts();
  }, []);

  const handleSubmitRequest = async (shiftId: number) => {
    try {
      await createSwapRequest(shiftId); // אפשר להרחיב ולשלוח גם comment אם API תומך
      // רענון מהשרת
      const updated = await getShiftsByEmployee(userId);
      setShifts(updated);

      setSelectedShiftId(null);
      setComment("");
    } catch (err) {
      console.error(err);
      alert("שגיאה בשליחת הבקשה");
    }
  };

  if (loading) return <div>טוען...</div>;

  return (
    <Paper
      sx={{
        maxWidth: 900,
        margin: "50px auto",
        padding: 3,
        direction: "rtl", // כיוון מימין לשמאל
        textAlign: "right", // יישור טקסט לימין
      }}
      elevation={3}
    >
      <Typography variant="h5" gutterBottom>
        המשמרות שלי
      </Typography>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>תאריך</TableCell>
            <TableCell>סוג משמרת</TableCell>
            <TableCell>סטטוס</TableCell>
            <TableCell align="center">פעולה</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {shifts.map((shift) => (
            <TableRow key={shift.id}>
              <TableCell>{new Date(shift.date).toLocaleDateString("he-IL")}</TableCell>
              <TableCell>{shift.type}</TableCell>
              <TableCell>{shift.status}</TableCell>
              <TableCell align="center">
                {shift.status === "Regular" && (
                  <Button
                    variant="contained"
                    size="small"
                    onClick={() => setSelectedShiftId(shift.id)}
                  >
                    בקש החלפה
                  </Button>
                )}

                {shift.status === "SwapRequested" && (
                  <Typography variant="body2" color="text.secondary">
                    בקשת החלפה נשלחה
                  </Typography>
                )}

                {selectedShiftId === shift.id && (
                  <div style={{ marginTop: 8 }}>
                    <input
                      type="text"
                      placeholder="הערה (לא חובה)"
                      value={comment}
                      onChange={(e) => setComment(e.target.value)}
                      style={{ width: "100%", marginBottom: 4, textAlign: "right" }}
                    />
                    <Button
                      variant="outlined"
                      size="small"
                      onClick={() => handleSubmitRequest(shift.id)}
                    >
                      שלח בקשה
                    </Button>
                  </div>
                )}
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}
