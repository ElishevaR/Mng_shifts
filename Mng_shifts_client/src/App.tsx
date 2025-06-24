// import React from "react";
// import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

// import Home from "./pages/Home";
// import Login from "./pages/Login";
// import MyShifts from "./pages/MyShifts";
// import OpenSwapRequests from "./pages/OpenSwapRequests";
// import OpenSwapProcesses from "./pages/SwapProcesses";


// function App() {
//   return (
//     <Router>
//       <Routes>
//         {/* עמוד הבית פתוח לכולם */}
//         <Route path="/" element={<Home />} />

//         {/* עמוד התחברות */}
//         <Route path="/login" element={<Login />} />

//         {/* עמודים שמחייבים התחברות */}
//         <Route
//           path="/user/:username/shifts"
//           element={
//               <MyShifts />
//           }
//         />
//         <Route
//           path="/user/:username/requests"
//           element={
//               <OpenSwapRequests />
//           }
//         />
//         <Route
//           path="/user/:username/swaps"
//           element={
//               <OpenSwapProcesses />
//           }
//         />

//         {/* ברירת מחדל לדף לא קיים */}
//         <Route path="*" element={<div>404 - הדף לא נמצא</div>} />
//       </Routes>
//     </Router>
//   );
// }

// export default App;






import React from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";

import Navbar from "./components/Navbar";
import Login from "./pages/Login";
import MyShifts from "./pages/MyShifts";
import OpenSwapRequests from "./pages/OpenSwapRequests";
import OpenSwapProcesses from "./pages/SwapProcesses";
import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<Login />} />
        <Route
          path="/user/:username/shifts"
          element={
            <ProtectedRoute>
              <MyShifts />
            </ProtectedRoute>
          }
        />
        <Route
          path="/user/:username/requests"
          element={
            <ProtectedRoute>
              <OpenSwapRequests />
            </ProtectedRoute>
          }
        />
        <Route
          path="/user/:username/swaps"
          element={
            <ProtectedRoute>
              <OpenSwapProcesses />
            </ProtectedRoute>
          }
        />
        <Route path="*" element={<div>404 - הדף לא נמצא</div>} />
      </Routes>
    </Router>
  );
}

export default App;
