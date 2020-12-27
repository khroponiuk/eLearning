import React, { useEffect } from 'react';
import { GraphContextMenuListener } from '../graph-context-menu.js';

export default function GraphContextMenu(props) {

  useEffect(() => {
    const listener = new GraphContextMenuListener(props.graph)
  }, [])

  return (
    <ul id="contextMenu" className="dropdown-menu" role="menu" style={{ display: 'none' }}>
      <li>
        <a className="dropdown-item add-node" tabIndex="-1" href="#">Add node</a>
      </li>
      <li>
        <a className="dropdown-item remove-graph-element" tabIndex="-1" href="#">Remove element</a>
      </li>
      <li className="dropdown-divider"></li>
      <li>
        <a className="dropdown-item go-to-topic-page" tabIndex="-1" href="#">Go to linked page</a>
      </li>
    </ul>
  );
}
