class AdminManagePanel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            categories: this.props.categories,
            loading: false,
            products: undefined
        };
        this.handleCategoryChange = this
            .handleCategoryChange
            .bind(this);

        this.getProductsByCategory = this
            .getProductsByCategory
            .bind(this);
    }

    getProductsByCategory(value) {
        this.setState({loading: true})
        $.ajax({
            type: 'GET',
            url: '/produkty/pobierzprodukty',
            data: {
                categoryId: value
            },
            success: (data) => {
                this.setState({products: data, loading: false})
                document
                    .getElementById('categorySelect')
                    .value = value;
            },
            error: (msg) => {
                this.setState({products: undefined, loading: false})
                document
                    .getElementById('categorySelect')
                    .value = value;
            }
        });
    }

    handleCategoryChange(event) {
        this.getProductsByCategory(event.target.value);
    }

    render() {
        return (
            <div id="adminManagePanel">
                <ReactCSSTransitionGroup
                    transitionName="adminFormAnimation"
                    transitionAppear={true}
                    transitionAppearTimeout={500}>
                    <CategoryDropDown
                        categories={this.state.categories}
                        onChange={this.handleCategoryChange}
                        disable={this.state.loading}/>
                </ReactCSSTransitionGroup>

                <ReactCSSTransitionGroup
                    transitionName="adminFormAnimation"
                    transitionAppear={true}
                    transitionAppearTimeout={500}>
                    {this.state.loading
                        ? <LoadingComponent/>
                        : <ProductsGrid
                            products={this.state.products}
                            categories={this.state.categories}
                            refresh={this.getProductsByCategory}/>}
                </ReactCSSTransitionGroup>
            </div>
        )
    }
};

class CategoryDropDown extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        return (
            <ReactCSSTransitionGroup
                transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <FormGroup>
                    <Col componentClass={ControlLabel}>
                        Kategoria produktu</Col>
                    <Col>
                        <FormControl
                            id="categorySelect"
                            componentClass="select"
                            placeholder="select"
                            onChange={this.props.onChange}
                            defaultValue={-1}
                            style={{
                            width: '265px',
                            height: '44px'
                        }}
                            disabled={this.props.disable}>
                            <option disabled value={-1}>{"Wybierz kategorię"}</option>
                            {this
                                .props
                                .categories
                                .map((category, index) => {
                                    return <option value={category.IdCategory}>{category.Name}</option>
                                })}
                        </FormControl>
                    </Col>
                </FormGroup>
            </ReactCSSTransitionGroup>
        )
    }
}

class ProductsGrid extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <ReactCSSTransitionGroup
                    transitionName="adminFormAnimation"
                    transitionAppear={true}
                    transitionAppearTimeout={500}>
                    <div id="productsAdminTableContainer">
                        <Table bordered hover id="productsAdminTable">
                            <thead>
                                <tr>
                                    <th data-sortable="false" data-filterable="false" id="adminTableHeader">Id</th>
                                    <th data-sortable="false" data-filterable="false" id="adminTableHeader">Zdjecie</th>
                                    <th id="adminTableHeader">Nazwa</th>
                                    <th data-type="date" id="adminTableHeader">Data dodania</th>
                                    <th
                                        data-sortable="false"
                                        data-filterable="false"
                                        data-type="html"
                                        id="adminTableHeader">Zobacz</th>
                                    <th
                                        data-sortable="false"
                                        data-filterable="false"
                                        data-type="html"
                                        id="adminTableHeader">Edytuj</th>
                                    <th
                                        data-sortable="false"
                                        data-filterable="false"
                                        data-type="html"
                                        id="adminTableHeader">Usuń</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.props.products !== undefined
                                    ? JSON
                                        .parse(this.props.products)
                                        .map((product, index) => {
                                            return < ProductGridRow product = {
                                                product
                                            }
                                            categories = {
                                                this.props.categories
                                            }
                                            refresh = {
                                                this.props.refresh
                                            }
                                            />
                                        })
                                    : [].map((product, index) => { return <ProductGridRow / >
                                    })}
                            </tbody>
                        </Table>
                    </div>
                </ReactCSSTransitionGroup>
            </div>
        );
    }
    componentDidMount() {
        var table = $('#productsAdminTable')
        table.footable({
            "sorting": {
                "enabled": true
            },
            "filtering": {
                "enabled": true
            },
            "paging": {
                "enabled": true,
                "size": 5
            },
            "columns": [
                {
                    "name": "Id",
                    "visible": false
                }
            ]
        });
    }
}

class ProductGridRow extends React.Component
{
    constructor(props) {
        super(props)
        this.state = {
            showEditModal: false,
            showDeleteModal: false
        }
    }
    render()
    {
        return (
            <tr>
                <td>
                    {this.props.product.IdProduct}
                </td>
                <td>
                    <img
                        src={this.props.product.PictureUri}
                        style={{
                        width: '200px',
                        height: 'auto'
                    }}/>
                </td>
                <td>{this.props.product.Name}</td>
                <td>{this.formatDate(this.props.product.InsertDate)}</td>
                <td>
                    <a href={this.props.product.ProductUrl}>
                        <Button className="adminButton">Zobacz</Button>
                    </a>
                </td>
                <td>
                    <ProductFormModal
                        productToEdit={this.props.product}
                        categories={this.props.categories}
                        refresh={this.props.refresh}/>
                </td>
                <td>{'Delete'}</td>
            </tr>

        )
    }
    formatDate(date) {
        return new Date(date).toLocaleString();
    }
}

class LoadingComponent extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        return (
            <ReactCSSTransitionGroup
                transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <div className="loadingContainer">
                    <div className="loadingComponent"></div>
                    <div><br/>{'Pobieram produkty..'}</div>
                </div>
            </ReactCSSTransitionGroup>
        );
    }
}

class ProductFormModal extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            show: false,
            formDisabled: false,
            succcesfullyUpdated: false,
            updatedProductCategory: undefined,
            backdrop: true,
            keyboard: true,
            closeButton: true

        }
        this.submitEditedProduct = this
            .submitEditedProduct
            .bind(this);
    }
    render() {
        let close = () => {
            if (this.state.succcesfullyUpdated && this.state.updatedProductCategory !== undefined) {
                this
                    .props
                    .refresh(this.state.updatedProductCategory)
            }
            this.setState({show: false})
        };
        return (
            <div>
                <Button
                    className="adminButton"
                    style={{
                    backgroundColor: '#32A432'
                }}
                    onClick={() => this.setState({show: true, succcesfullyUpdated: false})}>Edytuj
                </Button>
                <Modal
                    show={this.state.show}
                    onHide={close}
                    container={this}
                    backdrop={this.state.backdrop}
                    keyboard={this.state.keyboard}
                    aria-labelledby="contained-modal-title">
                    <Modal.Header closeButton={this.state.closeButton}>
                        <Modal.Title id="contained-modal-title">
                            <b>Edytuj produkt
                            </b>
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body >
                        {this.state.succcesfullyUpdated
                            ? <ProductUpdatedMessage/>
                            : <ProductForm
                                productToEdit={this.props.productToEdit}
                                categories={this.props.categories}
                                submitForm={this.submitEditedProduct}
                                formDisabled={this.state.formDisabled}/>}
                    </Modal.Body>
                </Modal>
            </div>
        );
    }
    submitEditedProduct(e) {
        var form = $("#productForm");
        form.validate({
            errorPlacement: function (error, element) {
                if (element.attr("name") == "mainImage") {
                    error.insertAfter("#mainImageError");
                } else if (element.attr("name") == "galleryImages") {
                    error.insertAfter("#galleryImagesError");
                } else {
                    error.insertAfter(element);
                }
            }
        });

        if (!form.valid()) 
            return;
        e.preventDefault();
        this.setState({formDisabled: true, backdrop: 'static', keyboard: false, closeButton: false})

        $('#newProduct').text("");
        $('#newProduct').append('Aktualizujemy produkt..&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');

        var data = new FormData();
        data.append("ProductId", $("#productToEditId").text())
        data.append("Name", $("#newProductName").val())
        data.append("Description", $("#productDescription").val())

        var mainImage = $('#main-image')[0];

        if (mainImage !== undefined) {
            data.append('OverridePictures', true)
            data.append('MainPhoto', mainImage.files[0]);
            jQuery.each(jQuery('#gallery-images')[0].files, function (i, file) {
                data.append('gallery-image-' + i, file);
            });
        }

        var categoryId = $("#productCategory option:selected").val();
        data.append('IdCategory', categoryId);
        data.append('CategoryName', $("#productCategory option:selected").text());

        $.ajax({
            url: 'produkty/zaaktualizuj',
            type: 'post',
            contentType: false,
            processData: false,
            data: data,
            success: (result) => {
                if (result.Success == "False") {
                    $('#validationMsg').text(result.responseText);
                } else {
                    this.setState({
                        formDisabled: false,
                        backdrop: true,
                        keyboard: true,
                        closeButton: true,
                        succcesfullyUpdated: true,
                        updatedProductCategory: categoryId
                    })
                }
            }
        });
    }
}

class ProductUpdatedMessage extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        return (
            <ReactCSSTransitionGroup
                transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <div >
                    <div className="aboutMe">
                        <div id="questionSendButton">
                            Pomyślnie zaaktualizowano produkt.
                        </div>
                    </div>
                </div>
            </ReactCSSTransitionGroup>
        );
    }
}