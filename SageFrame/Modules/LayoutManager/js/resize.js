(function ($, window, document, undefined) {
    // our plugin constructor
    var Plugin = function (elem, options) {
        this.elem = elem;
        this.$elem = $(elem);
        this.elemID = '#' + $(elem).attr('id');
        this.options = options;
        this.metadata = this.$elem.data('plugin-options');
    };
    // the plugin prototype
    Plugin.prototype = {
        defaults: {
            tableName: 'tableId'
        },
        init: function () {
            this.config = $.extend({}, this.defaults, this.options, this.metadata);
            this.BindEvent(this.elem);
            this.CalculateWidth();
            this.ColsList();
            return this;
        },
        BindEvent: function (myself) {
            var tblID = '_' + $(myself).attr('id');
            $('#hdnTableName').val(tblID);
            var mine = this;
            mine.SimpleResizableTable(myself);
            $('#btnMerge' + tblID).on('click', function () {
                var id = tblID.substring(1, tblID.length);
                mine.MergeTable(myself);
                $('#' + id + ' th').each(function () {
                    $(this).removeClass('sfActive');
                });
                $(this).hide();
            });
            $('#btnSplit' + tblID).on('click', function () {
                var id = tblID.substring(1, tblID.length);
                mine.SplitTable(myself);
                $('#' + id + ' th').each(function () {
                    $(this).removeClass('sfActive');
                });
                $(this).hide();
            });
        },
        SplitTable: function (myself) {
            var tblID = '#' + $(myself).attr('id') + ' th';
            var mine = this;
            var pos = 0;
            var splitMe;
            var tblID = '#' + $(myself).attr('id') + ' th';
            $('#hdnTableName').val('_' + $(myself).attr('id'));
            $(tblID).each(function (index, value) {
                if ($(this).hasClass('sfActive')) {
                    splitMe = $(this);
                    var temp = $(mine.elemID + ' thead tr:first th').eq(index);
                    var text = temp.text();
                    var div = temp.html();
                    div = div.replace(text, text + mine.GenerateRandNum());
                    var totalWidth = parseInt(temp.css('width').split('px')[0]);
                    var tempClass = temp.attr('class');
                    var tempWidth = parseInt(tempClass.replace('sfActive', '').trim().split('_')[1]);
                    var newClass = "sfCol_" + Math.floor(tempWidth / 2);
                    var th = '<th class="sfTdSplit">' + div + '</th>';
                    temp.after(th);
                    $('.sfTdSplit').addClass(newClass);
                    //$('.sfTdSplit').css('width', totalWidth / 2);
                    temp.removeClass(tempClass).addClass(newClass);
                    //temp.css('width', totalWidth / 2);
                    $(mine.elemID + ' thead tr:first th').removeClass('sfTdSplit');
                    mine.SimpleResizableTable(myself);
                    $("#btnSplit" + tblID).attr("disabled", "disabled");
                }
            });
        },
        GenerateRandNum: function () {
            var num = '2';
            var text = '';
            var possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
            text += '_';
            for (var i = 0; i < num; i++) {
                text += possible.charAt(Math.floor(Math.random() * possible.length));
            }
            return text;
        },
        MergeTable: function (myself) {
            var mine = this;
            var totalwidth = $(this.elemID + ' thead').css('width');
            var tblwidth = parseInt(totalwidth.split('px')[0]);
            var thIndex = [];
            var merge = 0;
            var start = 0;
            var tblID = '#' + $(myself).attr('id') + ' th';
            $('#hdnTableName').val('_' + $(myself).attr('id'));
            var newWidth = 0;
            $(tblID).each(function (index, value) {
                if ($(this).hasClass('sfActive')) {
                    $(this).addClass('merge');
                    var sfClass = $(this).attr('class').replace('merge', '').replace('sfActive', '').trim();
                    newWidth += parseInt(sfClass.split('_')[1]);
                }
            });
            var temp = 0,
                temp2 = 0;
            var count = 0;
            var mergeMe;
            $(tblID).each(function (index, value) {
                if ($(this).hasClass('merge')) {
                    if (count > 0) {
                        temp2 = parseInt($(this).css('width').split('px')[0]);
                        $(this).remove();
                        mergeMe.removeAttr('class').addClass('sfCol_' + newWidth);
                    }
                    temp = parseInt($(this).css('width').split('px')[0]);
                    mergeMe = $(this);
                    count++;
                }
            });
            $(tblID).removeClass('merge');
            if (count > 1) {
                mine.SimpleResizableTable(myself);
            }
            $("#btnMerge").attr("disabled", "disabled");
        },
        CalculateWidth: function () {
            var totalwidth = $(this.elemID + ' thead').css('width');
            var tblwidth = parseInt(totalwidth.split('px')[0]);
            var info = '';
            var placeHolderName = [];
            var placeholderWidth = [];
            $(this.elemID + ' thead  tr:first th').each(function () {
                var tdwidth = $(this).css('width');
                tdwidth = parseInt(tdwidth.split('px')[0]);
                var percentage = Math.round((tdwidth / tblwidth) * 100);
                var name = $(this).text();
                info += name + ' = ' + percentage + '% <br />';
                placeHolderName.push(name);
                placeholderWidth.push(percentage);
            });
            var divID = '';
            divID = '#divPlaceHolder' + $('#hdnTableName').val();
            var paneName = divID.split('_')[1];
            paneName = paneName.split('sf')[1];
            $('#divwidthInfo').html(info);
            placeHolderName = placeHolderName.join(',');
            placeholderWidth = placeholderWidth.join(',');
            var holder = '';
            holder += '<placeholder name="' + paneName + '" width="' + placeholderWidth + '">' + placeHolderName + '</placeholder>';
            var divID = '';
            divID = '#divPlaceHolder' + $('#hdnTableName').val();
            $(divID).text(holder);
        },
        ColsList: function () {
            var info = '';
            $(this.elemID + ' thead  tr th').each(function () {
                var name = $(this).text();
                info += '<span>' + name + '</span>';
            });
            $('#divColsInfo').html(info);
        },
        ResetTableSizes: function (table, change, columnIndex) {
            //calculate new width;
            var tblID = '_' + table.attr('id');
            $('#hdnTableName').val(tblID);
            var tableId = table.attr('id');
            var myWidth = $('#' + tableId + ' TR TH').get(columnIndex).offsetWidth;
            var myWidthafter = $('#' + tableId + ' TR TH').get(columnIndex + 1).offsetWidth;
            var newWidth = (myWidth + change) + 'px';
            var newWidthafter = (myWidthafter - change) + 'px';
            $('#' + tableId + ' TR').each(function () {
                $(this).find('TD').eq(columnIndex).css('width', newWidth);
                $(this).find('TH').eq(columnIndex).css('width', newWidth);
                $(this).find('TD').eq(columnIndex + 1).css('width', newWidthafter);
                $(this).find('TH').eq(columnIndex + 1).css('width', newWidthafter);
            });
            this.ResetSliderPositions(table);
        },
        ResetSliderPositions: function (table) {
            var tableId = table.attr('id');
            table.find(' TR:first TH').each(function (index) {
                var td = $(this);
                var newSliderPosition = td.position().left + td.outerWidth();
                $("#" + tableId + "_id" + (index + 1)).css({
                    left: newSliderPosition,
                    height: table.height() + 'px'
                });
            });
            this.CalculateWidth();
            this.ColsList();
        },
        MakeResizable: function (table) {
            var mine = this;
            //get number of columns
            var numberOfColumns = table.find('TR:first TH').size();
            //id is needed to create id's for the draghandles
            var tableId = table.attr('id');
            $('.srt-draghandle.' + tableId + '_' + tableId).remove();
            for (var i = 0; i <= numberOfColumns; i++) {
                if ($('#' + tableId + '_id' + i).length == 0) {
                    $('<div class="srt-draghandle ' + tableId + '_' + tableId + ' " id="' + tableId + '_id' + i + '"></div>').insertBefore(table).data('tableid', tableId).data('myindex', i).draggable({
                        axis: "x",
                        start: function () {
                            var tableId = ($(this).data('tableid'));
                            $(this).toggleClass("dragged");
                            //set the height of the draghandle to the current height of the table, to get the vertical ruler
                            $(this).css({
                                height: $('#' + tableId).height() + 'px'
                            });
                        },
                        stop: function (event, ui) {
                            var tableId = ($(this).data('tableid'));
                            $(this).toggleClass("dragged");
                            var oldPos = ($(this).data("draggable").originalPosition.left);
                            var newPos = ui.position.left;
                            var index = $(this).data("myindex");
                            mine.ResetTableSizes($('#' + tableId), newPos - oldPos, index - 1);
                        }
                    });;
                }
            };
            mine.ResetSliderPositions(table);
            return table;
        },
        SimpleResizableTable: function (myself) {
            $("<style type='text/css'> .srt-draghandle.dragged{border-left: 1px solid #333;}</style>").appendTo("head");
            $("<style type='text/css'> .srt-draghandle{ position: absolute; z-index:5; width:5px; cursor:e-resize;}</style>").appendTo("head");
            this.MakeResizable($(myself));
        }
    }
    Plugin.defaults = Plugin.prototype.defaults;
    $.fn.resize = function (options) {
        return this.each(function () {
            new Plugin(this, options).init();
        });
    };
})(jQuery, window, document);