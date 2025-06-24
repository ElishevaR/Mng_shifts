// import { useEffect, useState } from "react";
// import { getSwapRequestsByEmployee, respondToProposal } from "../api/api";
// import type { SwapRequest } from "../models/models";

// export default function OpenSwapProcesses() {
//   const [myRequests, setMyRequests] = useState<SwapRequest[]>([]);
//   const [myProposals, setMyProposals] = useState<SwapRequest[]>([]);
//   const [loading, setLoading] = useState(true);

//   const userId = JSON.parse(localStorage.getItem("userId") || "null");

//   useEffect(() => {
//     if (!userId) return;

//     getSwapRequestsByEmployee(userId)
//       .then((requests) => {
//         const initiated = requests.filter((r) => r.shift.employeeId === userId);
//         const participated = requests.filter((r) =>
//           r.swapProposals?.some((p) =>
//             p.proposedShift.employeeId === userId
//           )
//         );
//         setMyRequests(initiated);
//         setMyProposals(participated);
//       })
//       .finally(() => setLoading(false));
//   }, []);

//   const handleResponse = async (proposalId: number, action: "approve" | "reject") => {
//     try {
//       await respondToProposal(proposalId, action);
//       const updated = await getSwapRequestsByEmployee(userId);
//       setMyRequests(updated.filter((r) => r.shift.employeeId === userId));
//     } catch {
//       alert("Failed to update proposal status");
//     }
//   };

//   if (loading) return <div>Loading...</div>;

//   return (
//     <div >
//       <h2>Swap History</h2>

//       <h3>Swap Requests I Opened</h3>
//       {myRequests.map((req) => (
//         <div key={req.id} style={{ border: "1px solid #ccc", marginBottom: "10px", padding: "10px" }}>
//           <strong>{new Date(req.shift.date).toLocaleDateString()} - {req.shift.type}</strong> ({req.status})
//           <ul>
//             {req.swapProposals?.map((proposal) => (
//               <li key={proposal.id}>
//                 Proposal: {new Date(proposal.proposedShift.date).toLocaleDateString()} - {proposal.proposedShift.type} by {proposal.proposedShift.employee.username} | Status: {proposal.status}
//                 {proposal.status === "WaitingForApproval" && (
//                   <>
//                     <button onClick={() => handleResponse(proposal.id, "approve")}>Approve</button>
//                     <button onClick={() => handleResponse(proposal.id, "reject")}>Reject</button>
//                   </>
//                 )}
//               </li>
//             )) || <p>No proposals yet</p>}
//           </ul>
//         </div>
//       ))}

//       <h3>Swap Requests I Responded To</h3>
//       <ul>
//         {myProposals.map((req) => (
//           <li key={req.id}>
//             Request for {new Date(req.shift.date).toLocaleDateString()} - {req.shift.type} | Status: {req.status}
//           </li>
//         ))}
//       </ul>
//     </div>
//   );
// }


import { useEffect, useState } from "react";
import { getSwapRequestsByEmployee, respondToProposal } from "../api/api";
import type { SwapRequest } from "../models/models";
import {
  Box,
  Typography,
  Paper,
  Divider,
  Button,
  List,
  ListItem,
  ListItemText,
  Stack
} from "@mui/material";

export default function OpenSwapProcesses() {
  const [myRequests, setMyRequests] = useState<SwapRequest[]>([]);
  const [myProposals, setMyProposals] = useState<SwapRequest[]>([]);
  const [loading, setLoading] = useState(true);

  const userId = JSON.parse(localStorage.getItem("userId") || "null");

  useEffect(() => {
    if (!userId) return;

    getSwapRequestsByEmployee(userId)
      .then((requests) => {
        const initiated = requests.filter((r) => r.shift.employeeId === userId);
        const participated = requests.filter((r) =>
          r.swapProposals?.some((p) =>
            p.proposedShift.employeeId === userId
          )
        );
        setMyRequests(initiated);
        setMyProposals(participated);
      })
      .finally(() => setLoading(false));
  }, []);

  const handleResponse = async (proposalId: number, action: "approve" | "reject") => {
    try {
      await respondToProposal(proposalId, action);
      const updated = await getSwapRequestsByEmployee(userId);
      setMyRequests(updated.filter((r) => r.shift.employeeId === userId));
    } catch {
      alert("שגיאה בעדכון סטטוס ההצעה");
    }
  };

  if (loading) return <div>טוען...</div>;

  return (
    <Box sx={{ direction: "rtl", maxWidth: 900, mx: "auto", mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        היסטוריית תהליכי החלפה
      </Typography>

      <Divider sx={{ my: 2 }} />

      <Typography variant="h6" gutterBottom>
        בקשות להחלפה שאני יזמתי
      </Typography>
      {myRequests.length === 0 ? (
        <Typography color="text.secondary">לא פתחת בקשות להחלפה</Typography>
      ) : (
        myRequests.map((req) => (
          <Paper key={req.id} sx={{ mb: 3, p: 2 }} elevation={2}>
            <Typography variant="subtitle1" gutterBottom>
              {new Date(req.shift.date).toLocaleDateString("he-IL")} - {req.shift.type} | סטטוס: {req.status}
            </Typography>
            <Divider sx={{ mb: 1 }} />
            <List>
              {req.swapProposals?.length ? (
                req.swapProposals.map((proposal) => (
                  <ListItem key={proposal.id} sx={{ display: "block" }}>
                    <ListItemText
                      primary={`הצעה: ${new Date(proposal.proposedShift.date).toLocaleDateString("he-IL")} - ${proposal.proposedShift.type}`}
                      secondary={`מאת: ${proposal.proposedShift.employee.username} | סטטוס: ${proposal.status}`}
                    />
                    {proposal.status === "WaitingForApproval" && (
                      <Stack direction="row" spacing={1} sx={{ mt: 1 }}>
                        <Button
                          variant="contained"
                          color="success"
                          size="small"
                          onClick={() => handleResponse(proposal.id, "approve")}
                        >
                          אשר
                        </Button>
                        <Button
                          variant="outlined"
                          color="error"
                          size="small"
                          onClick={() => handleResponse(proposal.id, "reject")}
                        >
                          דחה
                        </Button>
                      </Stack>
                    )}
                  </ListItem>
                ))
              ) : (
                <Typography color="text.secondary">אין הצעות עדיין</Typography>
              )}
            </List>
          </Paper>
        ))
      )}

      <Divider sx={{ my: 3 }} />

      <Typography variant="h6" gutterBottom>
        בקשות להחלפה שהגבתי אליהן
      </Typography>
      {myProposals.length === 0 ? (
        <Typography color="text.secondary">לא השתתפת בהצעות להחלפה</Typography>
      ) : (
        <List>
          {myProposals.map((req) => (
            <ListItem key={req.id}>
              <ListItemText
                primary={`${new Date(req.shift.date).toLocaleDateString("he-IL")} - ${req.shift.type}`}
                secondary={`סטטוס: ${req.status}`}
              />
            </ListItem>
          ))}
        </List>
      )}
    </Box>
  );
}

