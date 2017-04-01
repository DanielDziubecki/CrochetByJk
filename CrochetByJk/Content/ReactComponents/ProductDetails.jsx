const {Checkbox, Form, Modal, Col, FormGroup, FormControl, ControlLabel, Button} = ReactBootstrap;

var ProductDetails = React.createClass({
    getInitialState: function () {
        return {
            product: this.props.product,
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
                    <br />
                    <p>{this.state.product.Description}</p>
                    <div id="contactForm">
                        <AskForProduct productName={this.state.product.Name} />
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
            gallery_min_height: 500,
            gallery_min_width: 250,
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
        return {
            show: false,
            productName: this.props.productName
        };
    },
    render() {
        let close = () => {
            this.setState({ show: false },
                $("#seeAlsoContainer").css('opacity', 1),
                $("#footer").css('opacity', 1)
            )
        };

        return (
            <div className="modal-container">
                <Button
                    className="adminButton"
                    id="askForProductButton"
                    bsStyle="primary"
                    bsSize="large"
                    onClick={() => this.setState({ show: true }, 
                                   $("#seeAlsoContainer").css('opacity', 0.1),
                                   $("#footer").css('opacity', 0.1))}>
                    Zapytaj o produkt
                    </Button>
                <div id="productQuestionModalContainer">
                    <Modal
                        id="productQuestionModal"
                        show={this.state.show}
                        onHide={close}
                        container={this}
                        aria-labelledby="contained-modal-title"
                    >
                        <Modal.Header closeButton>
                            <Modal.Title id="contained-modal-title"><b>Pytanie o produkt: {this.props.productName}</b></Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form id="productQuestionForm">
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
                                <FormGroup controlId="newsLetter">
                                    <Col>
                                        <Checkbox id="addToNewsletter" defaultChecked inline>
                                            <Col componentClass={ControlLabel}> Chcę otrzymywać powiadomienia o nowych produktach.</Col>
                                        </Checkbox>
                                    </Col>
                                </FormGroup>
                                <Button id="sendQuestion" className="adminButton" onClick={this.submitForm} type="submit" block>
                                    Wyślij
                                </Button>
                            </Form>
                            <div id="questionSend" className="aboutMe">
                                <div id="questionSendButton">
                                    Wysłano pomyślnie.
                                </div>
                            </div>
                        </Modal.Body>
                    </Modal>
                </div>

            </div>
        );
    },
    submitForm: function (e) {
        e.preventDefault();
        var form = $("#productQuestionForm");
        form.validate({
            errorPlacement: function (error, element) {
                error.insertAfter(element);
            }
        });
        if (!form.valid())
            return;

        var emailMessage = JSON.stringify({
            from: $("#emailAdress").val(),
            to: ["kontakt@crochetbyjk.pl"],
            subject: $("#contained-modal-title b").text(),
            body: $("#productQuestion").val(),
            addToNewsletter: $("#addToNewsletter").is(':checked')
        });

        $.ajax({
            url: '/produkty/zadajpytanie',
            type: 'post',
            contentType: 'application/json',
            dataType: "json",
            data: emailMessage,
            success: function (result) {

            },
            error: function (xhr) {

            }
        });
        $('#sendQuestion').append('&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');
        setTimeout(() => {
            $("#productQuestionForm").css('display', 'none');
            $("#contained-modal-title").css('display', 'none');
            $("#questionSend").fadeTo("slow", 1);
        }, 2000)
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
