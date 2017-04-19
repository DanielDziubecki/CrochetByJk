const {
    Form,
    Grid,
    Row,
    Col,
    FormGroup,
    FormControl,
    ControlLabel,
    HelpBlock,
    Checkbox,
    Radio,
    Button,
    Modal,
    Table
} = ReactBootstrap;
const ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
const AdminManagePanel = AdminManagePanel;
const AddNewProduct = AddNewProduct;
const ProductForm = ProductForm

var AdminNavigationPanel = React.createClass({
    getInitialState: function () {
        return {categories: [], showNewProductForm: false, showManagePanel: false}
    },
    componentWillMount: function () {
        $.ajax({
            url: "/categories",
            success: (data) => {
                this.setState({categories: data})
            }
        });
    },
    newProductButtonClick: function () {
        if (this.state.showNewProductForm) {
            this.setState({showNewProductForm: false})
        } else {
            this.setState({showNewProductForm: true, showManagePanel: false})
        }
    },
    managePanelButtonClick: function () {
        if (this.state.showManagePanel) {
            this.setState({showManagePanel: false})
        } else {
            this.setState({showManagePanel: true, showNewProductForm: false})
        }
    },
    render: function () {
        return (
            <div id="adminButtonGroup">
                <Button
                    id="addProductBtn"
                    className="adminButton"
                    onClick={this.newProductButtonClick}
                    style={{
                    margin: "2%"
                }}>Dodaj produkt</Button>
                <Button
                    id="managePanelBtn"
                    className="adminButton"
                    onClick={this.managePanelButtonClick}
                    style={{
                    margin: "2%"
                }}>Zarządzaj produktami</Button>
                {this.state.showNewProductForm
                    ? <AddNewProduct categories={this.state.categories}/>
                    : null}
                {this.state.showManagePanel
                    ? <AdminManagePanel categories={this.state.categories}/>
                    : null}
            </div>
        );
    }
});

ReactDOM.render(
    <AdminNavigationPanel/>, document.getElementById('adminNavigationPanel'));
