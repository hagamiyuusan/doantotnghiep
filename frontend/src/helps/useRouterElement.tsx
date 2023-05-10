import { Navigate, Outlet, useRoutes } from 'react-router-dom'
import MainLayout from '../MainLayout'
import LandingPage from '../pages/LandingPage'
import UserProfile from '../pages/Profile'
import { useContext } from 'react'
import { AppContext } from 'src/Context/context'
import ChangePassword from 'src/pages/ChangePassword'
import DurationManager from 'src/pages/Profile/AdminPage/DurationManager'
import ProductManager from 'src/pages/Profile/AdminPage/ProductManager'

export default function useRouterElement() {
  const { isAuthenticated, profile } = useContext(AppContext)
  const isAdmin = Boolean(profile?.role)
  function ProtectedRoute() {
    return isAuthenticated ? <Outlet /> : <Navigate to='/' />
  }
  function ProtectedAdminRoute() {
    if (isAuthenticated && isAdmin) {
      return <Outlet />
    } else {
      return <Navigate to='/' />
    }
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
      path: 'sendMailChangePassword/:username/:token',
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
    },
    {
      path: '',
      element: <ProtectedAdminRoute />,
      children: [
        {
          path: '/admin/usermanager',
          element: (
            <MainLayout>
              <UserProfile />
            </MainLayout>
          )
        },
        {
          path: '/admin/durationmanager',
          element: (
            <MainLayout>
              <DurationManager />
            </MainLayout>
          )
        },
        {
          path: '/admin/productmanager',
          element: (
            <MainLayout>
              <ProductManager />
            </MainLayout>
          )
        },
        {
          path: '/admin/productdurationmanager',
          element: (
            <MainLayout>
              <DurationManager />
            </MainLayout>
          )
        }
      ]
    }
  ])
  return elementRouters
}
