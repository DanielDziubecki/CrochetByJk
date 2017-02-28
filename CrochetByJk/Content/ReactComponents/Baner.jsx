var Baner = React.createClass({
    render: function () {
        return (
            <div>
                <img src="/Content/Img/v3.jpg" className="imageBaner" />
            </div>
        );
    }
});
ReactDOM.render(<Baner />, document.getElementById('baner'));