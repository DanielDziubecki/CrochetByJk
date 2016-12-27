const {Button ,Grid,Row,Col} = ReactBootstrap;

var Welcome = React.createClass({
    render: function () {
        return (
            <Grid className="welcome ">
                <Row>
                    <Col>
                        <div className="ourPhoto" style={
                            { background: 'url(/Content/Img/my.jpg)', backgroundSize: '100% 100%' }}>
               
                        </div>
                        <div className="welcomeSentence">
                            Kochana Mamuniu! <br/>
                Z okazji Twoich urodzin przygotowałem dla Ciebie coś specjalnego! :)
                Chciałbym, aby jeszcze większe grono osób mogło podziwiać Twoje niesamowite rękodzieło, w które wkładasz całe serce! :)
                Do efektu końcowego jeszcze trochę brakuje, dlatego też liczę na Twoje pomysły i sugestie, aby strona mogła jeszcze lepiej odzwieciedlać Twoją pasję.
                Jeszcze raz życzymy wszystkiego najlepszego!
                </div>
                    </Col>
                    <Row>
                    <a href="http://crochetbyjk.pl/home/index" className="moveForward">
                        <Button bsStyle="success" > Przejdź dalej!</Button>
                    </a></Row>
                </Row>
            </Grid>



        );
    }
});

ReactDOM.render(<Welcome />, document.getElementById('welcomeComponent'));