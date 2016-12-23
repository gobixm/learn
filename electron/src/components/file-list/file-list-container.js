import { connect } from 'react-redux';
import { map } from 'lodash';
import { fetchFiles, fetchFilesAsync } from './file-list-module';
import FileList from './file-list';

const mapDispatchToProps = () => {
    return {
        fetchFilesAsync: fetchFilesAsync
    }
}

const mapStateToProps = (state) => {
    return {
        files: state.fileList.files
    }
}

const FileListContainer = connect(
    mapStateToProps
)(FileList);

export default FileListContainer;
