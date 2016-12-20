import React from 'react';
import ReactDOM from 'react-dom';

var statusUrlRoot = '/api/windowsservice/status/';
var machineListUrl = '/api/windowsservice/machinenames';
var actionUrlRoot = '/api/windowsservice/action/';

var App = React.createClass({
    getInitialState: function () {
	return { currentMachineName: 'init'};
    },
    setCurrent: function (name) {
	this.setState({currentMachineName: name});
    },
    render: function () {
	return (
	    <div>
		<MachineList setCurrent={this.setCurrent} url={machineListUrl} currentMachineName={this.state.currentMachineName} />
		<ServiceList machineName={this.state.currentMachineName} pollInterval={3000} />
	    </div>
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
	    this.props.setCurrent(data[0].name);
	}.bind(this);
	xhr.send();
    },
    render: function () {
	var machineNodes = this.state.data.map(function (machine, i) {
	    return (
		<li key={i} onClick={this.props.setCurrent.bind(null, machine.name)} className={this.props.currentMachineName === machine.name ? 'active' : '' }>
		    <a href='#'>{machine.name}</a>
		</li>)
	}, this)
	return (
	    <div className='navbar navbar-fixed-left navbar-inverse'>
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
	var serviceNodes = this.state.data.map(function (service, i) {
	    return (
		<Service key={i} name={service.name} status={service.status} machineName={service.machineName} />
	    );
	})
	return (
	    <div className='container'>
		{serviceNodes}
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
		<div className='col-xs-2'>
		    <button className='btn' disabled={this.props.status === 'Running' || this.props.status === 'Not Installed'}>
			<i className='fa fa-play fa-fw' aria-hidden='true' onClick={this.start}></i>
		    </button>
		    <button className='btn' disabled={!this.props.status === 'Running' || this.props.status === 'Not Installed' || this.props.status === 'Stopped'}>
			<i className='fa fa-stop fa-fw' aria-hidden='true' onClick={this.stop}></i>
		    </button>
		    <button className='btn' disabled={!this.props.status === 'Running' || this.props.status === 'Not Installed' || this.props.status === 'Stopped'}>
			<i className='fa fa-refresh fa-fw' aria-hidden='true' onClick={this.restart}></i>
		    </button>
		</div>
	    </div>
	);
    } });

ReactDOM.render(
    <App />,
    document.getElementById('content')
);
