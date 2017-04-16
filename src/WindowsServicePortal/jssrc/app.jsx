import React from 'react';
import ReactDOM from 'react-dom';
import {
    BrowserRouter as Router,
    Route,
    Link
} from 'react-router-dom';

var statusUrlRoot = '/api/windowsservice/status/';
var machinesUrl = '/api/windowsservice/machines';
var actionUrlRoot = '/api/windowsservice/action/';

var App = React.createClass({
    getInitialState: function () {
        return { currentMachineName: 'init', currentMachineReadOnly: true };
    },
    setCurrent: function (name, readOnly) {
        this.setState({ currentMachineName: name, currentMachineReadOnly: readOnly });
    },
    render: function () {
        return (
            <Router>
                <div>
                    <MachineList setCurrent={this.setCurrent} url={machinesUrl} currentMachineName={this.state.currentMachineName} {...this.props}/>
                    <ServiceList machineName={this.state.currentMachineName} readOnly={this.state.currentMachineReadOnly} pollInterval={3000} />
                </div>
            </Router>
        );
    }
});

var MachineList = React.createClass({
    getInitialState: function () {
        return { data: [] };
    }.bind(this),
    componentWillMount: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
            this.props.setCurrent(data[0].networkName);
        }.bind(this);
        xhr.send();
    },
    render: function () {
        var machineNodes = this.state.data.map(function (machine, i) {
            return (
                <li key={i}  className={this.props.currentMachineName === machine.networkName ? 'active' : ''}>
                    <Link to={machine.networkName} onClick={this.props.setCurrent.bind(null, machine.networkName, machine.readOnly)}>
                        {machine.displayName}
                    </Link>
                </li>)
        }, this)
        return (
            <div className='navbar navbar-inverse'>
                <a className='navbar-brand' href='#'>Machines</a>
                <ul className='nav navbar-nav'>
                    {machineNodes}
                </ul>
            </div>
        )
    }
});

var ServiceList = React.createClass({
    loadServiceStatusFromServer: function () {
        var xhr = new XMLHttpRequest();
        if (this.props.machineName != 'init') {
            xhr.open('get', statusUrlRoot + this.props.machineName, true);
            xhr.onload = function () {
                var data = JSON.parse(xhr.responseText);
                this.setState({ data: data });
            }.bind(this);
            xhr.send();
        }
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentWillMount: function () {
        this.loadServiceStatusFromServer();
        window.setInterval(this.loadServiceStatusFromServer, this.props.pollInterval)
    },
    serviceNodes: function (readOnly) {
        return (
            this.state.data.map(function (service, i) {
                return (
                    <Service key={i} name={service.name} status={service.status} machineName={service.machineName} readOnly={readOnly} />
                )
            }
            ));
    },
    render: function () {
        return (
            <div className='container'>
                {this.serviceNodes(this.props.readOnly)}
            </div>)
            ;
    }
});

var Service = React.createClass({
    actionRequest: function (action) {
        var xhr = new XMLHttpRequest();
        xhr.open('get', actionUrlRoot + action + '/' + this.props.machineName + '/' + this.props.name, true);
        xhr.send();
    },
    start: function () {
        this.actionRequest('start')
    },
    stop: function () {
        this.actionRequest('stop')
    },
    restart: function () {
        this.actionRequest('restart')
    },
    render: function () {
        return (
            <div className='row'>
                <div className='col-xs-4'>{this.props.name}</div>
                <div className='col-xs-2'>{this.props.status}</div>
                {!this.props.readOnly &&
                    <div className='col-md-2'>
                        <button className='btn btn-sm' disabled={this.props.status === 'Running' || this.props.status === 'Not Installed'}>
                            <i className='fa fa-play fa-fw' aria-hidden='true' onClick={this.start}></i>
                        </button>
                        <button className='btn btn-sm' disabled={this.props.status === 'Stopped' || this.props.status === 'Not Installed'}>
                            <i className='fa fa-stop fa-fw' aria-hidden='true' onClick={this.stop}></i>
                        </button>
                        <button className='btn btn-sm' disabled={this.props.status === 'Stopped' || this.props.status === 'Not Installed'}>
                            <i className='fa fa-refresh fa-fw' aria-hidden='true' onClick={this.restart}></i>
                        </button>
                    </div>
                }
            </div>
        );
    }
});

ReactDOM.render(
    <Router>
        <Route path="/:machine" render={props =>
            <App {...props} />
        } />
    </Router>,
    document.getElementById('content')
);
