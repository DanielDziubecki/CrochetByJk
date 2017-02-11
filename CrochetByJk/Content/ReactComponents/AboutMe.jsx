const { Image } = ReactBootstrap;

var AboutMe = React.createClass({
    render: function () {
        return (

            <Grid className="aboutMeContainer">
                <Row>
                    <Col>
                        <div className="aboutMeHeader">O mnie</div>
                        <div className="myPicture" data-aos="flip-right" data-aos-delay="500">
                            <Image src="/Content/Img/me.jpg" circle />
                        </div>
                        <div className="aboutMeText" data-aos="fade-up">
                        Rękodziełem zajmuje się od 30 lat. Moje wyroby charakteryzuje dokładność i dbanie o szczegóły. Każdy wykonany produkt sprawdzam osobiście jak będzie zachowywał się w przyszłości udzielam rad jak o niego dbać aby służył jak najdłużej .
Moje projekty są indywidualne i rzadko je powielam aby zachować ich unikalność.Wykorzystuję do swoich prac moje własne wzory lub inspiruję się ogólnie dostępnymi w sieci
                        </div>
                    </Col>
                </Row>
            </Grid>
        );
    }
});
ReactDOM.render(<AboutMe />, document.getElementById('aboutMe'));