const {Navbar, Nav, MenuItem,FormGroup,FormControl,Button,ButtonGroup,Glyphicon} = ReactBootstrap;

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
              <MenuItem href="/Produkty/Sukienki/">SUKIENKI</MenuItem>
              <MenuItem>SWETRY</MenuItem>
              <MenuItem>TOREBKI</MenuItem>
              <MenuItem>DLA DZIECI</MenuItem>
              <MenuItem>DEKORACJE</MenuItem>
              <MenuItem>O MNIE</MenuItem>
          </Nav>
        </Navbar.Collapse>
        </Navbar>);
    }
});

ReactDOM.render(<MyNavbar />,document.getElementById('navbar'));


