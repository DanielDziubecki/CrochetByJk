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

                                <FormGroup controlId="productPrice">
                                    <Col componentClass={ControlLabel}>
                                        Cena produktu</Col>
                                    <Col>
                                        <FormControl
                                            placeholder="Cena"
                                            data-val="true"
                                            data-val-number="Cena musi być liczbą"
                                            data-val-required="Cena jest wymagana"
                                            type="number"
                                            name="Price" />
                                    </Col>
                                </FormGroup>
                            </div>
                        </Col>
                        {/*Section two*/}
                        <Col md={4}>
                            <div className="newProductFormSection">
                                <FormGroup controlId="productWorkTime">
                                    <Col componentClass={ControlLabel}>
                                        Szacowany czas wykonania</Col>
                                    <Col>
                                        <FormControl
                                            placeholder="Czas"
                                            data-val="true"
                                            data-val-required="Czas jest wymagany"
                                            name="Name"
                                            type="number"
                                            />
                                    </Col>
                                </FormGroup>

                                <FormGroup controlId="productImages">
                                    <Col componentClass={ControlLabel}>
                                        Wybierz zdjęcia do galerii</Col>
                                    <Col>
                                        <span className="btn btn-default btn-block btn-file">
                                            Wybierz<input type="file" multiple id="productGalleryImages" />
                                        </span>
                                    </Col>
                                </FormGroup>

                                <FormGroup controlId="productMainImage">
                                    <Col componentClass={ControlLabel}>
                                        Wybierz zdjęcie główne</Col>
                                    <Col>
                                        <span className="btn btn-default btn-block btn-file">
                                            Wybierz<input type="file" id="mainImage" />
                                        </span>
                                    </Col>
                                </FormGroup>
                            </div>
                        </Col>

                        {/*Section three*/}
                        <Col md={4}>
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
                            <div className="newProductFormSection">
                                <Button className="btnAddNewProduct" onClick={this.submitForm} type="submit" block>
                                    Dodaj nowy produkt
                                </Button>
                            </div>
                        </Col>
                    </Form>
                </fieldset>
            </ReactCSSTransitionGroup>
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
        data.append("Price", $("#productPrice").val())
        data.append("WorkTime", $("#productWorkTime").val())
        data.append("GalleryUri", $("#productGalleryImages").val())
        jQuery.each(jQuery('#productGalleryImages')[0].files, function (i, file) {
            data.append('gallery-image-' + i, file);
        });
        data.append('MainPhoto', $('#mainImage')[0].files[0]);
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