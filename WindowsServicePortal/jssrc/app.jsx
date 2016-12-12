var statusUrlRoot = "/api/windowsservice/status/";
var machineListUrl = "/api/windowsservice/machinenames";

import { createStore } from "redux";

let store = createStore();

var App = React.createClass({
    getInitialState: function () {
        return { currentMachineName: "Loading" };
    },
    render: function () {
        return (
            <div>
                <MachineList url={machineListUrl} />
                <ServiceList machineName={this.state.currentMachineName} pollInterval={3000} />
            </div>
            );
    }
});

var MachineList = React.createClass({
    getInitialState: function () {
        return { data: [], currentMachineName: "Loading" };
    },
    componentWillMount: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data, currentMachineName: data[0].name });
        }.bind(this);
        xhr.send();
    },
    render: function () {
        var machineNodes = this.state.data.map(function (machine) {
            return (<Machine name={machine.name} />)
        })
        return (
            <div className="machineList">
                {machineNodes}
            </div>
            )
    }
});

var Machine = React.createClass({
    render: function () {
        return (
            <div className="machine">
                {this.props.name}
            </div>
	    );
    }
});

var ServiceList = React.createClass({
    loadServiceStatusFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', statusUrlRoot + this.props.machineName, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentWillMount: function () {
        this.loadServiceStatusFromServer();
        window.setInterval(this.loadServiceStatusFromServer, this.props.pollInterval)
    },
    render: function () {
        var serviceNodes = this.state.data.map(function (service) {
            return (
                <Service name={service.name} status={service.status} />
                );
        })
        return (
    <table className="serviceList">
        <tbody>
            <tr><th>Service</th>
            <th>Status</th></tr>
            {serviceNodes}
        </tbody>
    </table>)
        ;
    }
});

var Service = React.createClass({
    render: function () {
        return (
              <tr className="serviceRow">
                <td className="serviceRowName">{this.props.name}</td>
                <td className="serviceRowStatus">{this.props.status}</td>
              </tr>
      );
    }
});

ReactDOM.render(
  <App />,
  document.getElementById('content')
);
