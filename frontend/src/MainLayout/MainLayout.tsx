import React, { useContext, useEffect, useState } from 'react'
import Header from '../components/Header'
// import Header from '../components/Header'
import LoginModal from '../components/LoginRegisterModal'
import { AppContext } from 'src/Context/context'
import Footer from 'src/components/Footer'
interface IProps {
  children: React.ReactNode
}
export default function MainLayout({ children }: IProps) {
  const [showModalLogin, setShowModalLogin] = useState(false)
  const user = useContext(AppContext)
  console.log('User from context', user);

  return (
    <div>
      <Header user={user} showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      {children}
      <LoginModal showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      {/* <Footer /> */}

    </div>
  )
}
