import React, { Component, PropTypes } from 'react';
import FileListItem from './file-list-item';
import { fetchFilesAsync, checkFile, checkAllFiles } from './file-list-module';
import path from 'path';
import { map } from 'lodash';

class FileList extends Component {
    constructor() {
        super();
        this.handleNavigate = this.handleNavigate.bind(this);
        this.handleChecked = this.handleChecked.bind(this);
        this.handleCheckAll = this.handleCheckAll.bind(this);
        this.renderItem = this.renderItem.bind(this);

        this.state = {
            path: 'd:'
        }
    }
    componentDidMount() {
        this.props.dispatch(fetchFilesAsync(this.state.path));
    }
    handleNavigate(file) {
        let newPath = path.normalize(this.state.path + '/' + file.name);
        this.setState({ path: newPath });
        this.props.dispatch(fetchFilesAsync(newPath));
    }
    handleChecked(name) {
        this.props.dispatch(checkFile(name));
    }
    handleCheckAll() {
        this.props.dispatch(checkAllFiles());
    }
    renderItem(file) {
        return (
            <li key={file.name}>
              <FileListItem fileName={file.name} 
                onClick={() => this.handleNavigate(file)} 
                onChecked={() => this.handleChecked(file.name)}
                checked={file.checked}
              />
            </li>
        )

    }
    render() {
        return (
            <div>
              <button onClick={this.handleCheckAll}>Check All</button>
              <ul className = 'file-list'>
                {map(this.props.files, this.renderItem)}
              </ul>
            </div>
        )
    }
}

export default FileList;
