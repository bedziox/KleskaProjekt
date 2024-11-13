import React,  {useState} from "react";
import { EditOutlined, EllipsisOutlined, SettingOutlined } from '@ant-design/icons';
import { Avatar, Card, Flex, Switch } from 'antd';
import heartHand from '../../assets/heart-hand.svg'

interface AnnouncementProps {
    title: string,
    message: string,
    avatar: string,
    author: string
}

const Announcement: React.FC<AnnouncementProps> = ({ title, message, avatar, author }) => {
    const [loading, setLoading] = useState<boolean>(false);
  

  return (
      <>
      <Card loading={loading} style={{ minWidth: 300 }}>
        <Card.Meta
          avatar={<Avatar src={avatar} />}
          title={title}
          description={
            <>
              <p>{message}</p>
              <p>Autor: {author}</p>
            </>
          }
        />
      </Card>
      </>
    )
  }

export default Announcement;