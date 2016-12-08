import { injectReducer } from '../../store/reducers'

export default (store) => ({
    path: 'files',
    getComponent(nextState, cb) {
        require.ensure([], (require) => {
            const FileList = require('./file-list-container').default;
            const reducer = require('./file-list-module').default;
            injectReducer(store, { key: 'file-list', reducer });
            cb(null, FileList);
        }, 'file-list')
    }
})
