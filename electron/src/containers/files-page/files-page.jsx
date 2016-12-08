import React, { Component, PropTypes } from 'react';
import PageHeader from '../../components/page-header/page-header';
import FileListContainer from '../../components/file-list/file-list-container';

class FilesPage extends Component {
    constructor() {
        super();
    }
    render() {
        return (
            <div className='files-page'>
            	<PageHeader text='Files'/>
            	<FileListContainer />
            </div>
        )
    }
}

export default FilesPage
