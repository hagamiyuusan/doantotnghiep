import { useRoutes } from 'react-router-dom'
import MainLayout from '../MainLayout'
// import LoginModal from '~/components/LoginRegisterModal'
import OCR from '../pages/OCR'

export default function useRouterElement() {

  const elementRouters = useRoutes([
    {
      path: '/',
      element: (
        <MainLayout>
          <OCR />
        </MainLayout>
      )
    }
  ])
  return elementRouters
}
