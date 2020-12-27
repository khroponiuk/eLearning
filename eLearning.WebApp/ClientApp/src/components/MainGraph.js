import React, { useState, useEffect } from 'react';
import { GraphObject } from './GraphObject';

export function MainGraph() {
  //const [graphState, setGraphState] = useState({
  //  nodes: [
  //    { "id": 0, "title": "Introduction", "x": 80, "y": 100 },
  //    { "id": 1, "title": "First topic", "x": 280, "y": 100 },
  //    { "id": 2, "title": "Second topic", "x": 480, "y": 100 },
  //    { "id": 3, "title": "Third topic", "x": 280, "y": 250 },
  //    { "id": 4, "title": "Fourth topic", "x": 480, "y": 250 },
  //    { "id": 5, "title": "Fifth topic", "x": 730, "y": 175 }
  //  ],
  //  edges: [
  //    { "source": { "id": 0, "title": "Introduction", "x": 80, "y": 100 }, "target": { "id": 1, "title": "First topic", "x": 280, "y": 100 } },
  //    { "source": { "id": 1, "title": "First topic", "x": 280, "y": 100 }, "target": { "id": 2, "title": "Second topic", "x": 480, "y": 100 } },
  //    { "source": { "id": 2, "title": "Second topic", "x": 480, "y": 100 }, "target": { "id": 5, "title": "Fifth topic", "x": 730, "y": 175 } },
  //    { "source": { "id": 1, "title": "First topic", "x": 280, "y": 100 }, "target": { "id": 3, "title": "Third topic", "x": 280, "y": 250 } },
  //    { "source": { "id": 3, "title": "Third topic", "x": 280, "y": 250 }, "target": { "id": 4, "title": "Fourth topic", "x": 480, "y": 250 } },
  //    { "source": { "id": 4, "title": "Fourth topic", "x": 480, "y": 250 }, "target": { "id": 5, "title": "Fifth topic", "x": 730, "y": 175 } }
  //  ]
  //});

  const [graphState, setGraphState] = useState({
    "id": "66c43729-10b4-4b0a-81a6-c6a5a708f44d",
    "name": "Main graph",
    "type": 0,
    "nodes": [
      { "id": "1e1ae409-5e4a-4c87-be64-61c0cd2ee460", "x": 750, "y": 370, "name": "Topic", "graphId": "66c43729-10b4-4b0a-81a6-c6a5a708f44d" },
      { "id": "bd5611ba-2217-447c-a4c1-fed37c59874f", "x": 550, "y": 270, "name": "Intro", "graphId": "66c43729-10b4-4b0a-81a6-c6a5a708f44d" }],
    "edges": [
      { "id": "52df0c1d-8c43-417b-9d13-5d43bbbaf89d", "sourceNodeId": "bd5611ba-2217-447c-a4c1-fed37c59874f", "targetNodeId": "1e1ae409-5e4a-4c87-be64-61c0cd2ee460", "graphId": "66c43729-10b4-4b0a-81a6-c6a5a708f44d" }
    ]
  });

  useEffect(() => {
    console.log()
  });

  return (
    <GraphObject graphNodes={graphState.nodes} graphEdges={graphState.edges} />
  );
}