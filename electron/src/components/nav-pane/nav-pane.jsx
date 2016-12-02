import React, { Component, PropTypes } from 'react';
import LinkPlate from '../link-plate/link-plate';

class NavPane extends Component {
    render() {
        return (
            <div className='nav-pane'>
            	<ul>
	            {
	            	this.props.pages.map(page => (
	            	    <li key={page.path}>
                            <LinkPlate path={page.path} name={page.name} />
                        </li>
	            ))}
	          </ul>
        	</div>
        )
    }
}

export default NavPane;
