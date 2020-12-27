import React, { useState, useEffect } from 'react';
import { GraphInteractor } from '../graph.js';
import GraphContextMenu from './GraphContextMenu.js';

export function GraphObject(props) {
  const [graph, setGraph] = useState(null);

  const graphNodes = props.graphNodes.map(n => { return { id: n.id, title: n.name, x: n.x, y: n.y } });
  const graphEdges = props.graphEdges.map(e => { return { source: graphNodes.find(x => x.id === e.sourceNodeId), target: graphNodes.find(x => x.id === e.targetNodeId) } });

  useEffect(() => {
    const options = {
      graphId: 'test',
      containerSelector: '.graph-container',
      nodes: graphNodes,
      edges: graphEdges,
      editMode: true
    };
    const graphIneractor = new GraphInteractor(options);
    setGraph(graphIneractor);
  }, []);

  return (
    <div className="graph-container">
      <svg className="graph-svg-background" width="100%" height="900">
        <g className="graph"></g>
      </svg>
      {graph ? <GraphContextMenu graph={graph } /> : ''}
    </div>
  );
}
