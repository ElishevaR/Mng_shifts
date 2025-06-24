// src/pages/OpenSwapRequests.tsx

import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Table, TableHead, TableRow, TableCell, TableBody,
  Paper, Button, Typography, Dialog, DialogTitle,
  DialogContent, DialogActions, Select, MenuItem
} from "@mui/material";

import {
  getOpenSwapRequests,
  getShiftsByEmployee,
  createSwapProposal
} from "../api/api";

import type { SwapRequest, Shift } from "../models/models";

export default function OpenSwapRequests() {
  const [requests, setRequests] = useState<SwapRequest[]>([]);
  const [myShifts, setMyShifts] = useState<Shift[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedRequest, setSelectedRequest] = useState<SwapRequest | null>(null);
  const [selectedShiftId, setSelectedShiftId] = useState<number | null>(null);

  const navigate = useNavigate();
  const userId = JSON.parse(localStorage.getItem("userId") || "null");

  useEffect(() => {
    if (!userId) {
      navigate("/login");
      return;
    }

    getOpenSwapRequests(userId)
      .then(setRequests)
      .catch(() => alert("שגיאה בטעינת הבקשות"));

    getShiftsByEmployee(userId)
      .then(setMyShifts)
      .catch(() => alert("שגיאה בטעינת המשמרות"))
      .finally(() => setLoading(false));
  }, []);

  const handleSubmitProposal = async () => {
    if (!selectedRequest || !selectedShiftId) return;



    try {
      await createSwapProposal(selectedRequest.id,selectedShiftId);
      alert("הצעה נשלחה בהצלחה");
      setSelectedRequest(null);
      setSelectedShiftId(null);
    } catch (err) {
      console.error(err);
      alert("שגיאה בשליחת הצעה");
    }
  };

  if (loading) return <div>טוען בקשות...</div>;

  return (
    <Paper sx={{ maxWidth: 900, margin: "50px auto", padding: 3 }}>
      <Typography variant="h5" gutterBottom>בקשות פתוחות להחלפה</Typography>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>שם העובד</TableCell>
            <TableCell>תאריך</TableCell>
            <TableCell>סוג משמרת</TableCell>
            <TableCell align="center">פעולה</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {requests.map((req) => (
            <TableRow key={req.id}>
              <TableCell>{req.shift.employee.username}</TableCell>
              <TableCell>{new Date(req.shift.date).toLocaleDateString()}</TableCell>
              <TableCell>{req.shift.type}</TableCell>
              <TableCell align="center">
                <Button
                  variant="contained"
                  color="secondary"
                  onClick={() => setSelectedRequest(req)}
                >
                  הצע להחליף איתי משמרת
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <Dialog open={!!selectedRequest} onClose={() => setSelectedRequest(null)}>
        <DialogTitle>בחר משמרת שלך להצעה</DialogTitle>
        <DialogContent>
          <Select
            fullWidth
            value={selectedShiftId ?? ""}
            onChange={(e) => setSelectedShiftId(Number(e.target.value))}
            displayEmpty
          >
            <MenuItem value="" disabled>בחר משמרת</MenuItem>
            {myShifts.map((shift) => (
              <MenuItem key={shift.id} value={shift.id}>
                {new Date(shift.date).toLocaleDateString()} - {shift.type}
              </MenuItem>
            ))}
          </Select>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setSelectedRequest(null)}>ביטול</Button>
          <Button
            onClick={handleSubmitProposal}
            variant="contained"
            disabled={!selectedShiftId}
          >
            שלח הצעה
          </Button>
        </DialogActions>
      </Dialog>
    </Paper>
  );
}
