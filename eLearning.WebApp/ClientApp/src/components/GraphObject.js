import React, { useState, useEffect } from 'react';
import { GraphInteractor } from '../graph.js';

export function GraphObject(props) {
  const graphNodes = props.graphNodes.map(n => { return { id: n.id, title: n.name, x: n.x, y: n.y } });
  const graphEdges = props.graphEdges.map(e => { return { source: graphNodes.find(x => x.id === e.sourceNodeId), target: graphNodes.find(x => x.id === e.targetNodeId) } });

  useEffect(() => {
    const graphIneractor = new GraphInteractor(".graph-container", "test", graphNodes, graphEdges, true);
  });

  return (
    <div className="graph-container">
      <svg className="graph-svg-background" width="100%" height="900">
        <g className="graph"></g>
      </svg>
    </div>
  );
}
