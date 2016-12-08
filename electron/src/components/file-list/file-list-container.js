import { connect } from 'react-redux'
import { fetchFiles, fetchFilesAsync } from './file-list-module'
import FileList from './file-list';

const mapDispatchToProps = {
    fetchFilesAsync: fetchFilesAsync
}

const mapStateToProps = (state) => {
    return {
        fileList: state.fileList
    }
}

const FileListContainer = connect(
    mapStateToProps
)(FileList);

export default FileListContainer;
