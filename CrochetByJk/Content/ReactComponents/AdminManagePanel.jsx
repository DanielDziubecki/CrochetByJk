const {Table, FormGroup, Col, FormControl, Button} = ReactBootstrap
const ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;

class AdminManagePanel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            categories: this.props.categories,
            loading: false,
            products: null
        };
        this.getProductsByCategory = this.getProductsByCategory.bind(this);
    }
    getProductsByCategory(event) {
        this.setState({ loading: true })
        // var h1 = document.getElementById("productCategory").disabled = true;
        $.ajax({
            type: 'GET',
            url: '/produkty/pobierzprodukty',
            data: { categoryId: event.target.value },
            success: (data) => {
                this.setState({
                    products: data,
                    loading: false
                })
            }
        });
        // var h1 = document.getElementById("productCategory").disabled = false;
    }
    render() {
        return (
            <div id="adminManagePanel">
                <ReactCSSTransitionGroup transitionName="adminFormAnimation"
                    transitionAppear={true}
                    transitionAppearTimeout={500}>
                    <CategoryDropDown categories={this.state.categories} onChange={this.getProductsByCategory} disable={this.state.loading} />
                    {this.state.loading ? <LoadingComponent /> : <ProductsGrid products={this.state.products} />}
                </ReactCSSTransitionGroup>
            </div>)
    }
};

class CategoryDropDown extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        return (
            <ReactCSSTransitionGroup transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <FormGroup controlId="productCategory">
                    <Col componentClass={ControlLabel}>
                        Kategoria produktu</Col>
                    <Col>
                        <FormControl
                            componentClass="select"
                            placeholder="select"
                            className="productCategories"
                            onChange={this.props.onChange}
                            defaultValue={-1}
                            style={{ width: '265px', height: '44px' }}
                            disabled={this.props.disable}>

                            <option disabled value={-1}>{"Wybierz kategorię"}</option>
                            {this.props.categories.map((category, index) => {
                                return <option value={category.IdCategory}>{category.Name}</option>
                            })}
                        </FormControl>
                    </Col>
                </FormGroup>
            </ReactCSSTransitionGroup>)
    }
}

class ProductsGrid extends React.Component {
    constructor(props) {
        super(props)
    }
    render() {
        return (<div>
            {this.props.products !== null ?
                <ReactCSSTransitionGroup transitionName="adminFormAnimation"
                    transitionAppear={true}
                    transitionAppearTimeout={500}>
                    <div id="productsAdminTableContainer">
                        <Table bordered hover id="productsAdminTable">
                            <thead>
                                <tr>
                                    <th style={{ display: 'none' }}>Id</th>
                                    <th>Zdjecie</th>
                                    <th>Nazwa</th>
                                    <th>Data dodania</th>
                                    <th>Zobacz</th>
                                    <th>Edytuj</th>
                                    <th>Usuń</th>
                                </tr>
                            </thead>
                            <tbody>
                                {JSON.parse(this.props.products).map((product, index) => {
                                    return (<tr>
                                        <td style={{ display: 'none' }}>
                                            {product.IdProduct}
                                        </td>
                                        <td>
                                            <img src={product.PictureUri} style={{ width: '200px', height: 'auto' }} />
                                        </td>
                                        <td>{product.Name}</td>
                                        <td>{this.formatDate(product.InsertDate)}</td>
                                        <td><a href={product.ProductUrl} ><Button className="adminButton">Zobacz</Button></a></td>
                                        <td>{'Edit'}</td>
                                        <td>{'Delete'}</td>
                                    </tr>)
                                })}
                            </tbody>
                        </Table>
                    </div>
                </ReactCSSTransitionGroup>
                : <FormGroup>
                    <Col>
                        <span style={{ fontSize: '30px' }}>{'Brak produktów w tej kategorii'}</span>
                    </Col>
                </FormGroup>
            }
        </div>
        );
    }
    componentDidMount() {
        var table = $('#productsAdminTable')
        table.DataTable({
            "language": {
                "lengthMenu": "Pokaż _MENU_ produktów na stronę",
                "zeroRecords": "Nie znaleziono",
                "info": "Strona _PAGE_ z _PAGES_",
                "infoEmpty": "Brak danych",
                "infoFiltered": "(Przefiltrowano z _MAX_ wszystkich rekordów)",
                "decimal": "",
                "emptyTable": "Brak danych",
                "infoPostFix": "",
                "thousands": ",",
                "loadingRecords": "Ładuję...",
                "processing": "Przetwarzam...",
                "search": "Szukaj:  ",
                "paginate": {
                    "first": "Pierwsza",
                    "last": "Ostatnia",
                    "next": "Następna",
                    "previous": "Poprzednia"
                },
                "aria": {
                    "sortAscending": ": Sortuj rosnąco",
                    "sortDescending": ": Sortuj malejąco"
                }
            }
        });
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
            <ReactCSSTransitionGroup transitionName="adminFormAnimation"
                transitionAppear={true}
                transitionAppearTimeout={500}>
                <div className="loadingContainer">
                    <div className="loadingComponent">
                    </div>
                    <div><br />{'Pobieram produkty..'}</div>
                </div>
            </ReactCSSTransitionGroup>
        );
    }
}

