import React from "react";
import './HomeLogged.scss';
import TopbarLogged from "../../components/TopbarLogged/TopbarLogged";
import Announcement from "../../components/Announcement/Announcement";
import { Col, Divider, Row } from 'antd';
import { AnnouncementProps } from "../../types";

const HomeLogged: React.FC = () => {

    const announcementsArray: AnnouncementProps[] = [
        {
            title: "Malowanie ścian",
            message:"Potrzebuję pomocy przy odmalowaniu ścian.",
            avatar: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwyXeKDN29AmZgZPLS7n0Bepe8QmVappBwZCeA3XWEbWNdiDFB",
            author: "Janek"
        },
        {
            title:"Potrzebuję pomocy przy zakupach",
            message:"Czy ktoś może mi pomóc z zakupami?",
            avatar:"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwyXeKDN29AmZgZPLS7n0Bepe8QmVappBwZCeA3XWEbWNdiDFB",
            author:"Joanna"
        },
        {
            title:"Potrzebne koce i poduszki do schroniska",
            message:"Schronisko dla zwierząt w Warszawie poszukuje kocy i poduszek na zimę.",
            avatar:"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwyXeKDN29AmZgZPLS7n0Bepe8QmVappBwZCeA3XWEbWNdiDFB",
            author:"Szymon"
        },
        {
            title:"Szukamy wolonatriuszy",
            message:"Szukamy wolontariuszy chętnych do pomocy przy sprzątaniu parku miejskiego.",
            avatar:"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwyXeKDN29AmZgZPLS7n0Bepe8QmVappBwZCeA3XWEbWNdiDFB",
            author:"Krzysiek"
        },
        {
            title:"Listy dla samotnych",
            message:"Rusza kolejna akcja pisania listów dla osób samotnych. Chętnych zapraszamy do kontaktu!",
            avatar:"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwyXeKDN29AmZgZPLS7n0Bepe8QmVappBwZCeA3XWEbWNdiDFB",
            author:"Katarzyna"
        },
        {
            title:"Szukamy świętego Mikołaja",
            message:"Czy ktoś jest chętny do przebrania się za Mikołaja i odwiedzenia dzieci w domu dziecka?",
            avatar:"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwyXeKDN29AmZgZPLS7n0Bepe8QmVappBwZCeA3XWEbWNdiDFB",
            author:"Karol"
        }
    ];

    return (
        <section className="homeLogged_section">
            <TopbarLogged />
            <div className="homeLogged_wrapper">
                <div className="latestAnnouncements">
                    <h1>Najnowsze ogłoszenia</h1>
                    <div className="latestAnnouncements_wrapper">
                        {announcementsArray.map((announcement, index) => (
                            <Announcement 
                            key={index}
                            {...announcement}
                            />
                         ))}
                    </div>
                </div>
            </div>
        </section>
    )
}

export default HomeLogged;