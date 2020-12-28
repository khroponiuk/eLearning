import React, { useState, useEffect } from 'react';
import api from '../utils/api'
import { GraphObject } from './GraphObject';
import Spinner from './Spinner';


export function CoursePage(props) {
  const [loading, setLoading] = useState(true);
  const [graphState, setGraphState] = useState({});

  useEffect(() => {
    async function fetchData() {
      debugger
      const data = await api.get('api/graph?graphId=' + props.match.params.id);

      setGraphState(data);
      setLoading(false);
    }
    
    fetchData();
  }, []);

  return (
    <div>
      {
        loading ?
          <Spinner /> :
          <GraphObject graphId={graphState.id} graphType={graphState.type} graphNodes={graphState.nodes} graphEdges={graphState.edges} />
      }
    </div>
  );
}