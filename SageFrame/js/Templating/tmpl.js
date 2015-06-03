﻿(function (jQuery, undefined) {
    var oldManip = jQuery.fn.domManip, tmplItmAtt = "_tmplitem", htmlExpr = /^[^<]*(<[\w\W]+>)[^>]*$|\{\{\! /, newTmplItems = {}, wrappedItems = {}, appendToTmplItems, topTmplItem = { key: 0, data: {} }, itemKey = 0, cloneIndex = 0, stack = []; function newTmplItem(options, parentItem, fn, data) {
        var newItem = { data: data || (data === 0 || data === false) ? data : (parentItem ? parentItem.data : {}), _wrap: parentItem ? parentItem._wrap : null, tmpl: null, parent: parentItem || null, nodes: [], calls: tiCalls, nest: tiNest, wrap: tiWrap, html: tiHtml, update: tiUpdate }; if (options) { jQuery.extend(newItem, options, { nodes: [], parent: parentItem }); }
        if (fn) { newItem.tmpl = fn; newItem._ctnt = newItem._ctnt || newItem.tmpl(jQuery, newItem); newItem.key = ++itemKey; (stack.length ? wrappedItems : newTmplItems)[itemKey] = newItem; }
        return newItem;
    }
    jQuery.each({ appendTo: "append", prependTo: "prepend", insertBefore: "before", insertAfter: "after", replaceAll: "replaceWith" }, function (name, original) {
        jQuery.fn[name] = function (selector) {
            var ret = [], insert = jQuery(selector), elems, i, l, tmplItems, parent = this.length === 1 && this[0].parentNode; appendToTmplItems = newTmplItems || {}; if (parent && parent.nodeType === 11 && parent.childNodes.length === 1 && insert.length === 1) { insert[original](this[0]); ret = this; } else {
                for (i = 0, l = insert.length; i < l; i++) { cloneIndex = i; elems = (i > 0 ? this.clone(true) : this).get(); jQuery(insert[i])[original](elems); ret = ret.concat(elems); }
                cloneIndex = 0; ret = this.pushStack(ret, name, insert.selector);
            }
            tmplItems = appendToTmplItems; appendToTmplItems = null; jQuery.tmpl.complete(tmplItems); return ret;
        };
    }); jQuery.fn.extend({
        tmpl: function (data, options, parentItem) { return jQuery.tmpl(this[0], data, options, parentItem); }, tmplItem: function () { return jQuery.tmplItem(this[0]); }, template: function (name) { return jQuery.template(name, this[0]); }, domManip: function (args, table, callback, options) {
            if (args[0] && jQuery.isArray(args[0])) {
                var dmArgs = jQuery.makeArray(arguments), elems = args[0], elemsLength = elems.length, i = 0, tmplItem; while (i < elemsLength && !(tmplItem = jQuery.data(elems[i++], "tmplItem"))) { }
                if (tmplItem && cloneIndex) { dmArgs[2] = function (fragClone) { jQuery.tmpl.afterManip(this, fragClone, callback); }; }
                oldManip.apply(this, dmArgs);
            } else { oldManip.apply(this, arguments); }
            cloneIndex = 0; if (!appendToTmplItems) { jQuery.tmpl.complete(newTmplItems); }
            return this;
        }
    }); jQuery.extend({
        tmpl: function (tmpl, data, options, parentItem) {
            var ret, topLevel = !parentItem; if (topLevel) { parentItem = topTmplItem; tmpl = jQuery.template[tmpl] || jQuery.template(null, tmpl); wrappedItems = {}; } else if (!tmpl) {
                tmpl = parentItem.tmpl; newTmplItems[parentItem.key] = parentItem; parentItem.nodes = []; if (parentItem.wrapped) { updateWrapped(parentItem, parentItem.wrapped); }
                return jQuery(build(parentItem, null, parentItem.tmpl(jQuery, parentItem)));
            }
            if (!tmpl) { return []; }
            if (typeof data === "function") { data = data.call(parentItem || {}); }
            if (options && options.wrapped) { updateWrapped(options, options.wrapped); }
            ret = jQuery.isArray(data) ? jQuery.map(data, function (dataItem) { return dataItem ? newTmplItem(options, parentItem, tmpl, dataItem) : null; }) : [newTmplItem(options, parentItem, tmpl, data)]; return topLevel ? jQuery(build(parentItem, null, ret)) : ret;
        }, tmplItem: function (elem) {
            var tmplItem; if (elem instanceof jQuery) { elem = elem[0]; }
            while (elem && elem.nodeType === 1 && !(tmplItem = jQuery.data(elem, "tmplItem")) && (elem = elem.parentNode)) { }
            return tmplItem || topTmplItem;
        }, template: function (name, tmpl) {
            if (tmpl) {
                if (typeof tmpl === "string") { tmpl = buildTmplFn(tmpl) } else if (tmpl instanceof jQuery) { tmpl = tmpl[0] || {}; }
                if (tmpl.nodeType) { tmpl = jQuery.data(tmpl, "tmpl") || jQuery.data(tmpl, "tmpl", buildTmplFn(tmpl.innerHTML)); }
                return typeof name === "string" ? (jQuery.template[name] = tmpl) : tmpl;
            }
            return name ? (typeof name !== "string" ? jQuery.template(null, name) : (jQuery.template[name] || jQuery.template(null, htmlExpr.test(name) ? name : jQuery(name)))) : null;
        }, encode: function (text) { return ("" + text).split("<").join("&lt;").split(">").join("&gt;").split('"').join("&#34;").split("'").join("&#39;"); }
    }); jQuery.extend(jQuery.tmpl, { tag: { "tmpl": { _default: { $2: "null" }, open: "if($notnull_1){__=__.concat($item.nest($1,$2));}" }, "wrap": { _default: { $2: "null" }, open: "$item.calls(__,$1,$2);__=[];", close: "call=$item.calls();__=call._.concat($item.wrap(call,__));" }, "each": { _default: { $2: "$index, $value" }, open: "if($notnull_1){$.each($1a,function($2){with(this){", close: "}});}" }, "if": { open: "if(($notnull_1) && $1a){", close: "}" }, "else": { _default: { $1: "true" }, open: "}else if(($notnull_1) && $1a){" }, "html": { open: "if($notnull_1){__.push($1a);}" }, "=": { _default: { $1: "$data" }, open: "if($notnull_1){__.push($.encode($1a));}" }, "!": { open: "" } }, complete: function (items) { newTmplItems = {}; }, afterManip: function afterManip(elem, fragClone, callback) { var content = fragClone.nodeType === 11 ? jQuery.makeArray(fragClone.childNodes) : fragClone.nodeType === 1 ? [fragClone] : []; callback.call(elem, fragClone); storeTmplItems(content); cloneIndex++; } }); function build(tmplItem, nested, content) {
        var frag, ret = content ? jQuery.map(content, function (item) { return (typeof item === "string") ? (tmplItem.key ? item.replace(/(<\w+)(?=[\s>])(?![^>]*_tmplitem)([^>]*)/g, "$1 " + tmplItmAtt + "=\"" + tmplItem.key + "\" $2") : item) : build(item, tmplItem, item._ctnt); }) : tmplItem; if (nested) { return ret; }
        ret = ret.join(""); ret.replace(/^\s*([^<\s][^<]*)?(<[\w\W]+>)([^>]*[^>\s])?\s*$/, function (all, before, middle, after) {
            frag = jQuery(middle).get(); storeTmplItems(frag); if (before) { frag = unencode(before).concat(frag); }
            if (after) { frag = frag.concat(unencode(after)); }
        }); return frag ? frag : unencode(ret);
    }
    function unencode(text) { var el = document.createElement("div"); el.innerHTML = text; return jQuery.makeArray(el.childNodes); }
    function buildTmplFn(markup) {
        return new Function("jQuery", "$item", "var $=jQuery,call,__=[],$data=$item.data;" + "with($data){__.push('" + jQuery.trim(markup).replace(/([\\'])/g, "\\$1").replace(/[\r\t\n]/g, " ").replace(/\$\{([^\}]*)\}/g, "{{= $1}}").replace(/\{\{(\/?)(\w+|.)(?:\(((?:[^\}]|\}(?!\}))*?)?\))?(?:\s+(.*?)?)?(\(((?:[^\}]|\}(?!\}))*?)\))?\s*\}\}/g, function (all, slash, type, fnargs, target, parens, args) {
            var tag = jQuery.tmpl.tag[type], def, expr, exprAutoFnDetect; if (!tag) { throw "Unknown template tag: " + type; }
            def = tag._default || []; if (parens && !/\w$/.test(target)) { target += parens; parens = ""; }
            if (target) { target = unescape(target); args = args ? ("," + unescape(args) + ")") : (parens ? ")" : ""); expr = parens ? (target.indexOf(".") > -1 ? target + unescape(parens) : ("(" + target + ").call($item" + args)) : target; exprAutoFnDetect = parens ? expr : "(typeof(" + target + ")==='function'?(" + target + ").call($item):(" + target + "))"; } else { exprAutoFnDetect = expr = def.$1 || "null"; }
            fnargs = unescape(fnargs); return "');" + tag[slash ? "close" : "open"].split("$notnull_1").join(target ? "typeof(" + target + ")!=='undefined' && (" + target + ")!=null" : "true").split("$1a").join(exprAutoFnDetect).split("$1").join(expr).split("$2").join(fnargs || def.$2 || "") + "__.push('";
        }) + "');}return __;");
    }
    function updateWrapped(options, wrapped) { options._wrap = build(options, true, jQuery.isArray(wrapped) ? wrapped : [htmlExpr.test(wrapped) ? wrapped : jQuery(wrapped).html()]).join(""); }
    function unescape(args) { return args ? args.replace(/\\'/g, "'").replace(/\\\\/g, "\\") : null; }
    function outerHtml(elem) { var div = document.createElement("div"); div.appendChild(elem.cloneNode(true)); return div.innerHTML; }
    function storeTmplItems(content) {
        var keySuffix = "_" + cloneIndex, elem, elems, newClonedItems = {}, i, l, m; for (i = 0, l = content.length; i < l; i++) {
            if ((elem = content[i]).nodeType !== 1) { continue; }
            elems = elem.getElementsByTagName("*"); for (m = elems.length - 1; m >= 0; m--) { processItemKey(elems[m]); }
            processItemKey(elem);
        }
        function processItemKey(el) {
            var pntKey, pntNode = el, pntItem, tmplItem, key; if ((key = el.getAttribute(tmplItmAtt))) {
                while (pntNode.parentNode && (pntNode = pntNode.parentNode).nodeType === 1 && !(pntKey = pntNode.getAttribute(tmplItmAtt))) { }
                if (pntKey !== key) {
                    pntNode = pntNode.parentNode ? (pntNode.nodeType === 11 ? 0 : (pntNode.getAttribute(tmplItmAtt) || 0)) : 0; if (!(tmplItem = newTmplItems[key])) { tmplItem = wrappedItems[key]; tmplItem = newTmplItem(tmplItem, newTmplItems[pntNode] || wrappedItems[pntNode]); tmplItem.key = ++itemKey; newTmplItems[itemKey] = tmplItem; }
                    if (cloneIndex) { cloneTmplItem(key); }
                }
                el.removeAttribute(tmplItmAtt);
            } else if (cloneIndex && (tmplItem = jQuery.data(el, "tmplItem"))) { cloneTmplItem(tmplItem.key); newTmplItems[tmplItem.key] = tmplItem; pntNode = jQuery.data(el.parentNode, "tmplItem"); pntNode = pntNode ? pntNode.key : 0; }
            if (tmplItem) {
                pntItem = tmplItem; while (pntItem && pntItem.key != pntNode) { pntItem.nodes.push(el); pntItem = pntItem.parent; }
                delete tmplItem._ctnt; delete tmplItem._wrap; jQuery.data(el, "tmplItem", tmplItem);
            }
            function cloneTmplItem(key) { key = key + keySuffix; tmplItem = newClonedItems[key] = (newClonedItems[key] || newTmplItem(tmplItem, newTmplItems[tmplItem.parent.key + keySuffix] || tmplItem.parent)); }
        }
    }
    function tiCalls(content, tmpl, data, options) {
        if (!content) { return stack.pop(); }
        stack.push({ _: content, tmpl: tmpl, item: this, data: data, options: options });
    }
    function tiNest(tmpl, data, options) { return jQuery.tmpl(jQuery.template(tmpl), data, options, this); }
    function tiWrap(call, wrapped) { var options = call.options || {}; options.wrapped = wrapped; return jQuery.tmpl(jQuery.template(call.tmpl), call.data, options, call.item); }
    function tiHtml(filter, textOnly) { var wrapped = this._wrap; return jQuery.map(jQuery(jQuery.isArray(wrapped) ? wrapped.join("") : wrapped).filter(filter || "*"), function (e) { return textOnly ? e.innerText || e.textContent : e.outerHTML || outerHtml(e); }); }
    function tiUpdate() { var coll = this.nodes; jQuery.tmpl(null, null, null, this).insertBefore(coll[0]); jQuery(coll).remove(); }
})(jQuery);