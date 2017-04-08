const {Form, FormGroup, ControlLabel, HelpBlock, Checkbox, Radio, Button, Col} = ReactBootstrap;

var AdminForm = React.createClass({
  render: function () {
    return (
      <Form horizontal className="adminForm" method="POST">
        <FormGroup controlId="formHorizontalEmail">
          <Col componentClass={ControlLabel} sm={2}>
            Email
      </Col>
          <Col sm={10}>
            <FormControl placeholder="Email"
              data-val="true"
              data-val-email="Podaj poprawny adres email"
              data-val-required="Adres email jest wymagany"
              name="Email" />
          </Col>
        </FormGroup>

        <FormGroup controlId="formHorizontalPassword">
          <Col componentClass={ControlLabel} sm={2}>
            Hasło
      </Col>
          <Col sm={10}>
            <FormControl type="password" placeholder="Hasło"
              data-val="true"
              data-val-password="Podaj poprawne hasło"
              data-val-required="Podaj hasło"
              name="Password" />
          </Col>
        </FormGroup>

        <FormGroup>
          <Col smOffset={2} sm={10}>
            <Checkbox>Zapamiętaj mnie</Checkbox>
          </Col>
        </FormGroup>

        <FormGroup>
          <Col smOffset={2} sm={10}>
            <Button className="adminButton" type="submit">
              Zaloguj
        </Button>
          </Col>
        </FormGroup>
      </Form>
    );
  }
});
ReactDOM.render(<AdminForm />, document.getElementById('adminForm'));