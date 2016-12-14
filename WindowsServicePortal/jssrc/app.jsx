import React from "react";
import ReactDOM from "react-dom";
import { createStore } from "redux";

var statusUrlRoot = "/api/windowsservice/status/";
var machineListUrl = "/api/windowsservice/machinenames";
var actionUrlRoot = "/api/windowsservice/action/";


//let store = createStore();

var App = React.createClass({
    getInitialState: function () {
        return { currentMachineName: "Pending" };
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
        return { data: [], currentMachineName: "Pending" };
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
            return (<Machine key= {machine.name} name={machine.name} />)
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
                <Service key={service.name} name={service.name} status={service.status} machineName={service.machineName} />
                );
})
return (
<table className="serviceList">
<tbody>
    <tr>
    <th>Service</th>
    <th>Status</th>
    </tr>
{serviceNodes}
</tbody>
</table>)
;
}
});

var Service = React.createClass({
    actionRequest: function (action) {
        var xhr = new XMLHttpRequest();
        xhr.open("get", actionUrlRoot + action + "/" + this.props.machineName + "/" + this.props.name, true);
        xhr.send();
    },
    start: function () {
        this.actionRequest("start")
    },
    stop: function () {
        this.actionRequest("stop")
    },
    restart: function () {
        this.actionRequest("restart")
    },
    render: function () {
        return (
            <tr className="serviceRow">
    <td className="serviceRowName">{this.props.name}</td>
    <td className="serviceRowStatus">{this.props.status}</td>
    <td className="startButton"><button onClick={this.start}>Start</button></td>
    <td className="stopButton"><button onClick={this.stop}>Stop</button></td>
    <td className="reStartButton"><button onClick={this.restart}>Restart</button></td>
    </tr>);
    } });

ReactDOM.render(
  <App />,
  document.getElementById('content')
);
