const {Navbar, Nav, MenuItem, Glyphicon} = ReactBootstrap;

var Footer = React.createClass({
  render: function () {
    return (
      <Navbar>
        <Nav>
          <div className="footer-item">
            Creation by Daniel Dziubecki © 2016. All Rights Reserved
          </div>
        </Nav>
        <Navbar.Text pullRight>
          <a href="https://www.facebook.com/Crochet-by-Joanna-Kuczynska-300469270100790">
            <i
              className="fa fa-facebook-square"
              style={{
              fontSize: '30px',
              color: 'royalblue'
            }}></i>
          </a>
        </Navbar.Text>
      </Navbar>
    );
  }
});

ReactDOM.render(
  <Footer/>, document.getElementById('footer'));
