
var AdminNavigationPanel = React.createClass({
    render: function () {
        return (
            <div>
                <h2>Admin Panel</h2>
                <button type="button">Click Me!</button>
                <button type="button">Click Me!</button>
                <button type="button">Click Me!</button>
            </div>);
    }
});

ReactDOM.render(<AdminNavigationPanel />, document.getElementById('adminNavigationPanel'));