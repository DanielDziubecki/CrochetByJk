const {Navbar, Nav, MenuItem, Glyphicon} = ReactBootstrap;

var Footer = React.createClass({
  render: function () {
    return (
      <Navbar id="footer-container">
        <Nav>
          <div id="footer-item">
            Creation by Daniel Dziubecki © 2016. All Rights Reserved
          </div>
        </Nav>
      </Navbar>
    );
  }
});

ReactDOM.render(<Footer/>, document.getElementById('footer'));
