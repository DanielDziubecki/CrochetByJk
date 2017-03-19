const {Navbar, Nav, MenuItem, FormGroup, FormControl, Button, ButtonGroup, Glyphicon} = ReactBootstrap;

var MyNavbar = React.createClass({
    render: function () {
        return (<Navbar>
            <Navbar.Header>
                <Navbar.Brand>
                </Navbar.Brand>
                <Navbar.Toggle />
            </Navbar.Header>
            <Navbar.Collapse>
                <Nav>
                    <MenuItem href="/Produkty/Sukienki/" id="dresses">SUKIENKI</MenuItem>
                    <MenuItem href="/Produkty/Swetry/" id="sweaters">SWETRY</MenuItem>
                    <MenuItem href="/Produkty/Torebki/" id="bags">TOREBKI</MenuItem>
                    <MenuItem href="/Produkty/DlaDzieci/" id="forchildren">DLA DZIECI</MenuItem>
                    <MenuItem href="/Produkty/Dekoracje/" id="decor" >DEKORACJE</MenuItem>
                    <MenuItem href="/Kontakt/" id="contact">KONTAKT</MenuItem>
                </Nav>
            </Navbar.Collapse>
        </Navbar>);
    }
});

ReactDOM.render(<MyNavbar />, document.getElementById('navbar'));


