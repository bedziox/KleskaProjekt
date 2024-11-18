import React,  {useState} from "react";
import { Avatar, Card, Modal, Button } from 'antd';
import { EditOutlined } from "@ant-design/icons";
import './Announcement.scss';

interface AnnouncementProps {
    title: string,
    message: string,
    avatar: string,
    author: string
}

const Announcement: React.FC<AnnouncementProps> = ({ title, message, avatar, author }) => {
  const [loading, setLoading] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  function truncateDescription(description: string, maxLength: number = 100): string {
    if(description.length <= maxLength) {
      return description;
    }
    return description.slice(0, maxLength) + "...";
  }
  
  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
      <>
      <Card 
      loading={loading} 
      style={{ minWidth: 300, textAlign: 'left', cursor: 'pointer' }}
      onClick={handleOpenModal}
      className="announcementCard"
      >
        <Card.Meta
          avatar={<Avatar src={avatar} />}
          title={title}
          description={
            <>
              <p>{truncateDescription(message)}</p>
              <p>Autor: {author}</p>
            </>
          }
        />
      </Card>

      <Modal
        title=""
        open={isModalOpen}
        onCancel={handleCloseModal}
        footer={[
          <Button key="cancel" onClick={handleCloseModal}>
            Zamknij
          </Button>,
        ]}
        width={1200}
        height={1000}
      >
        <div className="modal_grid">
          <div className="modalAnnouncement">
            <h1 className="modalAnnouncement_title">{title}</h1>
            <p>{message}</p>
            <div className="modalAnnouncement_details">
              <p>Autor: {author}</p>
              <p>Data dodania: 18.11.2024r.</p>
            </div>
          </div>
          <div className="modalContent2">
            <p>tu bedzie zgłaszanie się lub komentarze</p>
          </div>
        </div>
      </Modal>
      </>
    )
  }

export default Announcement;