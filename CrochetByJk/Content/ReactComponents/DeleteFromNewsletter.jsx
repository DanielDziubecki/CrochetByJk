const {Button} = ReactBootstrap;

var DeleteFromNewsletter = React.createClass({
    getInitialState: function () {
        return {clientId: this.props.clientId};
    },
    render() {
        return (
            <div
                id="adminButtonGroup"
                style={{
                margin: "10%"
            }}>
                <Button
                    id="confirmDelete"
                    className="adminButton"
                    onClick={this.confirm}
                    type="submit">
                    Usuń mnie z newslettera
                </Button>
                <div id="questionSend" className="aboutMe">
                    <div id="questionSendButton">
                        Twój adres email nie znajduję się już w naszej bazie.
                    </div>
                </div>
            </div>
        )
    },
    confirm: function (e) {
        var clientId = this.props.clientId;
        $.ajax({
            type: 'POST',
            url: '/newsletter/usun/',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({clientId: clientId}),
            success: function (result) {},
            error: function (jqXHR, exception) {
                console.log(jqXHR);

            }
        });
        $('#confirmDelete').append('&nbsp &nbsp<i class="fa fa-refresh fa-spin"></i>');
        setTimeout(x => {
            $("#confirmDelete").css('display', 'none');
            $("#questionSend").fadeTo("slow", 1);
        }, 2000)
    }
})