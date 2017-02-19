var ProductDetails = React.createClass({
    getInitialState: function () {
        return {
            product: this.props.product
        };
    },
    render() {
        return (
            <div>
                <div id="galleryContainer">
                    <div id="gallery">
                        {this.state.product.PictureUrls.map((url, index) => {
                            return <img src={url} data-image={url} />
                        })}
                    </div>
                </div>
                <div id="productName">
                    <h2>{this.state.product.Name}</h2>
                </div>
                <div id="productDescription">
                    {this.state.product.Description}
                </div>
                <div style={{clear:"both"}}></div>
            </div>)
    },
    componentDidMount() {
        var api = $("#gallery").unitegallery({
            gallery_theme: "grid",
            theme_panel_position: "bottom",
            slider_scale_mode: "fit",
            slider_control_swipe: false,
            slider_control_zoom: false,
            slider_enable_zoom_panel: false,
            slider_enable_play_button: false,
            gallery_width: 500,
            gallery_height: 500,
            gallery_min_width: 500,
            gallery_min_height: 500,
        });
        addEventListeners(api);
        api.on("item_change", function (num, data) {
            addEventListeners(api);
        });
        api.on("exit_fullscreen", function () {
            $('html, body').animate({
                scrollTop: $("#galleryContainer").offset().top
            });
        });
    }
});

function addEventListeners(api) {
    var images = document.getElementById("gallery").getElementsByTagName("img");
    for (var i = 0; i < images.length; i++) {
        images[i].addEventListener("click", function () {
            api.enterFullscreen();
        });
    }
}

