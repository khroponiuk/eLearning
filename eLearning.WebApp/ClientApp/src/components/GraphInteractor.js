import React, { Component } from 'react';

export class GraphInteractor extends Component {
  static displayName = "Graph interactor";

  constructor(props) {
    super(props);
    this.state = getInitialState();
  }

  getInitialState() {
    return {
        selectedNode: null,
        selectedEdge: null,
        mouseDownNode: null,
        mouseDownLink: null,
        justDragged: false,
        justScaleTransGraph: false,
        lastKeyDown: -1,
        shiftNodeDrag: false,
        selectedText: null
    };
};


  componentDidMount() {
    
  }

  render () {
    return (
        <div class="graphcontainer"></div>
    );
  }
}
