import { useContext } from 'react'
import { AppContext } from 'src/Context/context'
import UserProfile from './UserPage/UserProfile'
import AdminPage from './AdminPage/AdminPage'
export default function Profile() {
  const { profile } = useContext(AppContext)

  return <div className='container'>{profile?.role === 'admin' ? <AdminPage /> : <UserProfile />}</div>
}
