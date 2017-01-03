const {Form, Grid, Row, Col, FormGroup, FormControl, ControlLabel, HelpBlock, Checkbox, Radio, Button} = ReactBootstrap;
const ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;

var AdminNavigationPanel = React.createClass({
    render: function () {
        return (
            <div id="adminButtonGroup">
                <Button className="adminButton" onClick={addNewProductForm}>Dodaj produkt</Button>
                <Button className="adminButton">Click Me!</Button>
                <Button className="adminButton">Click Me!</Button>
            </div>
        );
    }
});
var AddNewProduct = React.createClass({
    getInitialState: function () {
        return {
            data: [],
            formDisabled: false
        }
    },
    render: function () {
        return (
            <ReactCSSTransitionGroup transitionName="example"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <div className="validationMsg"></div>
                <fieldset disabled={this.state.formDisabled}>
                    <Form horizontal className="addNewProductForm" method="POST">
                        {/*Section one*/}
                        <Col md={4}>
                            <div className="newProductFormSection">
                                <FormGroup controlId="productName">
                                    <Col componentClass={ControlLabel}>
                                        Nazwa produktu</Col>
                                    <Col>
                                        <FormControl placeholder="Nazwa"
                                            data-val="true"
                                            data-val-required="Nazwa jest wymagana"
                                            name="Name" />
                                    </Col>
                                </FormGroup>
                                <FormGroup controlId="productCategory">
                                    <Col componentClass={ControlLabel}>
                                        Kategoria produktu</Col>
                                    <Col>
                                        <FormControl
                                            componentClass="select"
                                            placeholder="select"
                                            className="productCategories"
                                            accept=".png, .jpg, .jpeg">
                                            {this.state.data.map(function (category) {
                                                return <option value={category.IdCategory}>{category.Name}</option>
                                            })}
                                        </FormControl>
                                    </Col>
                                </FormGroup>
                            </div>
                        </Col>

                        {/*Section two*/}
                        <Col md={8}>
                            <div className="newProductFormSection">
                                <FormGroup controlId="productDescription">
                                    <Col componentClass={ControlLabel}>
                                        Opis produktu</Col>
                                    <Col>
                                        <FormControl
                                            placeholder="Opis"
                                            componentClass="textarea"
                                            data-val="true"
                                            data-val-required="Opis jest wymagany"
                                            className="newProductDescription"
                                            name="Description" />
                                    </Col>
                                </FormGroup>
                            </div>
                        </Col>
                        <Col>
                            {/*Section three*/}
                            <Col md={12}>
                                <div className="newProductFormSection">
                                    <FormGroup controlId="productImages">
                                        <Col componentClass={ControlLabel}>
                                            Wybierz zdjęcia do galerii</Col>
                                        <Col>
                                            <input id="gallery-images" type="file"
                                                class="file" multiple data-show-upload="false"
                                                data-show-caption="true" />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup controlId="productMainImage">
                                        <Col componentClass={ControlLabel}>
                                            Wybierz zdjęcie główne</Col>
                                        <Col>
                                            <input id="main-image" type="file" class="file" />
                                        </Col>
                                    </FormGroup>
                                </div>
                            </Col>
                            <div className="newProductFormSection">
                                <Button className="btnAddNewProduct" onClick={this.submitForm} type="submit" block>
                                    Dodaj nowy produkt
                                </Button>
                            </div>
                        </Col>
                    </Form>
                </fieldset >
            </ReactCSSTransitionGroup >
        );
    },
    componentDidMount: function () {
        $.ajax({
            url: "/categories",
            success: (data) => {
                this.setState({
                    data: data
                })
            }
        });
        var $gallery = $('#gallery-images');
        var $mainImage = $('#main-image');
        if ($gallery.length)
            $gallery.fileinput();

        if ($mainImage.length)
            $mainImage.fileinput();
    },
    submitForm: function (e) {
        var self = this;
        e.preventDefault();
        $('.btnAddNewProduct').text("");
        $('.btnAddNewProduct').append('Dodajemy produkt..&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');
        self.setState({
            formDisabled: true
        })
        var data = new FormData();
        data.append("Name", $("#productName").val())
        data.append("Description", $("#productDescription").val())
        data.append("GalleryUri", $("#productGalleryImages").val())
        jQuery.each(jQuery('#gallery-images')[0].files, function (i, file) {
            data.append('gallery-image-' + i, file);
        });
        data.append('MainPhoto', $('#main-image')[0].files[0]);
        data.append('IdCategory', $("#productCategory option:selected").val());

        $.ajax({
            url: '/addnewproduct',
            type: 'post',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                $('.validationMsg').text(result.responseText);
            }
        });
        $('.btnAddNewProduct').text("Dodaj nowy produkt");
        self.setState({ formDisabled: false });
    }
});

function addNewProductForm() {
    ReactDOM.render(<AddNewProduct />, document.getElementById('addNewProductForm'));
}
ReactDOM.render(<AdminNavigationPanel />, document.getElementById('adminNavigationPanel'));
