import React, { Component } from 'react';
import $ from 'jquery';
import { Col } from 'reactstrap';
import { Row } from 'reactstrap';
import Select, { createFilter } from 'react-select';
import CreatableSelect from 'react-select/creatable';
import authService from './api-authorization/AuthorizeService';

export class Hashtag {
    constructor() {
        this.id = 0;
        this.name = "";
        this.myTasks = [];
    }
}

export class Hashtags extends Component {

    constructor(props) {
        super(props);
        this.state = {
            taskId: props.taskId,
            hashtags: props.hashtags,
            options: []
        };
        this.deleteHashtag = this.deleteHashtag.bind(this);
        this.addHashtag = this.addHashtag.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.checkInput = this.checkInput.bind(this);
    }

    async getHashtagList() {
        const token = await authService.getAccessToken();
        const response = await fetch('api/Hashtags', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        for (var hashtag of this.state.hashtags) {
            data.splice(data.findIndex(h => h.name == hashtag.name), 1);
        }
        this.setState({
            options: data.map(h => { return { value: h.name, label: h.name }; })
        });
        //console.log(data);
    }

    componentDidMount() {
        this.getHashtagList();
    }

    deleteHashtag(hashtagId) {
        //console.log('delete');
        //console.log(hashtagId);
        let index = this.state.hashtags.findIndex((h) => h.id == hashtagId);
        //console.log(index);
    }
    addHashtag(event) {
        if (event.key === 'Enter') {
            let val = $('#addHashtagBtn')[0].value;
            //console.log(val);
            this.state.hashtags.push(val)
            //console.log(val);
        }
    }
    handleChange(newValue, actionMeta) {/*
        console.group('Value Changed');
        console.log(newValue);
        console.log(`action: ${actionMeta.action}`);
        console.groupEnd();
        if (actionMeta.action === "create-option") {
            console.log("ok");
        }*/
    };
    handleInputChange(inputValue, actionMeta) {/*
        console.group('Input Changed');
        console.log(inputValue);
        console.log(`action: ${actionMeta.action}`);
        console.groupEnd();
        if (actionMeta.action === "input-change" && inputValue.length > 0 && inputValue[0] === "#") {
            console.log("ok");
        }*/
    };
    checkInput(input) {
        let hash = $('#react-select-2-input');
        let code = input.keyCode;
        if (code === 8 || (input.shiftKey === true && code === 51 && (hash.length > 0 && (hash[0].value === undefined || hash[0].value.length === 0))) || (input.shiftKey === false && code >= 48 && code <= 57) || (code >= 65 && code <= 90)) {
            //console.log(code);
        } else if (code === 13 && (hash.length > 0 && (hash[0].value === undefined || hash[0].value.length > 1)) && hash[0].value[0] === "#") {
            //console.log("enter");
        } else {
            input.preventDefault();
        }
    }


    renderHashtag(hashtags) {
        return (
            <div>
                <Row className="task-hashtags">
                    {
                        hashtags.map(hashtag => (
                            <Col className="col-sm-auto hashtagList" key={hashtag.id} onDoubleClick={(event) => this.deleteHashtag(hashtag.id)}>{hashtag.name}</Col>
                        ))
                    }
                </Row>
                <Row className="task-hashtags">
                    <CreatableSelect id="task-hashtagSelect" isClearable value={this.state.hash} onKeyDown={this.checkInput} filterOption={createFilter({ matchFrom: 'start', trim: true })} onChange={this.handleChange} onInputChange={this.handleInputChange} options={this.state.options} />
                </Row>
            </div>
        );
    }

    render() {
        return (
            <div style={{ width: '100%' }}>
                {this.renderHashtag(this.state.hashtags)}
            </div>
        );
    }
}