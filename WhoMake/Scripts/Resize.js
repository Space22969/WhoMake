function onBodyResize(force) {
  var w = window, de = document.documentElement;
  if (!w.pageNode) return;

  var dwidth = Math.max(intval(w.innerWidth), intval(de.clientWidth));
  var dheight = Math.max(intval(w.innerHeight), intval(de.clientHeight));
  var sbw = sbWidth(), changed = false;

  if (browser.mobile) {
    dwidth = Math.max(dwidth, intval(bodyNode.scrollWidth));
    dheight = Math.max(dheight, intval(bodyNode.scrollHeight));
  }
  if (w.lastWindowWidth != dwidth || force === true) {
    changed = true;
    w.lastInnerWidth = w.lastWindowWidth = dwidth;
    layerWrap.style.width = boxLayerWrap.style.width = dwidth + 'px';

    var layerWidth = layer.style.width = boxLayer.style.width = (dwidth - sbw - 2) + 'px';
    if (window.mvLayerWrap && !mvcur.minimized) {
      mvLayerWrap.style.width = dwidth + 'px';
      mvLayer.style.width = layerWidth;
    }
    if (window.wkLayerWrap) {
      wkLayerWrap.style.width = dwidth + 'px';
      wkLayer.style.width = layerWidth;
    }
    if (bodyNode.offsetWidth < vk.width + sbw + 2) {
      dwidth = vk.width + sbw + 2;
    }
    if (dwidth) {
      for (var el = pageNode.firstChild; el; el = el.nextSibling) {
        if (!el.tagName) continue;
        var sfWidth = ((w.lastInnerWidth = (dwidth - sbw - 1)) - 1);
        for (var e = el.firstChild; e; e = e.nextSibling) {
          if (e.className == 'scroll_fix') {
            e.style.width = sfWidth + 'px';
          }
        }
        vk.staticheader || updateHeaderStyles({width: sfWidth});
      }
    }
    if (_pads.shown) Pads.reposition();
  }
  if (w.lastWindowHeight != dheight || force === true) {
    changed = true;
    w.lastWindowHeight = dheight;
    layerBG.style.height = boxLayerBG.style.height =
    layerWrap.style.height = boxLayerWrap.style.height = dheight + 'px';
    if (window.mvLayerWrap && !mvcur.minimized) {
      mvLayerWrap.style.height = dheight + 'px';
    }
    if (window.wkLayerWrap) {
      wkLayerWrap.style.height = dheight + 'px';
    }
    if (_pads.layerBG) _pads.layerBG.style.height = dheight + 'px';
    if (browser.mozilla && layers.visible) {
      pageNode.style.height = (_oldScroll + dheight) + 'px';
    }
  }
  if (!vk.noSideTop) {
    updSideTopLink(1);
  }
  if (changed && w.curRBox && w.curRBox.boxes && window.getWndInner) {
    var wndInner = getWndInner();
    each (curRBox.boxes, function() {this._wnd_resize(wndInner[0], wndInner[1])});
  }
  setTimeout(updSeenAdsInfo, 0);

  var ap = getAudioPlayer();
  if (ap.audioLayer && ap.audioLayer.isShown()) {
    ap.audioLayer.updatePosition();
  }

  if (cur.pvShown && window.Photoview) {
    setTimeout(Photoview.updatePhotoDimensions);
  }

  if (window.tooltips) {
    tooltips.rePositionAll();
  }
  if (cur.lSTL) {
    setStyle(cur.lSTL, {width: Math.max(getXY(cur.lSTL.el)[0], 0), height: dheight - 1});
  }
  if (ge('dev_top_nav')) {
    setStyle(ge('dev_top_nav', 'left', null));
  }
  var fixedEls = geByClass('ui_search_fixed'),
      rbar = ge('narrow_column');
  each(fixedEls, function() {
    redraw(this, 'ui_search_fixed');
    setTimeout(redraw.pbind(this, 'ui_search_fixed'), 0);
  });
  if (rbar) {
    redraw(rbar, 'fixed');
    setTimeout(redraw.pbind(rbar, 'fixed'), 0);
  }
  updateLeftMenu();
  updateNarrow();
  updateSTL();
}