import React, { Component } from 'react';
import { NavMenu } from './NavMenu';

export class GraphLayout extends Component {
  static displayName = GraphLayout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <div>
          {this.props.children}
        </div>
      </div>
    );
  }
}
