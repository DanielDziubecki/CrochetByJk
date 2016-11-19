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
              <MenuItem>Sukienki</MenuItem>
              <MenuItem>Dla dzieci</MenuItem>
              <MenuItem>Swetry</MenuItem>
              <MenuItem>Torebki</MenuItem>
              <MenuItem>Dekoracje</MenuItem>
              <MenuItem>O mnie</MenuItem>
          </Nav>
        </Navbar.Collapse>
        </Navbar>);
    }
});
ReactDOM.render(<MyNavbar />,document.getElementById('navbar'));


