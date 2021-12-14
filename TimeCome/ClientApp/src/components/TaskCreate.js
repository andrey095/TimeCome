import React, { Component } from 'react';
import Datetime from 'react-datetime';
import "react-datetime/css/react-datetime.css";
import 'moment/locale/en-ie';
import $ from 'jquery';
import { Col } from 'reactstrap';
import { Row } from 'reactstrap';
import { Hashtags } from './Hashtags';
import authService from './api-authorization/AuthorizeService';

export class MyTask {
    constructor() {
        let date = new Date();
        let enddate = new Date();
        enddate.setDate(date.getDate() + 7);
        this.id = 0;
        this.about = "new task";
        this.comment = "";
        this.createTime = date;
        this.endTime = enddate;
        this.status = false;
        this.applicationUserId = "";
        this.priorityId = 1;
        this.hashtags = [];
        this.taskFiles = [];
        this.myTasks = [];
    }
    convertObjectToMyTask(obj) {
        this.id = obj.id;
        this.about = obj.about;
        this.comment = obj.comment;
        this.createTime = obj.createTime;
        this.endTime = obj.endTime;
        this.status = obj.status;
        this.applicationUserId = obj.applicationUserId;
        this.priorityId = obj.priorityId;
        this.hashtags = obj.hashtags;
        this.taskFiles = obj.taskFiles;
        this.myTasks = obj.myTasks;
    }
    async setCurrentUser() {
        const user = await authService.getUser();
        this.applicationUserId = user.sub;
    }
}

export class TaskCreate extends Component {

    constructor(props) {
        super(props);
        let task = props.task === null ? new MyTask() : props.task;
        if (task.id === 0) {
            task.setCurrentUser();
        }
        this.state = {
            task: task,
            update: props.update,
            hideCurrentUser: props.hide,
            deleteCurrentUser: props.delete
        };
        this.handleSaveChanges = this.handleSaveChanges.bind(this);
        this.checkTaskStatus = this.checkTaskStatus.bind(this);
        this.checkTaskTime = this.checkTaskTime.bind(this);
    }

    async handleSaveChanges(field) {
        this.state.task[field.target.id.slice(5)] = field.target.value;
        this.setState({ task: this.state.task });
        //console.log(this.state.task);
        //console.log(field.target.value);
        console.log(field.target);

        const token = await authService.getAccessToken();
        const data = new MyTask();
        data.convertObjectToMyTask(this.state.task);
        fetch(`api/MyTasks/${this.state.task.id}`, {
            method: 'PUT',
            headers: !token ? {} : { 'Content-Type': 'application/json' ,'Authorization': `Bearer ${token}` },
            body: JSON.stringify(data)
        });
        this.state.update(this.state.task);
    }

    checkTaskStatus(done) {
        let list = $('li.active');
        if (done == true) {
            list[0].classList.add('task-done');
        } else {
            list[0].classList.remove('task-done')
        }
    }
    checkTaskTime(time) {
        let list = $('li.active');
        if (time <= Date.now()) {
            list[0].classList.add('task-overdue');
        } else {
            list[0].classList.remove('task-overdue')
        }
    }

    renderCreateForm() {
        return (
            <div id="task-form">
                <Row>
                    <label>About</label>
                </Row>
                <Row className="row-middle">
                    <input id="task-status" type="checkbox" checked={this.state.task.status} onChange={(e) => { this.handleSaveChanges({ target: { id: 'task-status', value: e.target.checked } }); this.checkTaskStatus(e.target.checked); }} className="form-control" />
                    <input id="task-about" type="text" defaultValue={this.state.task.about} onChange={this.handleSaveChanges} className="form-control" required />
                </Row>
                <Row>
                    <label>Comment</label>
                </Row>
                <Row>
                    <textarea id="task-comment" cols="5" rows="4" defaultValue={this.state.task.comment} onChange={this.handleSaveChanges} className="form-control"></textarea>
                </Row>
                <Row>
                    <label>Date Create of Task:</label>
                </Row>
                <Row>
                    <Datetime initialValue={this.state.task.createTime} initialViewDate={this.state.task.createTime} locale="en-ie" onChange={(e) => this.handleSaveChanges({ target: { id: 'task-createTime', value: e._d } }) } />
                </Row>
                <Row>
                    <label>Date Finish of Task:</label>
                </Row>
                <Row>
                    <Datetime initialValue={this.state.task.endTime} initialViewDate={this.state.task.endTime} locale="en-ie" onChange={(e) => { this.handleSaveChanges({ target: { id: 'task-endTime', value: e._d } }); this.checkTaskTime(e._d) } } />
                </Row>
                <Row>
                    <Hashtags hashtags={this.state.task.hashtags} taskId={this.state.task.id} />
                </Row>
                <Row>
                    <div onClick={this.state.hideCurrentUser}>
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" style={{ rotate: "90deg" }} className="bi bi-file-arrow-up" viewBox="0 0 16 16">
                            <path d="M8 11a.5.5 0 0 0 .5-.5V6.707l1.146 1.147a.5.5 0 0 0 .708-.708l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 .5.5z" />
                            <path d="M4 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H4zm0 1h8a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1z" />
                        </svg>
                    </div>
                    <div onClick={() => this.state.deleteCurrentUser(this.state.task.id)} style={{ marginLeft: "auto" }}>
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" />
                            <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z" />
                        </svg>
                    </div>
                </Row>
            </div>
        );
    }

    render() {
        return (
            <div>
                {this.renderCreateForm()}
                <br />
            </div>
        );
    }
}