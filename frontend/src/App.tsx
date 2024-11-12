import './App.css'
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from './pages/Home/Home.tsx'
import Topbar from './components/Topbar/Topbar.tsx'
import HomeLogged from './pages/HomeLogged/HomeLogged.tsx'

function App() {
  return (
    <Router>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<HomeLogged />} />
        </Routes>
    </Router>
  )
}

export default App
