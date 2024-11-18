import React,  {useState} from "react";
import TopbarLogged from "../../components/TopbarLogged/TopbarLogged";
import AnnouncementForm from "../../components/AnnouncementForm/AnnouncementForm";
import './AddAnnouncement.scss'

const AddAnnouncement: React.FC = () => {

  return (
         <section className="addAnnouncement_section">
            <TopbarLogged />
            <div className="announcemenetForm_wrapper">
                <AnnouncementForm />
            </div>
        </section>
    )
  }

export default AddAnnouncement;