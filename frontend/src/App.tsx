import { useState } from 'react'
import './App.css'
import Header from './components/Header/Header'
import LoginModal from './components/LoginModal'

function App() {
  const [showModalLogin, setShowModalLogin] = useState(false)

  return (
    <div className='container'>
      <Header showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
      <LoginModal showModalLogin={showModalLogin} setShowModalLogin={setShowModalLogin} />
    </div>
  )
}

export default App
