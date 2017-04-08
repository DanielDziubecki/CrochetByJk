const {Form, Grid, Row, Col, FormGroup, FormControl, ControlLabel, HelpBlock, Checkbox, Radio, Button} = ReactBootstrap;
const ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
const AdminManagePanel = AdminManagePanel;

var AdminNavigationPanel = React.createClass({

    getInitialState: function () {
        return {
            categories: [],
            showNewProductForm: false,
            showManagePanel: false,
        }
    },
    newProductButtonClick: function () {
        this.setState({
            showNewProductForm: !this.state.showNewProductForm,
            showManagePanel: this.state.showNewProductForm
        })
    },
    managePanelButtonClick: function () {
        this.setState({
            showManagePanel: !this.state.showManagePanel,
            showNewProductForm: this.state.showManagePanel
        })
    },
    render: function () {
        return (
            <div id="adminButtonGroup">
                <Button id="addProductBtn" className="adminButton" onClick={this.newProductButtonClick} style={{ margin: "2%" }}>Dodaj produkt</Button>
                <Button id="managePanelBtn"  className="adminButton" onClick={this.managePanelButtonClick} style={{ margin: "2%" }}>Zarządzaj produktami</Button>
                {this.state.showNewProductForm ? <AddNewProduct categories={this.state.categories} /> : null}
                {this.state.showManagePanel ?<AdminManagePanel categories={this.state.categories}/> : null}
            </div>
        );
    },
    componentWillMount: function () { 
        $.ajax({
            url: "/categories",
            success: (data) => {
                this.setState({
                    categories: data
                })
            }
        });
    }

});

var AddNewProduct = React.createClass({
    getInitialState: function () {
        return {
            categories: this.props.categories,
            formDisabled: false,
            showGoToProductButton: false,
            productUrl: null
        }
    },
    render: function () {
        return (
            this.state.showGoToProductButton ?
                <GoToNewProduct url={this.state.productUrl} /> :
                 <div id="addNewProductForm">
                <ReactCSSTransitionGroup transitionName="adminFormAnimation"
                    transitionAppear={true}
                    transitionAppearTimeout={500}>
                   
                    <div id="validationMsg"></div>
                    <fieldset disabled={this.state.formDisabled}>
                        <Form horizontal method="POST" id="productForm">
                            {/*Section one*/}
                            <Col md={4}>
                                <div className="newProductFormSection">
                                    <FormGroup
                                        controlId="newProductName"
                                        htmlFor="Name">
                                        <Col componentClass={ControlLabel}>
                                            Nazwa produktu
                                   </Col>
                                        <Col>
                                            <FormControl
                                                placeholder="Nazwa"
                                                name="Name"
                                                type="text"
                                                minLength="5"
                                                required />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup controlId="productCategory">
                                        <Col componentClass={ControlLabel}>
                                            Kategoria produktu</Col>
                                        <Col>
                                            <FormControl
                                                componentClass="select"
                                                placeholder="select"
                                                className="productCategories">
                                                {this.state.categories.map((category, index) => {
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
                                    <FormGroup
                                        controlId="productDescription"
                                        htmlFor="Description">
                                        <Col componentClass={ControlLabel}>
                                            Opis produktu</Col>
                                        <Col>
                                            <FormControl
                                                placeholder="Opis"
                                                componentClass="textarea"
                                                className="newProductDescription"
                                                name="Description"
                                                minLength="10"
                                                maxLength="250"
                                                required />
                                        </Col>
                                    </FormGroup>
                                </div>
                            </Col>
                            <Col>
                                {/*Section three*/}
                                <Col md={12}>
                                    <div className="newProductFormSection">
                                        <FormGroup
                                            controlId="productImages"
                                            htmlFor="galleryImages">
                                            <Col componentClass={ControlLabel}>
                                                Wybierz zdjęcia do galerii</Col>
                                            <Col>
                                                <input
                                                    id="gallery-images"
                                                    type="file"
                                                    className="file"
                                                    name="galleryImages"
                                                    multiple data-show-upload="false"
                                                    data-show-caption="true"
                                                    accept=".png, .jpg, .jpeg"
                                                    required />
                                            </Col>
                                            <div id="galleryImagesError"></div>
                                        </FormGroup>

                                        <FormGroup
                                            controlId="productMainImage"
                                            htmlFor="mainImage">
                                            <Col componentClass={ControlLabel}>
                                                Wybierz zdjęcie główne</Col>
                                            <Col>
                                                <input
                                                    id="main-image"
                                                    type="file"
                                                    className="file"
                                                    name="mainImage"
                                                    accept=".png, .jpg, .jpeg"
                                                    required />
                                            </Col>
                                            <div id="mainImageError"></div>
                                        </FormGroup>
                                    </div>
                                </Col>

                                <div className="newProductFormSection">
                                    <Button id="newProduct" className="adminButton" onClick={this.submitForm} type="submit" block>
                                        Dodaj nowy produkt
                                </Button>
                                </div>
                            </Col>
                        </Form>
                    </fieldset >
                   
                </ReactCSSTransitionGroup > </div>
        );
    },
    componentDidMount: function () {
        var $gallery = $('#gallery-images');
        var $mainImage = $('#main-image');
        if ($gallery.length)
            $gallery.fileinput({
                showUpload: false,
                allowedFileExtensions: ['jpg', 'png'],
                maxFileSize: 1000
            });

        if ($mainImage.length)
            $mainImage.fileinput({
                showUpload: false,
                allowedFileExtensions: ['jpg', 'png'],
                maxFileSize: 1000
            });
    },
    submitForm: function (e) {
        var form = $("#productForm");
        form.validate({
            errorPlacement: function (error, element) {
                if (element.attr("name") == "mainImage") {
                    error.insertAfter("#mainImageError");
                }
                else if (element.attr("name") == "galleryImages") {
                    error.insertAfter("#galleryImagesError");
                }
                else {
                    error.insertAfter(element);
                }
            }
        });
        if (!form.valid())
            return;
        var self = this;
        e.preventDefault();
        $('#newProduct').text("");
        $('#newProduct').append('Dodajemy produkt..&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');
        self.setState({
            formDisabled: true
        })
        var data = new FormData();
        data.append("Name", $("#newProductName").val())
        data.append("Description", $("#productDescription").val())
        data.append("GalleryUri", $("#productGalleryImages").val())

        jQuery.each(jQuery('#gallery-images')[0].files, function (i, file) {
            data.append('gallery-image-' + i, file);
        });

        data.append('MainPhoto', $('#main-image')[0].files[0]);
        data.append('IdCategory', $("#productCategory option:selected").val());
        data.append('CategoryName', $("#productCategory option:selected").text());

        $.ajax({
            url: 'produkty/dodajnowy',
            type: 'post',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.Success == "False") {
                    $('#validationMsg').text(result.responseText);
                    $('#newProduct').text("Dodaj nowy produkt");
                    return;
                }
                self.setState({
                    productUrl: result.Url,
                    showGoToProductButton: true
                })

            }
        });
        self.setState({ formDisabled: false });
    }
});

var GoToNewProduct = ({url}) => (
    <div id="goToNewProduct">
        <ReactCSSTransitionGroup transitionName="adminFormAnimation"
            transitionAppear={true}
            transitionAppearTimeout={500}>
            <Button className="adminButton" href={url}>
                Przejdź do produktu
          </Button>
        </ReactCSSTransitionGroup>
     </div>
);



ReactDOM.render(<AdminNavigationPanel />, document.getElementById('adminNavigationPanel'));
