const {Form, Grid, Row, Col, FormGroup, FormControl, ControlLabel, HelpBlock, Checkbox, Radio, Button} = ReactBootstrap;
const ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;

var AdminNavigationPanel = React.createClass({
    render: function () {
        return (
            <div id="adminButtonGroup">
                <Button className="adminButton" onClick={addNewProductForm} style={{margin: "4%"}}>Dodaj produkt</Button>
                <Button className="adminButton" style={{margin: "4%"}}>Zarządzaj produktami</Button>
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
            <ReactCSSTransitionGroup transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <div id="validationMsg"></div>
                <fieldset disabled={this.state.formDisabled}>
                    <Form horizontal className="addNewProductForm" method="POST" id="productForm">
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
                                            {this.state.data.map((category,index) =>{
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
            $gallery.fileinput({
               allowedFileExtensions : ['jpg', 'png']
            });

        if ($mainImage.length)
            $mainImage.fileinput({
                showUpload: false,
                allowedFileExtensions : ['jpg', 'png']
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
        $('.btnAddNewProduct').text("");
        $('.btnAddNewProduct').append('Dodajemy produkt..&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');
        setTimeout(()=>{},2000);
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
            url: '/addnewproduct',
            type: 'post',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result.Success == "False") {
                    $('#validationMsg').text(result.responseText);
                    return;
                }
                ReactDOM.unmountComponentAtNode(document.getElementById('addNewProductForm'));
                newProductAdded(result.Url);
            },
            error: function(data){
                console.log(data);
            }
        });
        $('.btnAddNewProduct').text("Dodaj nowy produkt");
        self.setState({ formDisabled: false });
    }
});

var GoToNewProduct = ({url}) => (
    <div className="goToNewProductContainer">
        <ReactCSSTransitionGroup transitionName="adminFormAnimation"
            transitionAppear={true}
            transitionAppearTimeout={500}>
            <Button className="btnGoToNewProduct" href={url}>
                Przejdź do produktu
          </Button>
        </ReactCSSTransitionGroup>
    </div>
);

function addNewProductForm() {
    ReactDOM.unmountComponentAtNode(document.getElementById('goToNewProduct'));
    ReactDOM.render(<AddNewProduct />, document.getElementById('addNewProductForm'));
}
function newProductAdded(url) {
    ReactDOM.render(<GoToNewProduct url={url} />, document.getElementById('goToNewProduct'));
}
ReactDOM.render(<AdminNavigationPanel />, document.getElementById('adminNavigationPanel'));
