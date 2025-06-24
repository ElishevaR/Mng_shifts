import { type ReactNode } from "react";
import { Navigate, useParams } from "react-router-dom";

export default function ProtectedRoute({ children }: { children: ReactNode }) {
  const username = JSON.parse(localStorage.getItem("username") || "null");
  const params = useParams();

  if (!username || username !== params.username) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
}
