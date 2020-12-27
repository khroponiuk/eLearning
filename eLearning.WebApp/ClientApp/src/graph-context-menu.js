import * as d3 from "d3";

export var GraphContextMenuListener = function (graphIneractor) {
  var settings = {
    menuSelector: '#contextMenu'
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
        //'left': getContextMenuPosition(d3.event.clientX, 'width', 'scrollLeft') + 'px',
        //'top': getContextMenuPosition(d3.event.clientY, 'height', 'scrollTop') + 'px'
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
        else if (selectedMenu.classList.contains('go-to-topic-page'))
          goToTopicPageClickHandler(graphIneractor.state.selectedNode);

        //settings.onMenuSelected.call(this, $invokedOn, $selectedMenu);
      });

    return false;
  }

  var hideContextMenu = () => d3.select(settings.menuSelector).style('display', 'none');

  var goToTopicPageClickHandler = function (node) {
    if (node == null)
      return;

    //return Utils.openUrl("/Courses/TopicDetails/" + node.id, true);

    createTopicPage(node);
  }

  var createTopicPage = function (node) {
    //$.post(settings.createTopicUrl, function (connectedPageUrl) {
    //  node.connectedPageUrl = connectedPageUrl;
    //  Utils.openUrl(connectedPageUrl, true);
    //});
  }

  initialize();
};