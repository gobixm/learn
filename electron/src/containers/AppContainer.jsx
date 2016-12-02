import React, { Component, PropTypes } from 'react';
import NavPane from '../components/nav-pane/nav-pane';

class AppContainer extends Component {
    constructor() {
        super();
    }
    render() {
        return (
            <div className='app'>
            	<NavPane pages={this.props.route.pages}/>
	            {this.props.children}
            </div>
        )
    }
}

export default AppContainer
