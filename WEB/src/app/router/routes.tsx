import { createBrowserRouter, Navigate } from "react-router";
import App from "../components/App";
import ErrorPage from "../../features/errors/ErrorPage";
import NotFound from "../../features/errors/NotFound";
import Dashboard from "../components/Dashboard";
import LoginForm from "../../features/auth/LoginForm";
import RequireAuth from "./RequireAuth";

export const routes = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <Navigate to="/dashboard" replace /> },
      { element: <RequireAuth/>, 
         children: [
            { path: "dashboard", element: <Dashboard /> }
        ] 
      },
      { path: "login", element: <LoginForm /> },
    ],
  },
  { path: "*", element: <NotFound /> },
]);
