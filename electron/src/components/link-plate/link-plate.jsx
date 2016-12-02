import React, { Component, PropTypes } from 'react';
import { Router, Route, Link } from 'react-router';

class LinkPlate extends Component {
    render() {
        return (
            <div className='link-plate'>
            	<Link to={this.props.path}>{this.props.name}</Link>
        	</div>
        )
    }
}

export default LinkPlate;
