import React, { useState, useEffect } from 'react';
import { GraphInteractor } from '../graph.js';
import GraphContextMenu from './graph-context-menu/GraphContextMenu.js';
import authService from './api-authorization/AuthorizeService'

export function GraphObject(props) {
  const [graph, setGraph] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);

  async function loadUserRole() {
    const data = await authService.isInRole('Admin');

    setIsAdmin(data);
  }

  loadUserRole();

  const graphNodes = props.graphNodes.map(n => { return { id: n.id, title: n.name, x: n.x, y: n.y } });
  const graphEdges = props.graphEdges.map(e => { return {id: e.id, source: graphNodes.find(x => x.id === e.sourceNodeId), target: graphNodes.find(x => x.id === e.targetNodeId) } });

  useEffect(() => {
    const options = {
      graphId: props.graphId,
      graphType: props.graphType,
      containerSelector: '.graph-container',
      nodes: graphNodes,
      edges: graphEdges,
      scale: props.scale,
      translate: props.translate,
      editMode: isAdmin,
      onNodeClick: props.onNodeClick
    };
    const graphIneractor = new GraphInteractor(options);
    setGraph(graphIneractor);
  }, []);

  return (
    <div className="graph-container">
      <svg className="graph-svg-background" width="100%" height="900">
        <g className="graph"></g>
      </svg>
      {isAdmin && graph ? <GraphContextMenu options={{ graphIneractor: graph, onNodeClickHandler : props.onNodeClick}} /> : ''}
    </div>
  );
}
