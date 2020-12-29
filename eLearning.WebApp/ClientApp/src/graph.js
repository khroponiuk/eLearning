import * as d3 from "d3";
import utils from './utils/utils'

export var GraphInteractor = function (options) {
  var graph = this;

  graph.id = options.graphId;
  graph.type = options.graphType;
  graph.editMode = options.editMode || false;

  graph.nodes = options.nodes || [];
  graph.edges = options.edges || [];
  graph.scale = options.scale || 1;
  graph.translate = options.translate || [0, 0];
  graph.onNodeClick = options.onNodeClick;

  graph.state = getInitialState();

  var svg = d3.select(options.containerSelector).select('svg');

  graph.svg = svg;
  graph.svgG = d3.select(options.containerSelector).select('g');
  var svgG = graph.svgG;

  // line displayed when dragging between nodes
  graph.dragLine = svgG
    .append('svg:path')
    .attr('class', 'link dragline hidden')
    .attr('d', 'M0,0L0,0');

  var dragSvg = d3.behavior.zoom()
    .on("zoom", function () {
      if (d3.event.sourceEvent.shiftKey)
        return false;
      d3.event.sourceEvent.preventDefault();
      graph.zoomed.call(graph);
      return true;
    })
    .on("zoomstart", function () {
      var activeElement = d3.select("#" + graph.constants.activeEditId).node();
      if (activeElement)
        activeElement.blur();

      if (!d3.event.sourceEvent.shiftKey)
        d3.select('body').style("cursor", "move");
    })
    .on("zoomend", function () {
      d3.select('body').style("cursor", "auto");
    });

  svg.call(dragSvg).on("dblclick.zoom", null);


  // svg nodes and edges 
  graph.paths = svgG.append("g").selectAll("g");
  graph.circles = svgG.append("g").selectAll("g");

  graph.drag = d3.behavior.drag()
    .origin(function (d) {
      return {
        x: d.x,
        y: d.y
      };
    })
    .on("drag", function (args) {
      graph.state.justDragged = true;
      graph.dragMove.call(graph, args);
    });

  if (options.editMode)
    graph.initializeEditorEventListeners(svg);
  else
    graph.initializeInteractorEventListeners(svg);

  graph.updateGraph();
}

function getInitialState() {
  return {
    selectedNode: null,
    selectedEdge: null,
    mouseDownNode: null,
    mouseDownLink: null,
    justDragged: false,
    justScaleTransGraph: false,
    lastKeyDown: -1,
    shiftNodeDrag: false,
    selectedText: null
  };
};

/* Prototype fucntions */

GraphInteractor.prototype.constants = getGraphConstants();

function getGraphConstants() {
  return {
    selectedClass: "selected",
    connectClass: "connect-node",
    circleGClass: "conceptG",
    graphClass: "graph",
    activeEditId: "active-editing",
    BACKSPACE_KEY: 8,
    DELETE_KEY: 46,
    ENTER_KEY: 13,
    nodeRadius: 50
  };
};

GraphInteractor.prototype.initializeInteractorEventListeners = function (svg) {
  var graph = this;
  svg.on('click', function (e, data, index) {
    if (graph.state.selectedNode == null)
      return;

    graph.onNodeClick && graph.onNodeClick(graph.state.selectedNode.id)
  });
}

GraphInteractor.prototype.initializeEditorEventListeners = function (svg) {
  var graph = this;

  d3.select(window)
    .on("keydown", function () {
      graph.svgKeyDown.call(graph);
    })
    .on("keyup", function () {
      graph.svgKeyUp.call(graph);
    });

  svg.on("mousedown", function (d) {
    graph.svgMouseDown.call(graph, d);
  });
  svg.on("mouseup", function (d) {
    graph.svgMouseUp.call(graph, d);
  });
}

GraphInteractor.prototype.dragMove = function (drag) {
  var graph = this;
  if (graph.state.shiftNodeDrag) {
    var x = drag.x,
      y = drag.y,
      node = graph.svgG.node();

    graph.dragLine.attr('d', 'M' + x + ',' + y + 'L' + d3.mouse(node)[0] + ',' + d3.mouse(node)[1]);
  } else {
    drag.x += d3.event.dx;
    drag.y += d3.event.dy;
    graph.updateGraph();
  }
}

GraphInteractor.prototype.selectElementContents = function (el) {
  var range = document.createRange();
  range.selectNodeContents(el);
  var selection = window.getSelection();
  selection.removeAllRanges();
  selection.addRange(range);
};

GraphInteractor.prototype.insertTitleLinebreaks = function (gEl, title) {
  var words = title.split(/\s+/g),
    wordsNumber = words.length;
  var el = gEl.append("text")
    .attr("text-anchor", "middle")
    .attr("dy", "-" + (wordsNumber - 1) * 7.5);

  for (var i = 0; i < words.length; i++) {
    var tspan = el.append('tspan').text(words[i]);
    if (i > 0)
      tspan.attr('x', 0).attr('dy', '15');
  }
};

GraphInteractor.prototype.removeNodeEdges = function (node) {
  var graph = this;

  var edgesToRemove = graph.edges
    .filter(function (e) {
      return (e.source === node || e.target === node);
    });

  edgesToRemove.map(function (e) {
    graph.edges.splice(graph.edges.indexOf(e), 1);
  });
};

GraphInteractor.prototype.replaceSelectEdge = function (d3Path, edgeData) {
  var graph = this;
  d3Path.classed(graph.constants.selectedClass, true);

  if (graph.state.selectedEdge) {
    graph.removeSelectFromEdge();
  }
  graph.state.selectedEdge = edgeData;
};

GraphInteractor.prototype.replaceSelectNode = function (d3Node, nodeData) {
  var graph = this;
  d3Node.classed(graph.constants.selectedClass, true);

  if (graph.state.selectedNode) {
    graph.removeSelectFromNode();
  }
  graph.state.selectedNode = nodeData;
};

GraphInteractor.prototype.removeSelectFromNode = function () {
  var graph = this;
  graph.circles.filter(function (cd) {
    return cd.id === graph.state.selectedNode.id;
  })
    .classed(graph.constants.selectedClass, false);

  graph.state.selectedNode = null;
};

GraphInteractor.prototype.removeSelectFromEdge = function () {
  var graph = this;
  graph.paths.filter(function (cd) {
    return cd === graph.state.selectedEdge;
  })
    .classed(graph.constants.selectedClass, false);

  graph.state.selectedEdge = null;
};

GraphInteractor.prototype.pathMouseDown = function (d3path, d) {
  var graph = this,
    state = graph.state,
    previousEdge = state.selectedEdge;

  d3.event.stopPropagation();
  state.mouseDownLink = d;

  if (state.selectedNode) {
    graph.removeSelectFromNode();
  }

  if (!previousEdge || previousEdge !== d) {
    graph.replaceSelectEdge(d3path, d);
  } else {
    graph.removeSelectFromEdge();
  }
};

GraphInteractor.prototype.circleMouseDown = function (d3node, d) {
  var graph = this,
    state = graph.state;

  d3.event.stopPropagation();
  state.mouseDownNode = d;

  if (d3.event.shiftKey) {
    state.shiftNodeDrag = d3.event.shiftKey;
    // reposition dragged directed edge
    graph.dragLine.classed('hidden', false)
      .attr('d', 'M' + d.x + ',' + d.y + 'L' + d.x + ',' + d.y);
    return;
  }
};

GraphInteractor.prototype.changeTextOfNode = function (d3node, d) {
  var graph = this,
    constants = graph.constants,
    containerRect = document.querySelector("body .graph-container").getBoundingClientRect(),
    graphNode = d3node.node(),
    graphNodeBCR = graphNode.getBoundingClientRect(),
    curScale = graphNodeBCR.width / constants.nodeRadius;

  var x = constants.nodeRadius * (2 - Math.sqrt(3)) / 2,
    y = constants.nodeRadius / 2;

  d3node.selectAll("text").remove();

  var d3txt = graph.svg.selectAll("foreignObject")
    .data([d])
    .enter()
    .append("foreignObject")
    .attr("x", graphNodeBCR.left + x - containerRect.left)
    .attr("y", graphNodeBCR.top + y - containerRect.top)
    .attr("height", constants.nodeRadius)
    .attr("width", Math.sqrt(3) * constants.nodeRadius)
    .append("xhtml:p")
    .attr("id", constants.activeEditId)
    .attr("contentEditable", "true")
    .text(d.title)
    .on("mousedown", function (d) {
      d3.event.stopPropagation();
    })
    .on("keydown", function (d) {
      d3.event.stopPropagation();
      if (d3.event.keyCode == constants.ENTER_KEY && !d3.event.shiftKey) {
        this.blur();
      }
    })
    .on("blur", function (d) {
      d.title = this.textContent;
      graph.insertTitleLinebreaks(d3node, d.title);
      d3.select(this.parentElement).remove();
    });
  return d3txt;
};

GraphInteractor.prototype.circleMouseUp = function (d3node, d) {
  var graph = this,
    state = graph.state,
    constants = graph.constants;

  // reset the states
  state.shiftNodeDrag = false;
  d3node.classed(constants.connectClass, false);

  var mouseDownNode = state.mouseDownNode;
  if (!mouseDownNode)
    return;

  graph.dragLine.classed("hidden", true);

  if (mouseDownNode !== d) {
    // we're in a different node: create new edge for mousedown edge and add to graph
    var newEdge = { id: utils.generateGuid(), source: mouseDownNode, target: d };
    var filtRes = graph.paths.filter(function (d) {
      if (d.source === newEdge.target && d.target === newEdge.source) {
        graph.edges.splice(graph.edges.indexOf(d), 1);
      }
      return d.source === newEdge.source && d.target === newEdge.target;
    });
    if (!filtRes[0].length) {
      graph.edges.push(newEdge);
      graph.updateGraph();
    }
  } else {
    // we're in the same node
    if (state.justDragged) {
      // dragged, not clicked
      state.justDragged = false;
    } else {
      // clicked, not dragged
      if (d3.event.shiftKey) {
        // shift-clicked node: edit text content
        var d3txt = graph.changeTextOfNode(d3node, d);
        var txtNode = d3txt.node();
        graph.selectElementContents(txtNode);
        txtNode.focus();
      } else {
        if (state.selectedEdge) {
          graph.removeSelectFromEdge();
        }
        var prevNode = state.selectedNode;

        if (!prevNode || prevNode.id !== d.id) {
          graph.replaceSelectNode(d3node, d);
        } else {
          graph.removeSelectFromNode();
        }
      }
    }
  }
  state.mouseDownNode = null;

  return;
};

// mousedown on main svg
GraphInteractor.prototype.svgMouseDown = function () {
  this.state.graphMouseDown = true;
};

// mouseup on main svg
GraphInteractor.prototype.svgMouseUp = function () {
  var graph = this,
    state = graph.state;

  if (state.justScaleTransGraph) {
    // dragged not clicked
    state.justScaleTransGraph = false;
  } else if (state.graphMouseDown && d3.event.shiftKey) {
    // clicked not dragged from svg
    graph.addNode();
  } else if (state.shiftNodeDrag) {
    // dragged from node
    state.shiftNodeDrag = false;
    graph.dragLine.classed("hidden", true);
  }
  state.graphMouseDown = false;
};

GraphInteractor.prototype.addNode = function () {
  var graph = this,
    nodeId = utils.generateGuid();

  var xyCoords = d3.mouse(graph.svgG.node()),
    d = {
      id: nodeId,
      title: "Topic",
      x: xyCoords[0],
      y: xyCoords[1]
    };
  graph.nodes.push(d);
  graph.updateGraph();
  // make title of text immediently editable
  var d3txt = graph.changeTextOfNode(graph.circles.filter(function (dval) {
    return dval.id === d.id;
  }), d),
    txtNode = d3txt.node();
  graph.selectElementContents(txtNode);
  txtNode.focus();
};

// keydown on main svg
GraphInteractor.prototype.svgKeyDown = function () {
  var graph = this,
    state = graph.state,
    constants = graph.constants;
  // make sure repeated key presses don't register for each keydown
  if (state.lastKeyDown !== -1) return;

  state.lastKeyDown = d3.event.keyCode;
  var selectedNode = state.selectedNode,
    selectedEdge = state.selectedEdge;

  switch (d3.event.keyCode) {
    case constants.BACKSPACE_KEY:
    case constants.DELETE_KEY:
      d3.event.preventDefault();
      if (selectedNode) {
        graph.removeNode();
      } else if (selectedEdge) {
        graph.removeEdge();
      }
      break;
  }
};

GraphInteractor.prototype.removeSelectedElement = function () {
  var graph = this,
    state = graph.state;

  if (state.selectedNode && state.selectedNode.id != 0) {
    graph.removeNode();
  } else if (state.selectedEdge) {
    graph.removeEdge();
  }
};

GraphInteractor.prototype.removeNode = function () {
  var graph = this,
    state = graph.state,
    nodeId = state.selectedNode.id;

  graph.nodes.splice(graph.nodes.indexOf(state.selectedNode), 1);
  graph.removeNodeEdges(state.selectedNode);
  state.selectedNode = null;
  graph.updateGraph();

  removeTopic(nodeId);
}

function removeTopic(nodeId) {
  // $.post('/Courses/DeleteTopic', { id: nodeId });
}

GraphInteractor.prototype.removeEdge = function () {
  var graph = this,
    state = graph.state;

  graph.edges.splice(graph.edges.indexOf(state.selectedEdge), 1);
  state.selectedEdge = null;
  graph.updateGraph();
}

GraphInteractor.prototype.svgKeyUp = function () {
  this.state.lastKeyDown = -1;
};

// call to propagate changes to graph
GraphInteractor.prototype.updateGraph = function () {

  var graph = this,
    constants = graph.constants,
    state = graph.state;

  graph.paths = graph.paths.data(graph.edges, function (d) {
    return String(d.source.id) + "+" + String(d.target.id);
  });
  var paths = graph.paths;
  // update existing paths
  paths.style('marker-end', 'url(#end-arrow)')
    .classed(constants.selectedClass, function (d) {
      return d === state.selectedEdge;
    })
    .attr("d", function (d) {
      return "M" + d.source.x + "," + d.source.y + "L" + d.target.x + "," + d.target.y;
    });

  // add new paths
  paths.enter()
    .append("path")
    .style('marker-end', 'url(#end-arrow)')
    .classed("link", true)
    .attr("d", function (d) {
      return "M" + d.source.x + "," + d.source.y + "L" + d.target.x + "," + d.target.y;
    })
    .on("mousedown", function (d) {
      graph.pathMouseDown.call(graph, d3.select(this), d);
    }
    )
    .on("mouseup", function (d) {
      state.mouseDownLink = null;
    });

  // remove old links
  paths.exit().remove();

  // update existing nodes
  graph.circles = graph.circles.data(graph.nodes, function (d) { return d.id; });
  graph.circles.attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; });

  // add new nodes
  var newGs = graph.circles.enter()
    .append("g");

  newGs.classed(constants.circleGClass, true)
    .attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; })
    .on("mouseover", function (d) {
      if (state.shiftNodeDrag) {
        d3.select(this).classed(constants.connectClass, true);
      }
    })
    .on("mouseout", function (d) {
      d3.select(this).classed(constants.connectClass, false);
    })
    .on("mousedown", function (d) {
      graph.circleMouseDown.call(graph, d3.select(this), d);
    })
    .on("mouseup", function (d) {
      graph.circleMouseUp.call(graph, d3.select(this), d);
    })
    .call(graph.drag);

  newGs.append("circle")
    .attr("r", String(constants.nodeRadius))
    .attr("class", function (item) {
      if (item.isOpen)
        return "open"

      return "closed";
    });

  newGs.each(function (d) {
    graph.insertTitleLinebreaks(d3.select(this), d.title);
  });

  // remove old nodes
  graph.circles.exit().remove();
};

GraphInteractor.prototype.zoomed = function () {
  this.state.justScaleTransGraph = true;
  d3.select("." + this.constants.graphClass)
    .attr("transform", "translate(" + d3.event.translate + ") scale(" + d3.event.scale + ")");

  this.scale = d3.event.scale;
  this.translate = d3.event.translate;
};

GraphInteractor.prototype.exportData = function () {
  var graph = this;

  var edges = [];
  graph.edges.forEach(function (val, i) {
    edges.push({ id: val.id, sourceNodeId: val.source.id, targetNodeId: val.target.id, graphId: graph.id });
  });
  var nodes = [];
  graph.nodes.forEach(function (item, i) {
    nodes.push({ id: item.id, name: item.title, x: item.x, y: item.y, isCompleted: item.isCompleted, graphId: graph.id });
  });

  return {
    id: graph.id,
    type: graph.type,
    scale: graph.scale,
    translateX: graph.translate[0],
    translateY: graph.translate[1],
    translate: graph.translate,
    nodes: nodes,
    edges: edges
  };
}
