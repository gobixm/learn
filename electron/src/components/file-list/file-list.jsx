import React, { Component, PropTypes } from 'react';

class FileList extends Component {
    constructor() {
        super();
    }
    componentDidMount() {
        console.log(this.props);
    }
    render() {
        return (
            <div className='file-list'>
                
            </div>
        )
    }
}

export default FileList;
