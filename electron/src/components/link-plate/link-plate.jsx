import React, { Component, PropTypes } from 'react';
import { Router, Route, Link } from 'react-router';

class LinkPlate extends Component {
    render() {
        return (
            <Link className='link-plate' to={this.props.path}>{this.props.name}</Link>
        )
    }
}

export default LinkPlate;
