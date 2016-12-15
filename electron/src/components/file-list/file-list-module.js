import fs from 'fs';
import { concat } from 'lodash';

export const FETCH_FILES = 'FETCH_FILES';

export const fetchFiles = (files) => {
    return {
        type: FETCH_FILES,
        files: files
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
    fetchFilesAsync
}

const ACTION_HANDLERS = {
    [FETCH_FILES]: (state, action) => Object.assign({}, state, { files: concat(['..'], action.files) })
}

const initialState = [];
export default function fileListReducer(state = initialState, action) {
    const handler = ACTION_HANDLERS[action.type]

    return handler ? handler(state, action) : state
}
