import { combineReducers } from 'redux';
import fileList from '../components/file-list/file-list-module';

export const makeRootReducer = (asyncReducers) => {
    return combineReducers({
        fileList,
        ...asyncReducers
    })
}

export const injectReducer = (store, { key, reducer }) => {
    if (Object.hasOwnProperty.call(store.asyncReducers, key)) return

    store.asyncReducers[key] = reducer
    store.replaceReducer(makeRootReducer(store.asyncReducers))
}

export default makeRootReducer
