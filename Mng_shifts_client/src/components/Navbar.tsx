import { Link } from "react-router-dom";
import { AppBar, Toolbar, Typography, Box, Button } from "@mui/material";

export default function Navbar() {
  const username = JSON.parse(localStorage.getItem("username") || "null");
  const displayName = username ? `שלום ${username}` : "שלום אורח";
  const routeUser = username || "guest";

  return (
    <AppBar position="static" elevation={0} color="transparent" sx={{ borderBottom: 1, borderColor: "divider" }}>
      <Toolbar sx={{ justifyContent: "space-between", direction: "rtl" }}>
        {/* שם מערכת */}
        <Typography variant="h6" component="div" sx={{ fontWeight: "bold" }}>
          מערכת לניהול עובדים
        </Typography>

        {/* קישורים */}
        <Box sx={{ display: "flex", gap: 2 }}>
          <Button component={Link} to={`/user/${routeUser}/shifts`} color="inherit">
            משמרות
          </Button>
          <Button component={Link} to={`/user/${routeUser}/requests`} color="inherit">
            בקשות
          </Button>
          <Button component={Link} to={`/user/${routeUser}/swaps`} color="inherit">
            החלפות
          </Button>
        </Box>

        {/* שם משתמש */}
        <Typography variant="body1">{displayName}</Typography>
      </Toolbar>
    </AppBar>
  );
}
