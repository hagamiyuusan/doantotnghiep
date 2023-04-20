import { useState } from 'react'
import './App.css'
import Header from './components/Header/Header'
import LoginModal from './components/LoginRegisterModal'
import useRouterElement from './helps/useRouterElement'

function App() {
  const routerElements = useRouterElement()
  return (
    <>
      {routerElements}
    </>
  )
}

export default App
