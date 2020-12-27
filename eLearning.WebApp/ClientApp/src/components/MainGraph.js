import React, { useState, useEffect } from 'react';
import request from '../utils/request'
import { GraphObject } from './GraphObject';
import Spinner from './Spinner';


export function MainGraph() {
  const [loading, setLoading] = useState(true);
  const [graphState, setGraphState] = useState({});

  useEffect(() => {
    async function fetchData() {
      const data = await request('api/maingraph');

      setGraphState(data);
      setLoading(false);
    }
    
    fetchData();
  }, []);

  return (
    <div>
      {loading ? <Spinner /> : <GraphObject graphNodes={graphState.nodes} graphEdges={graphState.edges} />}
    </div>
  );
}