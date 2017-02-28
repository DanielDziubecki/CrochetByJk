const {Form, Modal, Col, FormGroup, FormControl, ControlLabel, Button} = ReactBootstrap;

var ProductDetails = React.createClass({
    getInitialState: function () {
        return {
            product: this.props.product
        };
    },
    render() {
        return (
            <div id="productDetails">
                <div id="galleryContainer">
                    <div id="gallery">
                        {this.state.product.PictureUrls.map((url, index) => {
                            return <img src={url} data-image={url} />
                        })}
                    </div>
                </div>
                <div id="productName">
                    <h2>{this.state.product.Name}</h2>
                    <p>{this.state.product.Description}</p>
                    <div id="contactForm">
                        <AskForProduct />
                    </div>
                </div>

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
            // gallery_width:"100%",	
            // gallery_height:"100%",
            // gallery_min_width: "100%",
            gallery_min_height: 500,
            thumb_selected_border_width: 3,
            thumb_selected_border_color: "#67A3D9",
        });
        addFullScreenListeners(api);
        api.on("item_change", function (num, data) {
            addFullScreenListeners(api);
        });
        api.on("exit_fullscreen", function () {
            $('html, body').animate({
                scrollTop: $("#galleryContainer").offset().top
            });
        });
    }
});
const AskForProduct = React.createClass({
    getInitialState() {
        return { show: false };
    },

    render() {
        let close = () => this.setState({ show: false });

        return (
            <div className="modal-container">

                <Button
                    className="adminButton"
                    id="askForProductButton"
                    bsStyle="primary"
                    bsSize="large"
                    onClick={() => this.setState({ show: true })}>
                    Zapytaj o produkt
                    </Button>

                <Modal
                    show={this.state.show}
                    onHide={close}
                    container={this}
                    aria-labelledby="contained-modal-title">
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title">Zapytaj o produkt</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form horizontal id="productQuestionForm">
                            <FormGroup
                                controlId="emailAdress"
                                htmlFor="email">
                                <Col componentClass={ControlLabel}>
                                    Email
                                   </Col>
                                <Col>
                                    <FormControl
                                        placeholder="Email"
                                        name="email"
                                        type="email"
                                        minLength="5"
                                        required />
                                </Col>
                            </FormGroup>
                            <FormGroup
                                controlId="emailTopic"
                                htmlFor="topic">
                                <Col componentClass={ControlLabel}>
                                    Temat
                                   </Col>
                                <Col>
                                    <FormControl
                                        placeholder="Temat"
                                        name="topic"
                                        type="text"
                                        minLength="5"
                                        required />
                                </Col>
                            </FormGroup>
                            <FormGroup
                                controlId="productQuestion"
                                htmlFor="question">
                                <Col componentClass={ControlLabel}>
                                    Treść</Col>
                                <Col>
                                    <FormControl
                                        placeholder="Treść"
                                        componentClass="textarea"
                                        className="newProductQuestion"
                                        name="question"
                                        minLength="10"
                                        maxLength="250"
                                        required />
                                </Col>
                            </FormGroup>
                            <Button className="adminButton" onClick={this.submitForm} type="submit" block>
                                Wyślij
                                </Button>
                        </Form>
                    </Modal.Body>

                </Modal>

            </div>
        );
    },
    submitForm: function (e) {
        var form = $("#productQuestionForm");
        form.validate({
            errorPlacement: function (error, element) {
                error.insertAfter(element);
            }
        });
        if (!form.valid())
            return;
    }
});

function addFullScreenListeners(api) {
    var images = document.getElementById("gallery").getElementsByTagName("img");
    for (var i = 0; i < images.length; i++) {
        images[i].addEventListener("click", function () {
            api.enterFullscreen();
        });
    }
}

