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
                        Tutaj wpiszesz informację o sobie.
                        </div>
                    </Col>
                </Row>
            </Grid>
        );
    }
});
ReactDOM.render(<AboutMe />, document.getElementById('aboutMe'));