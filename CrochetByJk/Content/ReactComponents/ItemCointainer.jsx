const { Grid, Row, Col } = ReactBootstrap;

var Tiles = React.createClass({
    render: function () {
        return (
            <Grid className="image-container">
                <Row>
                    <Col lg={4} md={4} sm={6} xs={12} className="image-block">
                        <div className="image-block-body" style={
                            { background: 'url(/Content/Img/3.jpg)', backgroundSize: '100% 100%' }}>
                            <p> </p>
                        </div>
                    </Col>
                    <Col lg={4} md={4} sm={6} xs={12} className="image-block">
                        <div className="image-block-body" style={
                            { background: 'url(/Content/Img/2.jpg)', backgroundSize: '100% 100%' }}>
                            <p> </p>
                        </div>
                    </Col>
                    <Col lg={4} md={4} sm={6} xs={12} className="image-block">
                        <div className="image-block-body" style={
                            { background: 'url(/Content/Img/1.jpg)', backgroundSize: '100% 100%' }}>
                            <p> </p>
                        </div>
                    </Col>
                </Row>
            </Grid >
        );
    }
});

ReactDOM.render(<Tiles />, document.getElementById('tiles'));