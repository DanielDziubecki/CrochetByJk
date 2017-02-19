const { Grid, Row, Col } = ReactBootstrap;

var CategoryProducts = React.createClass({
    getInitialState: function () {
        return {
            products: this.props.products,
            categoryName: this.props.categoryName
        };
    },
    render: function () {
        return (
            <Grid className="image-container">
                {/*<Row className="headermessage">*/}
                <h2>{this.state.categoryName}</h2>
                {/*</Row>*/}
                <Row>
                    {GenerateTiles(this.state.products)}
                </Row>
            </Grid >
        );
    }
});

var Tile = ({product, animtation, delay}) => (
    <Col lg={4} md={4} sm={6} xs={12}
        className="image-block"
        onClick={GoToProductDetails(product.ProductUrl)}
        data-aos={animtation}
        data-aos-delay={delay}>

        <div className="image-block-body" style={
            { background: 'url(' + product.PictureUri + ')', backgroundSize: '100% 100%' }}>
            <p>{product.Name} </p>
        </div>
    </Col>
);

function GenerateTiles(products) {
    var counter = 0;
    return products.map((product, index) => {
        var animtation = "fade-up"
        counter++;
        if (counter == 4)
            counter = 1;
        return <Tile product={product} animtation={animtation} delay={counter * 300} />
    })
}
function GoToProductDetails(url) {
    return function (e) {
        document.location.href = url;
    };
}

