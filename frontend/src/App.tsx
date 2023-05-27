import './App.css'
import useRouterElement from './helps/useRouterElement'
function App() {
  const routerElements = useRouterElement()
  return <>{routerElements}</>
}

export default App
