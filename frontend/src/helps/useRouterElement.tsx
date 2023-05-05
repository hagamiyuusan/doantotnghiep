import { Navigate, Outlet, useRoutes } from 'react-router-dom'
import MainLayout from '../MainLayout'
import LandingPage from '../pages/LandingPage'
import UserProfile from '../pages/Profile'
import { useContext } from 'react'
import { AppContext } from 'src/Context/context'
import ChangePassword from 'src/pages/ChangePassword'

export default function useRouterElement() {
  const { isAuthenticated } = useContext(AppContext)
  function ProtectedRoute() {
    return isAuthenticated ? <Outlet /> : <Navigate to='/' />
  }
  function RejectRoute() {
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
    },
    {
      path: 'sendMailChangePassword',
      element: <ChangePassword />
    },
    {
      path: 'changePassword/:username/:token',
      element: <ChangePassword />
    },
    {
      path: '',
      element: <ProtectedRoute />,
      children: [
        {
          path: 'profile',
          element: (
            <MainLayout>
              <UserProfile />
            </MainLayout>
          )
        }
      ]
    }
  ])
  return elementRouters
}
