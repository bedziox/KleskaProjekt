import React from "react";
import './Home.scss';
import { Button } from "antd";
import heartHand from '../../assets/heart-hand.svg';

const Home = () => {
    return (
        <section className="home_section">
            <div className="home_sectionImageVolunteer">
            </div>
            <div className="home_sectionWrapper">
                <div className="home_sectionWrapperText">
                    <img src={heartHand} width={'50px'}/>
                    <h2>KlęskaProjekt to miejsce, które łączy ludzi potrzebujących wsparcia z tymi, którzy chcą pomóc. Niezależnie od tego, czy potrzebujesz pomocy przy codziennych obowiązkach, takich jak zakupy, malowanie ścian, czy szukasz wsparcia materialnego – tutaj znajdziesz kogoś, kto wyciągnie do Ciebie pomocną dłoń.
                    </h2>
                    <h2>Jeśli masz czas i chęć do działania, możesz zaoferować swoją pomoc innym. Nasza platforma pozwala łatwo dopasować potrzeby do osób gotowych udzielić wsparcia, tworząc społeczność, która działa razem, aby przezwyciężać trudności.
                    </h2>
                    <Button 
                    type="primary"
                    size="large"
                    className="home_animatedBtn"
                    >
                        Dołącz do nas!
                    </Button>
                </div>
            </div>
        </section>
    )
}

export default Home;