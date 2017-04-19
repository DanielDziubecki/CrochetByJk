class ProductForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            overridePictures: this.props.productToEdit === undefined
        }
    }

    render() {
        return (
            <Form method="POST" id="productForm">
                <div id="validationMsg"></div>
                <fieldset disabled={this.props.formDisabled}>
                    {this.props.productToEdit !== undefined
                        ? <div
                                id="productToEditId"
                                style={{
                                display: 'none'
                            }}>{this.props.productToEdit.IdProduct}
                            </div>
                        : null}

                    <FormGroup controlId="newProductName">
                        <ControlLabel>
                            Nazwa produktu
                        </ControlLabel>
                        <FormControl
                            placeholder="Nazwa"
                            name="Name"
                            type="text"
                            minLength="5"
                            defaultValue={this.props.productToEdit !== undefined
                            ? this.props.productToEdit.Name
                            : ""}
                            required/>
                    </FormGroup>
                    <FormGroup controlId="productCategory">
                        <ControlLabel>
                            Kategoria produktu</ControlLabel>
                        <FormControl
                            componentClass="select"
                            placeholder="select"
                            className="productCategories"
                            defaultValue={this.props.productToEdit !== undefined
                            ? this.props.productToEdit.IdCategory
                            : ""}>
                            {this
                                .props
                                .categories
                                .map((category, index) => {
                                    return <option value={category.IdCategory}>{category.Name}</option>
                                })}
                        </FormControl>
                    </FormGroup>

                    {this.props.productToEdit !== undefined
                        ? <FormGroup controlId="overridePictures">
                                <Checkbox
                                    id="overridePicturesCheck"
                                    onChange={() => {
                                    this.setState({
                                        overridePictures: !this.state.overridePictures
                                    })
                                }}
                                    inline>
                                    <ControlLabel>
                                        Chcę nadpisać wszystkie zdjęcia produktu
                                    </ControlLabel>
                                </Checkbox>
                            </FormGroup>
                        : null}

                    <FormGroup controlId="productDescription" htmlFor="Description">
                        <ControlLabel>
                            Opis produktu
                        </ControlLabel>
                        <FormControl
                            placeholder="Opis"
                            componentClass="textarea"
                            className="newProductDescription"
                            name="Description"
                            minLength="10"
                            maxLength="250"
                            required
                            defaultValue={this.props.productToEdit !== undefined
                            ? this.props.productToEdit.Description
                            : ""}/>
                    </FormGroup>
                    {this.state.overridePictures
                        ? <FileInputs/>
                        : null}
                </fieldset>
                <Button
                    id="newProduct"
                    className="adminButton"
                    onClick={this.props.submitForm}
                    type="submit"
                    block>
                    {this.props.productToEdit !== undefined
                        ? 'Zapisz zmiany'
                        : 'Dodaj nowy produkt'}

                </Button>
            </Form>
        )
    }
};

class FileInputs extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        var $gallery = $('#gallery-images');
        var $mainImage = $('#main-image');
        if ($gallery.length) 
            $gallery.fileinput({
                showUpload: false,
                allowedFileExtensions: [
                    'jpg', 'png'
                ],
                maxFileSize: 1000
            });
        
        if ($mainImage.length) 
            $mainImage.fileinput({
                showUpload: false,
                allowedFileExtensions: [
                    'jpg', 'png'
                ],
                maxFileSize: 1000
            });
        }
    
    render() {
        return (
            <FormGroup>
                <FormGroup controlId="productImages" htmlFor="galleryImages">
                    <ControlLabel>
                        Wybierz zdjęcia do galerii</ControlLabel>
                    <input
                        id="gallery-images"
                        type="file"
                        className="file"
                        name="galleryImages"
                        multiple
                        data-show-upload="false"
                        data-show-caption="true"
                        accept=".png, .jpg, .jpeg"
                        required/>
                    <div id="galleryImagesError"></div>
                </FormGroup>

                <FormGroup controlId="productMainImage" htmlFor="mainImage">
                    <ControlLabel>
                        Wybierz zdjęcie główne</ControlLabel>
                    <input
                        id="main-image"
                        type="file"
                        className="file"
                        name="mainImage"
                        accept=".png, .jpg, .jpeg"
                        required/>
                    <div id="mainImageError"></div>
                </FormGroup>
            </FormGroup>
        );
    }
}
