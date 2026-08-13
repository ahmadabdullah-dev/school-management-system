import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { routes } from "./app/router/routes";
import { ThemeProvider, CssBaseline } from "@mui/material";
import theme from "./lib/theme";
import { RouterProvider } from "react-router";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
          <RouterProvider router={routes} />
    </ThemeProvider>
  </StrictMode>,
);
