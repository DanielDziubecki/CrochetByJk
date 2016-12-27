const { Grid, Row, Col } = ReactBootstrap;

var CategoryProducts = React.createClass({
    getInitialState: function () {
         return { data: this.props.products };
    },
    render: function () {
        return (
            <Grid className="image-container">
                <Row>
                    {this.state.data.map((product, index) => {
                        return <Tile product={product} />
                    })}
                </Row>
            </Grid >
        );
    }
});

 var Tile = function(product){
   console.log(product);
    return (
     <Col lg={4} md={4} sm={6} xs={12} className="image-block">
            <div className="image-block-body" style={
            {background: 'url('+ product.PictureUri +')', backgroundSize: '100% 100%' }}>
                <p>{product.Name} </p>
            </div>
        </Col>
    );
}

// ReactDOM.render(<CategoryProducts products={Model} />, document.getElementById('categoryProducts'));
