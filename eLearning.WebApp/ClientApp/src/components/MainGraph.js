import React, { Component } from 'react';

export class MainGraph extends Component {
  static displayName = "Test name";

  componentDidMount() {
    let container = document.querySelector('.graphcontainer');

    let width = container.offsetWidth;
    let xLoc = width / 2 - 25,
        yLoc = 100;

    var nodes = [
      { id: 0, title: "Introduction", x: xLoc - 450, y: yLoc },

      { id: 1, title: "First topic", x: xLoc - 250, y: yLoc },
      { id: 2, title: "Second topic", x: xLoc - 50, y: yLoc },
      { id: 3, title: "Third topic", x: xLoc - 250, y: yLoc + 150 },
      { id: 4, title: "Fourth topic", x: xLoc - 50, y: yLoc + 150 },
      { id: 5, title: "Fifth topic", x: xLoc + 200, y: yLoc + 75 },
    ];
    var edges = [
        { source: nodes[0], target: nodes[1] },
        { source: nodes[1], target: nodes[2] },
        { source: nodes[2], target: nodes[5] },
        { source: nodes[1], target: nodes[3] },
        { source: nodes[3], target: nodes[4] },
        { source: nodes[4], target: nodes[5] },
    ];

    let graphData = { nodes, edges };
    

    Graph = new GraphInteractor(".graph-container", "test", graphData.nodes, graphData.edges, true);
  }

  fetchData() {
    
  }

  render () {
    return (
        <div class="graphcontainer"></div>
    );
  }
}
