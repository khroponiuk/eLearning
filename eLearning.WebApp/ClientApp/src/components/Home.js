import React, { Component } from 'react';
import { MainGraph } from './MainGraph';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <MainGraph />
      </div>
    );
  }
}
