/*
* jQuery UI Tree 1.7.1
*
* Copyright (c) 2009 Titkov Anton, ElSoft company (http://elsoft.tomsk.ru)
* Dual licensed under the MIT (MIT-LICENSE.txt)
* and GPL (GPL-LICENSE.txt) licenses.
*
* http://elsoft.tomsk.ru/jQuery/
*
* Version 0.1
*
* Depends:
*	ui.core.js
*	ui.draggable.js
*	ui.droppable.js - modified by Titkov Anton (http://elsoft.tomsk.ru/jQuery)
*/

/*
[
{
title : '1',
className : 'myClass',
type : 'node', //or 'list' or undefined(check attr children for define)
expand : 'false',//or true
img : url,
children : null
},
{
title : '1',
url : 'gogogo',
className : 'myClass',
children : null
}
]
*/
var g;
var mas = [];
(function($) {
    $.widget("ui.tree", {

        _init: function() {
            var options = $.extend(true, {}, $_ui_tree_defaults, this.options);
            if (this.options.droppable) options.droppable = this.options.droppable;
            if (this.options.draggable) options.draggable = this.options.draggable;
            this.options = options;

            if (this.options.IEDragBugFix && $.ui.draggable && !$.ui.draggable.prototype.__mouseCapture) {
                $.ui.draggable.prototype.__mouseCapture = true;
                var d = $.ui.draggable.prototype._mouseCapture;
                $.ui.draggable.prototype._mouseCapture = function(event) {
                    var result = d.call(this, event);
                    (result && $.browser.msie && event.stopPropagation());
                    return result;
                }
            }
            this.dragging = false;
            var self = this;
            var json = this.options.json ? this.options.json : this._getJSON(this.element);
            var ul = this._createBrunch(json).addClass('ui-tree');
            var id = this.element.attr('id');

            if (id) ul.attr('id', id);
            $('ul', ul).hide();
            this.element.replaceWith(ul);
            this.element = ul;
            ul.data('tree', this);
            this.removingElements = [];
            this._setNodeEvents(this.element);

            (this.options.expand && $('li', this.element).each(function() {
                if ($(this).is(self.options.expand)) self.expand(this);
            }));
            (this.options.hidden && this.element.hide());

            //CheckBox
            //var res = "";
            //this.data = $.extend({}, $_ui_tree_nodedatadefaults, data);
            // Checkbox mode
            //            if (this.options.checkbox && this.data.hideCheckbox != true) {
            //                res += cache.tagCheckbox;
            //            }       
            //alert(this.options.checkbox);

            if (this.options.checkbox) {
                $('li', this.element).each(function(i) {
                    if (!isUnassignedNode($(this))) {
                        $(this).prepend($('<input type="checkbox" name=' + this.id + ">"));
                    }
                    else {
                        $('li', this).each(function(j) {
                            //$(this).removeChild('input');
                            $(this).children("input").remove();
                        });
                    }
                });

                //                if (this.options.collapsable) {
                //                    // mantain compatibility with old "checkChildren" option
                //                    if (this.options.checkChildren) {
                //                        this.options.onCheck.descendants = 'check';
                //                        this.options.onUncheck.descendants = 'uncheck';
                //                    }

                //                    // mantain compatibility with old "checkChildren" option
                //                    if (this.options.checkParents) {
                //                        this.options.onCheck.ancestors = 'check';
                //                    }

                //                    // mantain compatibility with old "collapsed" option
                //                    if (this.options.collapsed) {
                //                        this.options.initializeChecked = 'collapsed';
                //                        this.options.initializeUnchecked = 'collapsed';
                //                    }

                //                    // initialize checked nodes
                //                    $("li:has(ul):has(input:checked)", this).each(function() {
                //                        $(this).prepend($('<span></span>'));
                //                        this.options.initializeChecked == 'collapsed' ? collapse($(this), this.options) : expand($(this), this.options);
                //                    });

                //                    // initialize unchecked nodes
                //                    $("li:has(ul):not(:has(input:checked))", this).each(function() {
                //                        $(this).prepend($('<span></span>'));
                //                        this.options.initializeUnchecked == 'collapsed' ? collapse($(this), this.options) : expand($(this), this.options);
                //                    });

                //                    // bind collapse on uncheck event
                //                    if (this.options.onUncheck.node == 'collapse') {
                //                        $(':checkbox:not(:checked)', this).live("click", function() {
                //                            collapse($(this).parents("li:first"), this.options);
                //                        });
                //                    } else

                //                    // bind expand on uncheck event
                //                        if (this.options.onUncheck.node == 'expand') {
                //                        $(':checkbox:not(:checked)', this).live("click", function() {
                //                            expand($(this).parents("li:first"), this.options);
                //                        });
                //                    }

                //                    // bind collapse on check event
                //                    if (this.options.onCheck.node == 'collapse') {
                //                        $(':checkbox:checked', this).live("click", function() {
                //                            collapse($(this).parents("li:first"), this.options);
                //                        });
                //                    } else

                //                    // bind expand on check event
                //                        if (this.options.onCheck.node == 'expand') {
                //                        $(':checkbox:checked', this).live("click", function() {
                //                            expand($(this).parents("li:first"), this.options);
                //                        });
                //                    }
                //                }
            }
        },

        after: function(json, node) {
            return this._change(json, node, 'after');
        },

        before: function(json, node) {
            return this._change(json, node, 'before');
        },

        append: function(json, node) {
            return this._change(json, node, 'append');
        },

        remove: function(node) {
            if (this.dragging && this.dragging[0] == this._getLI(node)[0]) this.removeDragging = true;
            else this._remove(node);
        },

        title: function(node, title) {
            if (title) this._setTitle(node, title);
            else this._getTitle(node);
        },

        attr: function(node, attrName, attrValue) {
        },

        _remove: function(node) {
            var ul = this._getLI(node).parent('ul');
            if ($('>li', ul).length == 1) ul.remove();
            else this._getLI(node).remove();
        },

        getJSON: function(node) {
            if (node == undefined) node = this.element;
            else node = this._getLI(node);
            return this._getJSON(node);
        },

        getSelect: function() {
            var select = $('.ui-tree-selected', this.element);
            if (select.length) return select;
            else return null;
        },

        nodeName: function(node) {
            if (node.attr != undefined) {
                return (node.length ? node.attr('nodeName') : $(node).attr('nodeName')).toLowerCase();
            }
        },

        select: function(node, multiselect) {
            return this._select(node, multiselect, null);
        },

        isNode: function(node) {
            var li = this._getLI(node);
            return (li.hasClass('ui-tree-node'));
        },

        isUnassignedNode: function(node) {
            var li = this._getLI(node);
            return (li.hasClass('unassigned-attributes'));
        },

        isUnassigned: function(li) {
            return (li.hasClass('unassigned-attributes'));
        },

        isList: function(node) {
            var li = this._getLI(node);
            return (li.hasClass('ui-tree-list'));
        },

        isExpand: function(node) {
            var li = this._getLI(node);
            return (li.hasClass('ui-tree-expanded'));
        },

        isCollapse: function(node) {
            var li = this._getLI(node);
            return (!li.hasClass('ui-tree-expanded'));
        },

        expand: function(node) {
            var self = this;
            var li = this._getLI(node);
            var ajax = /^\{url\:[\s\S]*?\}$/i.test($("li>span.ui-tree-title", li).text());
            if (ajax) {
                var url = $("li>span.ui-tree-title", li).text().replace(/^\{url\:([\s\S]*?)\}$/gim, '$1');
                $('ul>li', li).html("<div class=\"loading\">" + this.options.ajaxMessage + "</div>");
                var child_ul = $('ul', li);
                $.ajax({
                    url: url,
                    success: function(data) {
                        child_ul.empty();
                        $(data).each(function() {
                            (this.localName && child_ul.append(self._createBrunch(self.getJSON(this))));
                        });
                        self._setNodeEvents(child_ul);
                    }
                });
            }
            if (this.isList(li)) return false;
            if (this.isExpand(li)) return false;
            if (!this._trigger('expand', null, this._ui({}, li))) return false;
            var parents = li.parents().map(function() {
                if (self.nodeName(this) == 'li') return this;
            });
            if (!this.options.multiExpand) {
                var expanded = $('>li.ui-tree-expanded:visible', this.element);
                expanded.each(function() {
                    var el = this, col = true;
                    parents.each(function() {
                        if (this == el) col = false;
                    });
                    (col && self.collapse(this));
                });
            }
            if (!li.hasClass('ui-tree-expanded')) {
                li.addClass('ui-tree-expanded');
                parents.map(function() {
                    (!$(this).hasClass('ui-tree-expanded') && $(this).addClass('ui-tree-expanded') && self._show($('>ul', this)));
                });
                this._show($('>ul', li));
            }
            return true;
        },

        collapse: function(node) {
            var li = this._getLI(node);
            if (this.isList(li)) return false;
            if (!this._trigger('collapse', null, this._ui({}, li))) return false;
            if (this.isExpand(li)) {
                this._hide($('>ul', li));
                li.removeClass('ui-tree-expanded');
            }
            return true;
        },

        toggle: function(node) {
            var li = this._getLI(node);
            if (this.isList(li)) return false;
            if (this.isExpand(li)) return this.collapse(li);
            else return this.expand(li);
        },
        //http://www.jordivila.net/code/js/jquery/ui-widgetTree/widgetTree.js
        openNode: function($lisOpen) {
            if ($lisOpen) {
                $lisOpen.addClass('ui-tree-expanded')
                $lisOpen.children('ul')
                                .show()
                                .find('li')
                                        .removeClass('ui-tree-expanded');

            }
        },
        closeNode: function($lisClose) {
            if ($lisClose) {
                $lisClose.removeClass('ui-tree-expanded')
                $lisClose.children('ul')
                                .hide()
            }
        },
        _setNodeType: function(node, type) {
            var li = this._getLI(node);
            var removeClass = type == 'node' ? 'ui-tree-list' : 'ui-tree-node';
            var addClass = type == 'node' ? 'ui-tree-node' : 'ui-tree-list';
            li.removeClass(removeClass);
            if (!li.hasClass(addClass)) li.addClass(addClass);
        },

        _setNodeState: function(node, expandState) {
        },

        _change: function(json, node, changeMode) {
            var li = this._createBrunch(json); //.removeClass('ui-draggable-dragging');
            if (node == undefined) {
                node = this.element;
                changeMode = 'append';
            } else node = this._getLI(node);
            if (node.length == undefined) node = $(node);
            //if (!this._trigger('change', event, this._ui({}, node))) return false;
            switch (changeMode) {
                case 'before': node.before(li); break;
                case 'after': node.after(li); break;
                case 'append':
                    var ul = this._getUL(node);
                    if (!ul.length) ul = this._getUL(node.append('<ul></ul>'));
                    ul.append(li);
                    break;
                default: ;
            }

            this._setNodeEvents(li);
            return li;
        },

        _select: function(node, multiselect, event) {
            var span = this._getSPAN(node);
            if (!this._trigger('select', event, this._ui({}, node))) return false;
            if (!multiselect || !this.options.multiSelect) $('.ui-tree-selected', this.element).removeClass('ui-tree-selected');
            span.addClass('ui-tree-selected');
        },

        _show: function(el) {
            if ($.effects) {
                el.show(this.options.expandEffect, this.options.expandOptions, this.options.expandSpeed, this.options.expandCallback);
            } else {
                el.show(this.options.expandSpeed, this.options.expandCallback);
            }
        },

        _hide: function(el) {
            if ($.effects) {
                el.hide(this.options.expandEffect, this.options.expandOptions, this.options.expandSpeed, this.options.expandCallback);
            } else {
                el.hide(this.options.expandSpeed, this.options.expandCallback);
            }
        },

        _getUL: function(node) {
            node = node.length ? node : $(node);
            if (this.nodeName(node) == 'span') return $('>ul', node.parent());
            else if (this.nodeName(node) == 'li') return $('>ul', node);
            else return node;
        },

        _getLI: function(node) {
            node = node.length ? node : $(node);
            if (this.nodeName(node) == 'span') return node.parent();
            else if (this.nodeName(node) == 'ul') return node.parent();
            else return node;
        },

        _getSPAN: function(node) {
            node = node.length ? node : $(node);
            if (this.nodeName(node) == 'li') return $('>span.ui-tree-title:eq(0)', node);
            else if (this.nodeName(node) == 'ul') return $('span.ui-tree-title:eq(0)', node.parent());
            else return node;
        },

        _ui: function(ui, el) {
            ui = ui ? ui : {};
            el = el.length == undefined ? el : $(el);
            return {
                draggable: ui.draggable ? ui.draggable : el,
                droppable: ui.draggable ? el : null,
                helper: ui.helper,
                position: ui.position,
                offset: ui.offset,
                item: ui.draggable ? null : el,
                overState: ui.overState,
                target: this,
                sender: ui.draggable ? ui.draggable.data('tree') : null
            };
        },

        _createBrunch: function(json) {
            if (typeof (json) == 'string') json = this._evalJSON(json);
            var brunch = $(this._createLI(json));
            $('>ul', $('.ui-tree-expand', brunch)).show();
            return brunch;
        },

        // select all child elements of parent by selector in the tree. Need for mulpiple draggable and droppable rules
        _getAllElements: function(parent, selector) {
            parent = parent.length ? parent[0] : parent;
            return elements = $(selector, this.element).map(function() {
                if ($(this).is('li')) {
                    var i = $(this).parents().add(this).map(function() { if (this == parent) return this; });
                    if (i.length) return this;
                }
            });
        },

        // select all child elements of parent by options[n].element in the tree and exclude from result elements for other options. Need for mulpiple draggable and droppable rules
        _getElements: function(parent, options, n) {
            var elements = this._getAllElements(parent, options[n].element);
            for (var i = 0; i < options.length; i++) {
                if (i != n && options[i].element != '*') {
                    var excludeElements = this._getAllElements(parent, options[i].element);
                    elements = elements.not(excludeElements);
                }
            }
            return elements;
        },

        _createDDNodeOptions: function(options, type) {
            var self = this;
            var result = options;
            var createEvent = function(tree, eventName, treeEvent) {
                return function(event, ui) {
                    if (!treeEvent && !$(this).trigger('_tree_' + event, ui)) return false;
                    if (event.type == 'dragstart') tree.dragging = tree._getLI(this);
                    else if (event.type == 'dragstop' && tree.removeDragging) {
                        tree._remove(tree.dragging);
                        tree.dragging = tree.removeDragging = false;
                    }
                    var _ui = tree._ui(ui, this);
                    return tree._trigger(eventName, event, _ui);
                }
            }
            var ddEvents = (type == 'drag' ? this.options.dragEvents : (type == 'drop' ? this.options.dropEvents : []));
            for (var i = 0; i < ddEvents.length; i++) {
                var event = ddEvents[i];
                if (result[event] == undefined && this.options[event]) result[event] = createEvent(this, event, true);
                else {
                    result['_tree_' + event] = result[event];
                    result[event] = createEvent(this, event);
                }
            }
            if (type == 'drop') {
                var createAccept = function(tree, accept) {
                    if (accept == undefined) accept = '*';
                    var _accept = $.isFunction(accept) ? accept : function(d) {
                        return d.is(accept);
                    };
                    return function(el) {
                        var el_tree = $(el).data('tree');
                        var from_self = el_tree ? el_tree.element[0] == tree.element[0] : false;
                        if (from_self && !tree.options.acceptFromSelf) return false;
                        else if (!from_self && !tree.options.acceptFrom) return false;
                        else if (!from_self && tree.options.acceptFrom && tree.options.acceptFrom != '*') {
                            var child = false;
                            var parent = $(tree.options.acceptFrom);
                            if (!parent.length) return false;
                            $(el).parents().map(function() {
                                if (parent[0] == this) child = true;
                            });
                            if (!child) return false;
                        }
                        return _accept(el);
                    }
                }
                result.accept = createAccept(this, result.accept);
            } else if (type == 'drag') {
                result.handle = result.handle ? result.handle : '>span.ui-tree-title';
            }
            return result;
        },

        // bind events and make droppable and draggable elements in brunch el
        _setNodeEvents: function(el) {
            var droppable = this.options.droppable.length != undefined ? this.options.droppable : [this.options.droppable];
            var draggable = this.options.draggable.length != undefined ? this.options.draggable : [this.options.draggable];
            var self = this;
            for (var i = 0; i < droppable.length; i++) {
                var elements = this._getElements(el, droppable, i);
                if (elements.length) {
                    var options = this._createDDNodeOptions(droppable[i], 'drop');
                    $('>span.ui-tree-title', elements).droppable(options).data('tree', this);
                }
            }
            for (var i = 0; i < draggable.length; i++) {
                var elements = this._getElements(el, draggable, i);
                if (elements.length) {
                    var options = this._createDDNodeOptions(draggable[i], 'drag');
                    elements.draggable(options).data('tree', this);
                }
            }
            var events = $(this.options.events).not(this.options.selectOn, this.options.expandOn, this.options.collapseOn);
            var createEvent = function(eventName) {
                return function(event) {
                    self._trigger(eventName, event, self._ui({}, self._getLI(this)));
                }
            }
            var span = $('span.ui-tree-title', el);
            $(events).each(function() {
                span.bind(this, createEvent(this));
            });
            if (this.options.expandOn && this.options.expandOn == this.options.collapseOn) {
                span.bind(this.options.expandOn, function(event) {
                    if (self.isCollapse(this)) return self.expand(this);
                    else if (self.isExpand(this)) return self.collapse(this);
                })
            } else {
                (this.options.expandOn && span.bind(this.options.expandOn, function(event) { return self.expand(this); }));
                (this.options.collapseOn && span.bind(this.options.collapseOn, function(event) { return self.collapse(this); }));
            }
            (this.options.selectOn && span.bind(this.options.selectOn, function(event) { return self._select(this, self.options.multiSelectKey ? event[self.options.multiSelectKey] : true, event); }));
            span.disableSelection();
            (this.options.createbrunch && this._trigger('createbrunch', null, this._ui({}, this._getLI(el))));
        },

        _evalJSON: function(json) {
            return eval('(' + json + ')');
        },

        _getTitle: function(node) {
            var title = $('>span.ui-tree-title', node);
            var html = '';
            if (title.length) {
                html = title.html().replace(/<span class="?ui-tree-title-img"?[^>]*>[\s\S]*?<\/span>/gi, '');
            } else {
                node = node.length ? node : $(node);
                html = node.html().replace(/<ul[^>]*>[\s\S]*<\/ul>/gi, '').replace(/\s*style="[^"]*"/gi, '');
            }
            return $.trim(html.replace(/\n/g, '').replace(/<a[^>]*>/gi, '').replace(/<\/a>/gi, ''));
        },

        _setTitle: function(node, title) {
            this._getSPAN(node).html(title);
        },

        _getJSON: function(node, its_child) {
            var json = '';
            var nodeName = this.nodeName(node);
            if (nodeName == 'li') {
                json = '{';
                var title = this._getTitle(node);
                json += "'title' : '" + title + "'";
                var id = node.attr('id');
                if (id) json += ", 'id' : '" + id + "'";
                var className = $.trim(node.attr('className').replace(/ui-[^\s]*/gim, ''));
                if (className) json += ", 'className' : '" + className + "'";
                var img = $('>span.ui-tree-title>span.ui-tree-title-img>img', node);
                if (img.length) json += ", 'img' : '" + img.attr('src') + "'";
                var url = $('>span.ui-tree-title>a:eq(0)', node);
                if (!url.length) url = $('>a:eq(0)', node);
                if (url.length) json += ", 'url' : '" + url.attr('href') + "'";
                var expand = node.hasClass('.ui-tree-expanded');
                json += ", 'expand' : '" + expand + "'";

            } else if (nodeName == 'ul') {
                json += its_child ? ", 'children' : [" : "[";
            }
            var child = node.children(nodeName == 'ul' ? 'li' : (nodeName == 'li' ? 'ul' : 'xyz'));
            if (child.length > 0) {
                for (var i = 0; i < child.length; ) {
                    json += this._getJSON($(child.get(i)), true);
                    if (++i != child.length) json += ',';
                }
            }
            json += (nodeName == 'ul' ? ']' : (nodeName == 'li' ? '}' : ''));
            return json;
        },

        _createLI: function(obj) {
            //<input type="checkbox">
            if (obj.length) return this._createUL(obj);
            var html = '<li ';
            if (obj.id) html += 'id="' + obj.id + '"';
            html += 'class="';
            html += obj.children ? 'ui-tree-node' : 'ui-tree-list';
            html += obj.expand ? ' ui-tree-expand' : '';
            if (obj.className) html += ' ' + obj.className;
            html += '"><span class="ui-tree-expand-control"/><span class="ui-tree-title"><span class="ui-tree-title-img">';
            if (obj.img) html += '<img href="' + obj.img + '"/>';
            html += '</span>';
            if (obj.title) {
                if (obj.url) html += '<a href="' + obj.url + '">' + obj.title + '</a>';
                else html += obj.title;
            }
            html += '</span>';
            if (obj.children) {
                html += this._createUL(obj.children);
            }
            html += '</li>';
            return html;
        },

        _createUL: function(obj) {
            var html = '<ul>';
            if (obj.length != undefined) {
                for (var i = 0; i < obj.length; i++) {
                    html += this._createLI(obj[i]);
                }
            } else {
                html += this._createLI(obj);
            }
            html += '</ul>';
            return html;
        }

    });

    $.extend($.ui.tree, {
        version: "1.7.1",
        defaults: {}
    });

    var $_ui_tree_defaults = {
        // ajax options: object or array of object - not work!
        ajaxOptions: {
            element: '*',
            data: 'rel', //string or function(node, tree) {return ...} will be passed to request
            loadConstantly: false, // load constantly data from server on expand folder
            script: 'FileConnector.php'
        },
        ajaxMessage: 'Loading...',
        // events from node will be triggered to the tree events
        dragEvents: ['start', 'drag', 'stop'],
        dropEvents: ['activate', 'deactivate', 'stop', 'over', 'out', 'drop', 'overtop', 'overbottom', 'overright', 'overleft', 'overcenter', 'outtop', 'outbottom', 'outright', 'outleft', 'outcenter'],
        events: ['click', 'dblclick', 'mousedown', 'mouseup', 'mouseenter', 'mouseleave'],
        // drag options: object or array of object
        draggable: {
            element: '*',
            handle: '>span.ui-tree-title',
            helper: 'clone',
            revert: 'invalid',
            distance: 2
        },
        // drop options: object or array of object
        droppable: [
		{
		    element: 'li.ui-tree-node',
		    tolerance: 'around',
		    aroundTop: '25%',
		    aroundBottom: '25%',
		    aroundLeft: 0,
		    aroundRight: 0
		},
		{
		    element: 'li.ui-tree-list',
		    tolerance: 'around',
		    aroundTop: '50%',
		    aroundBottom: '50%',
		    aroundLeft: 0,
		    aroundRight: 0
		}
	],
        json: null,
        acceptFromSelf: true,
        acceptFrom: false,
        multiSelect: false,
        multiSelectKey: 'ctrlKey',
        multiExpand: true,

        expand: false,
        selectOn: 'mousedown',
        expandOn: 'mouseover',
        collapseOn: 'mouseover',
        //effect options
        expandEffect: 'blind',
        expandOptions: {},
        expandSpeed: 1000,
        expandCallback: false,
        collapseEffect: 'blind',
        collapseOptions: {},
        collapseSpeed: 1000,
        collapseCallback: false,


        // other options
        IEDragBugFix: true,

        //checkbox
        checkChildren: true,
        checkParents: true,
        collapsable: true,

        checkbox: false, // Show checkboxes.
        selectMode: 2, // 1:single, 2:multi, 3:multi-hier

        initializeChecked: 'expanded', // or 'collapsed'
        initializeUnchecked: 'expanded', // or 'collapsed'            
        onCheck: {
            ancestors: 'check', //or '', 'uncheck'
            descendants: 'check', //or '', 'uncheck'
            node: '' // or 'collapse', 'expand'
        },
        onUncheck: {
            ancestors: '', //or 'check', 'uncheck'
            descendants: 'uncheck', //or '', 'uncheck'
            node: '' // or 'collapse', 'expand'
        }
    };

})(jQuery);