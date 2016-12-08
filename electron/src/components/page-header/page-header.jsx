import React, { Component, PropTypes } from 'react'

class PageHeader extends Component {
    render() {
        return (
            <div className='page-header'>
            	<div className='page-header-text'>{this.props.text}</div>
            </div>
        )
    }
}

export default PageHeader;
