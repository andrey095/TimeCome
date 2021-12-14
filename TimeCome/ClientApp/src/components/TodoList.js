import React, { Component } from 'react';
import { Link } from 'react-router-dom';
//import axios from 'axios';
import { TaskCreate, MyTask } from './TaskCreate';
import { Row } from 'reactstrap';
import { Col } from 'reactstrap';
import { ListGroup, ListGroupItem } from 'reactstrap';
import './TodoList.css';
import $ from 'jquery';
import authService from './api-authorization/AuthorizeService';

export class TodoList extends Component {

    constructor(props) {
        super(props);
        this.state = {
            tasks: [],
            loading: true,
            currentTask: null
        };
        this.handleCreate = this.handleCreate.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
        this.handleSetCurrentTask = this.handleSetCurrentTask.bind(this);
        this.handleHideCurrentTask = this.handleHideCurrentTask.bind(this);
        this.changeListItem = this.changeListItem.bind(this);
        this.setCurrentTask = this.setCurrentTask.bind(this);
    }

    async getTaskList() {
        const token = await authService.getAccessToken();
        const response = await fetch('api/MyTasks', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        for (var task of data) {
            task.createTime = new Date(task.createTime);
            task.endTime = new Date(task.endTime);
        }
        this.setState({
            tasks: data,
            loading: false
        });
    }

    componentDidMount() {
        this.getTaskList();
    }

    async handleCreate() {
        const task = new MyTask();
        await task.setCurrentUser();
        const token = await authService.getAccessToken();
        //console.log(task);
        const response = await fetch('api/MyTasks', {
            headers: !token ? {} : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
            method: 'POST',
            body: JSON.stringify(task)
        });
        //console.log(response);
        const data = await response.json();
        //console.log(data);
        data.createTime = new Date(task.createTime);
        data.endTime = new Date(task.endTime);
        this.state.tasks.push(data);
        this.setState({
            tasks: this.state.tasks
        });
    }
    async handleDelete(id) {
        if (!window.confirm(`Вы подтверждание удаление задачи: ${id}`)) {
            return;
        } else {
            console.log(id);
            const token = await authService.getAccessToken();
            fetch(`api/MyTasks/${id}`, { method: 'DELETE', headers: !token ? {} : { 'Authorization': `Bearer ${token}` } })
                .then(data => {
                    this.handleHideCurrentTask();
                    this.getTaskList();
                });
        }
    }

    changeListItem(event) {
        let list = $('li.active');
        if (list[0] !== event.target) {
            if (list.length > 0) {
                list[0].classList.remove('active')
            }
            if (event.target.tagName === 'LI') {
                event.target.classList.add('active');
            }
            //console.log(this.state.tasks);
        }
    }
    clearActiveListItem() {
        let list = $('li.active');
        if (list.length > 0) {
            list[0].classList.remove('active')
        }
    }

    handleSetCurrentTask(task) {
        const target = task;
        this.setState({ currentTask: null }, () => this.setCurrentTask(target));
        $('.col-auto').css('padding', '0px 25px 0px 25px');
        $('#table-body').css('margin-right', '10px');
    }
    setCurrentTask(task) {
        this.setState({ currentTask: task });
    }
    handleHideCurrentTask() {
        this.setState({ currentTask: null });
        $('.col-auto').css('padding', '');
        $('#table-body').css('margin-right', '');
        this.clearActiveListItem();
    }

    renderTasksList(tasks) {
        return (
            <ListGroup flush onClick={this.changeListItem}>
                {
                    tasks.map(task => (
                        <ListGroupItem key={task.id} className={task.status?"task-done":""} onClick={(event) => this.handleSetCurrentTask(task)}>{task.about}</ListGroupItem>
                    ))
                }
            </ListGroup>
        );
    }

    render() {
        let contents = this.state.loading ? <div className="loader"></div> : this.renderTasksList(this.state.tasks);
        let currentTask = this.state.currentTask === null || this.state.currentTask === undefined ? '' : <TaskCreate task={this.state.currentTask} update={(task) => this.setCurrentTask(task)} hide={this.handleHideCurrentTask} delete={this.handleDelete} />;
        return (
            <div id="todo-list">
                <h2 id="table-caption">Tasks</h2>
                <Row id="tasks-table">
                    <Col id="table-body">
                        <button className="btn btn-link" onClick={this.handleCreate}>Create New Task</button>
                        {contents}
                    </Col>
                    <Col id="task-form" xs="auto">{currentTask}</Col>
                </Row>                
            </div>
        );
    }
}