import * as d3 from "d3";
import api from '../../utils/api';

export var GraphContextMenuListener = function ({ graphIneractor, onNodeClickHandler }) {
  var settings = {
    menuSelector: '#contextMenu',
    onNodeClickHandler: onNodeClickHandler
  };

  var initialize = function () {
    if (d3.select(settings.menuSelector) == null)
      throw "No context menu template found on page!"

    d3.select('.graph-container').on('contextmenu', onContextMenu);
    d3.select('body').on('click', hideContextMenu)
  }

  var onContextMenu = function () {
    if (d3.event.ctrlKey)
      return;

    d3.event.preventDefault();

    var menu = d3.select(settings.menuSelector)
      .attr('invokedOn', d3.event.target.tagName)
      .style({
        'display': 'block',
        'position': 'absolute',
        'left': (d3.event.clientX - 10) + 'px',
        'top': (d3.event.clientY - 10) + 'px'
      })
      .on('click', null)
      .on('click', function () {
        d3.event.preventDefault();
        if (!d3.event.target.classList.contains('dropdown-item'))
          return;

        hideContextMenu();

        var invokedOn = menu.attr('invokedOn');
        var selectedMenu = d3.event.target;

        if (selectedMenu.classList.contains('add-node'))
          graphIneractor.addNode();
        else if (selectedMenu.classList.contains('remove-graph-element'))
          graphIneractor.removeSelectedElement();
        else if (selectedMenu.classList.contains('save-graph'))
          saveGraph(graphIneractor.exportData());
      });

    return false;
  }

  const saveGraph = function(data) {
    api.post('api/graph/save', data);
  }

  var hideContextMenu = () => d3.select(settings.menuSelector).style('display', 'none');

  initialize();
};