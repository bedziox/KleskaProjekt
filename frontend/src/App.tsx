import './App.css'
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from './pages/Home/Home.tsx'
import HomeLogged from './pages/HomeLogged/HomeLogged.tsx'
import UserPanel from './pages/UserPanel/UserPanel.tsx'
import { AuthProvider } from './context/AuthContext.tsx'
import AddAnnouncement from './pages/AddAnnouncement/AddAnnouncement.tsx'

function App() {
  return (
    <Router>
      <div className='App'>
        <AuthProvider>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<HomeLogged />} />
          <Route path="/myaccount" element={<UserPanel />} />
          <Route path="/add-announcement" element={<AddAnnouncement />} />
        </Routes>
        </AuthProvider>
      </div>
    </Router>
  )
}

export default App
