import './App.css'
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from './pages/Home.tsx'
import Topbar from './components/Topbar.tsx'

function App() {
  return (
    <Router>
      <div className='App'>
        <Topbar />
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App
