import React, { createContext, useContext, useState, ReactNode } from "react";
import { useNavigate } from "react-router-dom";

interface AuthContextType {
  isLoggedIn: boolean;
  userName: string;
  userToken: string | null;
  login: (res: { data: { token: string } }) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [userName, setUserName] = useState<string>("");
  const [userToken, setUserToken] = useState<string | null>(null);
  const navigate = useNavigate();

  const login = (res: { data: { token: string } }) => {
    setIsLoggedIn(true);
    setUserToken(res.data.token);
    navigate("/");
  };

  const logout = () => {
    setIsLoggedIn(false);
    setUserName("");
    setUserToken(null);
  };

  return (
    <AuthContext.Provider value={{ isLoggedIn, userName, userToken, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
