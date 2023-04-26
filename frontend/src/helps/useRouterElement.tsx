import { Navigate, Outlet, useRoutes } from 'react-router-dom'
import MainLayout from '../MainLayout'
import LandingPage from '../pages/LandingPage'

export default function useRouterElement() {

  function ProtectedRoute() {
    const isAuthenticated = true
    return isAuthenticated ? <Outlet /> : <Navigate to='/' />
  }
  function RejectRoute() {
    const isAuthenticated = false
    return !isAuthenticated ? <Outlet /> : <Navigate to='/' />

  }
  const elementRouters = useRoutes([
    {
      path: '/',
      element: (
        <MainLayout>
          <LandingPage />
        </MainLayout>
      )
    }
  ])
  return elementRouters
}
