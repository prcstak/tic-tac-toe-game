import { BrowserRouter } from 'react-router-dom'
import { Router, Routes, Route} from 'react-router'
import Playground from './components/Playground/Playground'
import StartPage from './components/Authorization/StartPage'
import LoginPage from './components/Authorization/Login'
import SignupPage from './components/Authorization/Signup'

function App() {

  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path='/playground' element={<Playground/>}/>
          <Route path='/' element={<StartPage/>}/>
          <Route path='/login' element={<LoginPage/>}/>
          <Route path='/signup' element={<SignupPage/>}/>
        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App
