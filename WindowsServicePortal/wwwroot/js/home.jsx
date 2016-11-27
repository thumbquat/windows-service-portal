var data = [
  { MachineName: "Daniel Lo Nigro"},
  { MachineName: "Pete Hunt" },
  { MachineName: "Jordan Walke" }
];

var Machine = React.createClass({
    render: function () {
        return (
          <div className="machine">
            <h2 className="machineName">
              {this.props.name}
            </h2>
        {this.props.children}
        </div>
      );
    }
});

var MachineList = React.createClass({
    render: function() {
        return (
          <div className="machineList">
              <Service className="s">test *service*</Service>
          </div>
      );
    }
});

var MachineViewer = React.createClass({
    render: function() {
        return (
    <div className="machineViewer">
            <h1>Machines</h1>
            <MachineList data-animation={this.props.data}/>
          </div>
        );
    }
});
ReactDOM.render(
  <MachineList data={data} />,
  document.getElementById('content')
);