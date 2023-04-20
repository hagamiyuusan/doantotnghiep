import React, { useEffect, useState } from 'react'
import Header from '../components/Header'
// import Header from '../components/Header'
import LoginModal from '../components/LoginRegisterModal'
import axios from 'axios'
import { stringify } from 'querystring'
interface IProps {
  children: React.ReactNode
}
export default function MainLayout({ children }: IProps) {
  const [showModalLogin, setShowModalLogin] = useState(false)
  const [user, setUserInfo] = useState();
  // const login = async () => {
  //   const res = await axios.post('https://localhost:7749/api/UserService/register', {
  //     "email": "dathihi121211",
  //     "password": "Chamlohochanh1233@",
  //     "userName": "datzxczxcqwete123st1xzcwqezcx1"
  //   })
  //   // if (res.data) {
  //   // setUserInfo(res.data)
  //   // }
  //   console.log(res);
  // }
  // useEffect(() => {
  //   login()
  //   // console.log(user);
  // }, [])

  return (
    <div>
      <Header showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      {children}
      <LoginModal showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
    </div>
  )
}
