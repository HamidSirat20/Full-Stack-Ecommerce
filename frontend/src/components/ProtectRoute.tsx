import React, { ReactNode } from "react";
import { Navigate } from "react-router-dom";

interface Props {
  element: ReactNode;
  allowedRoles?: string[];
}

const ProtectedRoute: React.FC<Props> = ({ element, allowedRoles }) => {
  const storedRole = localStorage.getItem("Role");
  const userRole = storedRole ? storedRole.replace(/^"(.*)"$/, "$1") : null;

  if (userRole === "Admin") {
    return <>{element}</>;
  } else {
    return <Navigate to="/unauthorized" />;
  }
};

export default ProtectedRoute;
