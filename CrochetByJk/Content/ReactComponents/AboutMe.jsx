const { Image } = ReactBootstrap;

var AboutMe = React.createClass({
    render: function () {
        return (
            <div className="aboutMeContainer">
                <div className="aboutMeHeader">O mnie</div>
                <div className="myPicture" data-aos="flip-right" data-aos-delay="500">
                    <Image src="/Content/Img/me.jpg" circle/>
                </div>
                <div className="aboutMeText" data-aos="fade-up">
                Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat 
                </div>
            </div>
        );
    }
});
ReactDOM.render(<AboutMe />, document.getElementById('aboutMe'));