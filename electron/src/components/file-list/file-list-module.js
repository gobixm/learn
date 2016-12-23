import fs from 'fs';
import { concat, map } from 'lodash';

export const FETCH_FILES = 'FETCH_FILES';
export const CHECK_FILE = 'CHECK_FILE';
export const CHECK_ALL_FILES = 'CHECK_ALL_FILES';


export const fetchFiles = (files) => {
    return {
        type: FETCH_FILES,
        files: files
    }
}

export const checkFile = (name) => {
    return {
        type: CHECK_FILE,
        name: name
    }
}

export const checkAllFiles = (name) => {
    return {
        type: CHECK_ALL_FILES
    }
}

export const fetchFilesAsync = (path) => {
    return (dispatch, getState) => {
        return new Promise((resolve) => {
            fs.readdir(path, (err, files) => {
                dispatch(fetchFiles(files));
                resolve();
            });
        });
    }
}

export const actions = {
    fetchFiles,
    fetchFilesAsync,
    checkFile,
    checkAllFiles
}

const mapFiles = (files) => {
    return map(files, (file) => {
        return {
            name: file,
            path: file,
            checked: false
        }
    });
}

const checkFileList = (files, fileName) => {
    return map(files, (file) => {
        if (file.name !== fileName) {
            return file;
        }
        file.checked = !file.checked;
        return file;
    });
}

const checkFileListAll = (files) => {
    return map(files, (file) => {
        file.checked = true;
        return file;
    });
}

const ACTION_HANDLERS = {
    [FETCH_FILES]: (state, action) => Object.assign({}, state, { files: mapFiles(concat(['..'], action.files)) }),
    [CHECK_FILE]: (state, action) => Object.assign({}, state, { files: checkFileList(state.files, action.name) }),
    [CHECK_ALL_FILES]: (state, action) => Object.assign({}, state, { files: checkFileListAll(state.files) })
}

const initialState = [];
export default function fileListReducer(state = initialState, action) {
    const handler = ACTION_HANDLERS[action.type]
    return handler ? handler(state, action) : state
}
