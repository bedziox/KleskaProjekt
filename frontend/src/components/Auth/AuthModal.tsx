import React, { useState, useImperativeHandle, forwardRef } from 'react';
import { Modal, Button, Typography } from 'antd';
import Login from '../Login/Login';
import Register from '../Register/Register';
import './AuthModal.scss';

const { Text } = Typography;

interface AuthModalProps {
  defaultMode?: "login" | "register";
}

const AuthModal = forwardRef(({ defaultMode = "login" }: AuthModalProps, ref) => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isRegister, setIsRegister] = useState(defaultMode === "register");
  const [fade, setFade] = useState(false);

  useImperativeHandle(ref, () => ({
    showModal: () => {
      setIsModalVisible(true);
      setIsRegister(defaultMode === "register");
    },
  }));

  const handleCancel = () => {
    setIsModalVisible(false);
    setIsRegister(defaultMode === "register");
  };

  const handleToggleAuth = () => {
    setFade(true);
    setTimeout(() => {
      setIsRegister(!isRegister);
      setFade(false);
    }, 300);
  };

  return (
    <Modal
      title={isRegister ? "Rejestracja" : "Logowanie"}
      visible={isModalVisible}
      onCancel={handleCancel}
      footer={null}
    >
      <div className={`fade-container ${fade ? "fade-out" : "fade-in"}`}>
        {isRegister ? <Register /> : <Login />}
      </div>
      <Text type="secondary">
        {isRegister ? "Masz już konto?" : "Nie masz konta?"}{" "}
        <Button type="link" onClick={handleToggleAuth}>
          {isRegister ? "Zaloguj się!" : "Zarejestruj się!"}
        </Button>
      </Text>
    </Modal>
  );
});

export default AuthModal;
