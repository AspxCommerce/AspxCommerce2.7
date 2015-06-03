/**
* tablePagination - A table plugin for jQuery that creates pagination elements
*
* http://neoalchemy.org/tablePagination.html
*
* Copyright (c) 2009 Ryan Zielke (neoalchemy.com)
* licensed under the MIT licenses:
* http://www.opensource.org/licenses/mit-license.php
*
* @name tablePagination
* @type jQuery
* @param Object settings;
*      firstArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="./images/first.gif"
*      prevArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="./images/prev.gif"
*      lastArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="./images/last.gif"
*      nextArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="./images/next.gif"
*      rowsPerPage - Number - used to determine the starting rows per page. Default: 5
*      currPage - Number - This is to determine what the starting current page is. Default: 1
*      optionsForRows - Array - This is to set the values on the rows per page. Default: [5,10,25,50,100]
*      ignoreRows - Array - This is to specify which 'tr' rows to ignore. It is recommended that you have those rows be invisible as they will mess with page counts. Default: []
*      topNav - Boolean - This specifies the desire to have the navigation be a top nav bar
*
*
* @author Ryan Zielke (neoalchemy.org)
* @version 0.5
* @requires jQuery v1.2.3 or above
*/
(function(a) { a.fn.tablePagination = function(b) { var c = { firstArrow: (new Image()).src = "./images/first.gif", prevArrow: (new Image()).src = "./images/prev.gif", lastArrow: (new Image()).src = "./images/last.gif", nextArrow: (new Image()).src = "./images/next.gif", rowsPerPage: 5, currPage: 1, optionsForRows: [5, 10, 25, 50, 100], ignoreRows: [], topNav: false }; b = a.extend(c, b); return this.each(function() { var s = a(this)[0]; var h, f, l, i, j, u, q; h = "#tablePagination_totalPages"; f = "#tablePagination_currPage"; l = "#tablePagination_rowsPerPage"; i = "#tablePagination_firstPage"; j = "#tablePagination_prevPage"; u = "#tablePagination_nextPage"; q = "#tablePagination_lastPage"; var r = (c.topNav) ? "prev" : "next"; var t = a.makeArray(a("tbody tr", s)); var k = a.grep(t, function(x, w) { return (a.inArray(x, c.ignoreRows) == -1) }, false); var d = k.length; var v = e(); var m = (c.currPage > v) ? 1 : c.currPage; if (a.inArray(c.rowsPerPage, c.optionsForRows) == -1) { c.optionsForRows.push(c.rowsPerPage) } function o(y) { if (y == 0 || y > v) { return } var z = (y - 1) * c.rowsPerPage; var x = (z + c.rowsPerPage - 1); a(k).show(); for (var w = 0; w < k.length; w++) { if (w < z || w > x) { a(k[w]).hide() } } } function e() { var x = Math.round(d / c.rowsPerPage); var w = (x * c.rowsPerPage < d) ? x + 1 : x; if (a(s)[r]().find(h).length > 0) { a(s)[r]().find(h).html(w) } return w } function n(w) { if (w < 1 || w > v) { return } m = w; o(m); a(s)[r]().find(f).val(m) } function p() { var y = false; var z = c.optionsForRows; z.sort(function(B, A) { return B - A }); var x = a(s)[r]().find(l)[0]; x.length = 0; for (var w = 0; w < z.length; w++) { if (z[w] == c.rowsPerPage) { x.options[w] = new Option(z[w], z[w], true, true); y = true } else { x.options[w] = new Option(z[w], z[w]) } } if (!y) { c.optionsForRows == z[0] } } function g() { var w = []; w.push("<div id='tablePagination'>"); w.push("<span id='tablePagination_perPage'>"); w.push("<select id='tablePagination_rowsPerPage'><option value='5'>5</option></select>"); w.push("per page"); w.push("</span>"); w.push("<span id='tablePagination_paginater'>"); w.push("<img id='tablePagination_firstPage' src='" + c.firstArrow + "'>"); w.push("<img id='tablePagination_prevPage' src='" + c.prevArrow + "'>"); w.push("Page"); w.push("<input id='tablePagination_currPage' type='input' value='" + m + "' size='1'>"); w.push("of <span id='tablePagination_totalPages'>" + v + "</span>"); w.push("<img id='tablePagination_nextPage' src='" + c.nextArrow + "'>"); w.push("<img id='tablePagination_lastPage' src='" + c.lastArrow + "'>"); w.push("</span>"); w.push("</div>"); return w.join("").toString() } if (a(s)[r]().find(h).length == 0) { if (c.topNav) { a(this).before(g()) } else { a(this).after(g()) } } else { a(s)[r]().find(f).val(m) } p(); o(m); a(s)[r]().find(i).bind("click", function(w) { n(1) }); a(s)[r]().find(j).bind("click", function(w) { n(m - 1) }); a(s)[r]().find(u).bind("click", function(w) { n(parseInt(m) + 1) }); a(s)[r]().find(q).bind("click", function(w) { n(v) }); a(s)[r]().find(f).bind("change", function(w) { n(this.value) }); a(s)[r]().find(l).bind("change", function(w) { c.rowsPerPage = parseInt(this.value, 10); v = e(); n(1) }) }) } })(jQuery);