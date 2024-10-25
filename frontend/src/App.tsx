import './App.css'
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from './pages/Home/Home.tsx'
import Topbar from './components/Topbar/Topbar.tsx'

function App() {
  return (
    <Router>
        <Topbar />
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>
    </Router>
  )
}

export default App
