import React, { useState, useEffect } from 'react';
import api from '../utils/api'
import { GraphObject } from './GraphObject';
import Spinner from './Spinner';
import { withRouter } from 'react-router-dom';


const Home = withRouter(({ history }) => {
  const [loading, setLoading] = useState(true);
  const [graphState, setGraphState] = useState({});

  useEffect(() => {
    async function fetchData() {
      const data = await api.get('api/graph/main');

      setGraphState(data);
      setLoading(false);
    }

    fetchData();
  }, []);

  const onNodeClick = function (nodeId) {
    history.push('course/' + nodeId);
  }

  return (
    <div>
      {
        loading ?
          <Spinner /> :
          <GraphObject
            graphId={graphState.id}
            graphType={graphState.type}
            graphNodes={graphState.nodes}
            graphEdges={graphState.edges}
            scale={graphState.scale}
            translate={[graphState.translateX, graphState.translateX]}
            onNodeClick={onNodeClick}
          />
      }
    </div>
  );
});

export default Home
