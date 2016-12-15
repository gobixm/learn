import React, { Component, PropTypes } from 'react';
import { fetchFilesAsync } from './file-list-module';
import { map } from 'lodash';
import path from 'path';

class FileList extends Component {
    constructor() {
        super();
        this.handleNavigate = this.handleNavigate.bind(this);

        this.state = {
            path: 'd:'
        }
    }
    componentDidMount() {
        this.props.dispatch(fetchFilesAsync(this.state.path));
    }
    handleNavigate(file) {
        let newPath = path.normalize(this.state.path + '/' + file);
        this.setState({ path: newPath });
        this.props.dispatch(fetchFilesAsync(newPath));
    }
    render() {
        return (
            <ul className='file-list'>
                {
                    map(this.props.files, (file) => 
                        <li key={file} 
                            className = 'file-list-item'
                            onClick={() => this.handleNavigate(file)}>
                            {file}
                        </li>
                    )
                }
            </ul>
        )
    }
}

export default FileList;
