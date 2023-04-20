import React, { useState } from 'react'
import Header from '../components/Header'
// import Header from '../components/Header'
import LoginModal from '../components/LoginRegisterModal'
interface IProps {
  children: React.ReactNode
}
export default function MainLayout({ children }: IProps) {
  const [showModalLogin, setShowModalLogin] = useState(false)

  return (
    <div>
      <Header showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      {children}
      <LoginModal showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
    </div>
  )
}
