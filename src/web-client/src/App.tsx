import { BrowserRouter } from 'react-router-dom'
import { Routes, Route} from 'react-router'
import LoginPage from './pages/LogIn/Login'
import SignupPage from './pages/SignUp/Signup'
import StartPage from './pages/StartPage/StartPage'
import Playground from './pages/Playground/Playground'
import BattleList from './pages/BattleList/BattleListPage'

function App() {

  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path='/playground' element={<Playground/>}/>
          <Route path='/' element={<StartPage/>}/>
          <Route path='/login' element={<LoginPage/>}/>
          <Route path='/signup' element={<SignupPage/>}/>
          <Route path='/battlelist' element={<BattleList/>}/>
        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App
