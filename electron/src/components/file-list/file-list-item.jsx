import React, { Component, PropTypes } from 'react';

class FileListItem extends Component {
    constructor() {
        super();
        this.handleCheck = this.handleCheck.bind(this);
    }
    handleCheck(e) {
        e.stopPropagation();
        this.props.onChecked();
    }
    render() {
        return (
            <div className='file-list-item' onClick={this.props.onClick}>
                <input type="checkbox" 
                	className='file-checkbox' 
                	checked={this.props.checked}
                	onClick={this.handleCheck}></input>
                <div className='file-checkbox'>{this.props.fileName}</div>
            </div>
        )
    }
}

export default FileListItem;
