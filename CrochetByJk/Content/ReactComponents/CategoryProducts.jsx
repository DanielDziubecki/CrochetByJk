var CategoryProducts = React.createClass({
    getInitialState: function () {
        return {
            products: this.props.products
        };
    },
    render: function () {
        return (
            <div className="image-container">
                {GenerateTiles(this.state.products)}
            </div>
        );
    }
});

var Tile = ({product, animtation, delay}) => (
    <div
        className="image-block"
        onClick={GoToProductDetails(product.ProductUrl)}
        data-aos={animtation}
        data-aos-delay={delay}>
        <div className="image-block-body" style={
            { height:product.Height, width:product.Width,
             background: 'url(' + product.PictureUri + ')', backgroundSize: '100% 100%' }}>
            <p>{product.Name} </p>
        </div>
    </div>
);

function GenerateTiles(products) {
    var counter = 0;
    return products.map((product, index) => {
        var animtation = "fade-up"
        counter++;
        if (counter == 4)
            counter = 1;
        if(product.Width)
        return <Tile product={product} animtation={animtation} delay={counter * 300} />
    })
}

function GoToProductDetails(url) {
    return function (e) {
        document.location.href = url;
    };
}
