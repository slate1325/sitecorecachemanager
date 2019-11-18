function OnResize() {
 
  if (typeof (Items) != 'undefined') {
    Items.render();
  }

  // Some Component Art styles breaks layout.
  $$(".Grid td").each(function (e) {
    //id ends with "_Items_dom"
    if (e.id.lastIndexOf("_Items_dom") + "_Items_dom".length == e.id.length) {
      e.style.height = "";
      e.style.width = "";
    }
  });
  /* re-render again after some "magic amount of time" - without this second re-render grid doesn't pick correct width sometimes */
  setTimeout("Items.render()", 150);
}

function scOnGridLoad(scHandler) {
  scHandler.add_itemSelect(itemsOnItemSelect);
  scHandler.add_itemUnSelect(itemsOnItemUnSelect);
}

window.onresize = OnResize;
Event.observe(window, "load", OnResize);
Event.observe(window, "load", OnLoad);

function OnLoad() {
  setTimeout(function () { window != window.parent && (window.parent.frameElement.style.width = window.parent.frameElement.clientWidth + 20 + 'px'); }, 200);
}

function getOuterHeight(element) {
    var layout = element.getLayout();
    var height = Number(layout.get("height"));
    var padding = Number(layout.get("padding-top")) + Number(layout.get("padding-bottom"));
    var margin = Number(layout.get("margin-top")) + Number(layout.get("margin-bottom"));
    var border = Number(layout.get("border-top")) + Number(layout.get("border-bottom"));

    return height + padding + margin + border;
}

setInterval(function () {
  var searchBox = document.querySelector("[id$=searchBox]");
  if (searchBox && searchBox.value.indexOf('\"') != -1) {
    searchBox.value = searchBox.value.replace(/"/g, "");
  };
}, 50);