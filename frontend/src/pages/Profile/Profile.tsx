import { useContext } from 'react'
import './style.css'
import { AppContext } from 'src/Context/context'
import UserProfile from './UserProfile'
import AdminPage from './AdminPage'
export default function Profile() {
  const { profile } = useContext(AppContext)
  console.log("🚀 ~ file: Profile.tsx:7 ~ Profile ~ profile:", profile)

  return <div className='container'>{profile?.role === 'admin' ? <AdminPage /> : <UserProfile />}</div>
}
