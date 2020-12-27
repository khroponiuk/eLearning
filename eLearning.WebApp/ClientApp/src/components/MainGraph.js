import React, { useState, useEffect } from 'react';
import api from '../utils/api'
import { GraphObject } from './GraphObject';
import Spinner from './Spinner';
import authService from './api-authorization/AuthorizeService'


export function MainGraph() {
  const [loading, setLoading] = useState(true);
  const [graphState, setGraphState] = useState({});
  const [isAdmin, setIsAdmin] = useState(false);

  async function loadUserRole() {
    const data = await authService.isInRole('Admin');

    setIsAdmin(data);
  }

  loadUserRole();

  useEffect(() => {
    async function fetchData() {
      const data = await api.get('api/maingraph');

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