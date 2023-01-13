import { BrowserRouter } from 'react-router-dom'
import { Routes, Route } from 'react-router'
import LoginPage from './pages/LogIn/Login'
import SignupPage from './pages/SignUp/Signup'
import StartPage from './pages/StartPage/StartPage'
import Playground from './pages/Playground/Playground'
import BattleList from './pages/BattleList/BattleListPage'
import ProtectedRoutes from './components/ProtectedRoutes'
import { HubConnection } from '@microsoft/signalr'
import { useState } from 'react'

function App() {
  const [connection, setConnection] = useState<HubConnection>( );

  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path='/login' element={<LoginPage />} />
          <Route path='/signup' element={<SignupPage />} />
          <Route path='/' element={<StartPage />} />
          <Route element={<ProtectedRoutes />}>
            <Route path='/playground' element={<Playground />} />
            <Route path='/battlelist' element={<BattleList connection={connection} setConnection={(val : HubConnection) => setConnection(val)}/>} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App
