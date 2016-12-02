import React from 'react';
import ReactDom from 'react-dom';
import { Provider } from 'react-redux';
import thunkMiddleware from 'redux-thunk'
import createLogger from 'redux-logger'
import { Router, Route, IndexRoute, IndexRedirect, hashHistory } from 'react-router';
import createStore from './store/createStore';
import AppContainer from './containers/AppContainer';
import FilesPage from './containers/files-page/files-page';

const initialState = window.___INITIAL_STATE__;
const store = createStore(initialState);

ReactDom.render(
    <Provider store={store}>
	    <Router history={hashHistory}>
	    	<Route path='/' component={AppContainer} pages={
	    		[
	    			{name: 'Files', path: 'files'}
	    		]}>
	    		<Route path='files' component={FilesPage} />
	    	</Route>
		</Router>    
	</Provider>,
    document.getElementById('root-container')
)
