// Modified by AI on 05/11/2026. Edit #1.
// Modified by AI on 05/12/2026. Edit #2.
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ThemeProvider, CssBaseline } from '@mui/material';
import { AppBar, Toolbar, Typography, Button, Box } from '@mui/material';
import theme from './theme';
import { useMe } from './api/participantApi';
import DashboardPage from './pages/DashboardPage';
import AdminDashboardPage from './pages/AdminDashboardPage';
import ReviewSubmissionPage from './pages/ReviewSubmissionPage';
import ParticipantDetailPage from './pages/ParticipantDetailPage';
import TeamProgressPage from './pages/TeamProgressPage';
import ProtectedRoute from './components/ProtectedRoute';

const queryClient = new QueryClient();

function NavBar() {
  const { data: me } = useMe();

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component={Link} to="/" sx={{ flexGrow: 1, textDecoration: 'none', color: 'inherit' }}>
          AI Trail Tracker
        </Typography>
        {me && (
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
            <Typography variant="body2">{me.displayName}</Typography>
            {me.isAdmin && (
              <Button color="inherit" component={Link} to="/admin">
                Admin
              </Button>
            )}
            {me.isAdmin && (
              <Button color="inherit" component={Link} to="/admin/teams">
                Teams
              </Button>
            )}
          </Box>
        )}
      </Toolbar>
    </AppBar>
  );
}

function NotFound() {
  return (
    <Box sx={{ textAlign: 'center', mt: 10 }}>
      <Typography variant="h4">404 — Page Not Found</Typography>
      <Button component={Link} to="/" sx={{ mt: 2 }}>Go Home</Button>
    </Box>
  );
}

function AppRoutes() {
  return (
    <>
      <NavBar />
      <Routes>
        <Route path="/" element={<DashboardPage />} />
        <Route
          path="/admin"
          element={
            <ProtectedRoute requiredRole="admin">
              <AdminDashboardPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/review/:userId"
          element={
            <ProtectedRoute requiredRole="admin">
              <ReviewSubmissionPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/participant/:userId"
          element={
            <ProtectedRoute requiredRole="admin">
              <ParticipantDetailPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/teams"
          element={
            <ProtectedRoute requiredRole="admin">
              <TeamProgressPage />
            </ProtectedRoute>
          }
        />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </>
  );
}

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <BrowserRouter>
          <AppRoutes />
        </BrowserRouter>
      </ThemeProvider>
    </QueryClientProvider>
  );
}

export default App;
