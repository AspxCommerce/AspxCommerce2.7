function encHTML(str) {
    return str.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

(function($) {
    $.debug = {
        dump: function(arr, level, enc) {
            var dumped_text = "";
            if (!level) level = 0;
            var level_padding = "";
            for (var j = 0; j < level + 1; j++) level_padding += "    ";
            if (typeof (arr) == 'object') { //Array/Hashes/Objects
                for (var item in arr) {
                    var value = arr[item];

                    if (typeof (value) == 'object') { //If it is an array,
                        dumped_text += level_padding + "'" + item + "' ...\n";
                        dumped_text += $.debug.dump(value, level + 1);
                    } else if (typeof (value) == 'string') {
                        value = enc == true ? encHTML(value) : value;
                        dumped_text += level_padding + "'" + item + "' => \"" + value + "\"\n";
                    } else {
                        dumped_text += level_padding + "'" + item + "' => \"" + value + "\"\n";
                    }
                }
            } else { //Stings/Chars/Numbers etc.
                dumped_text = "===>" + arr + "<===(" + typeof (arr) + ")";
            }
            return dumped_text;
        },
        print_r: function(obj, contId) {
            $("#" + contId).removeClass().css({
                display: "block",
                position: "absolute",
                top: "0px",
                right: "0px",
                padding: "10px",
                width: "700px",
                height: "auto",
                background: "#ddd",
                color: "black",
                border: "solid 1px black",
                zIndex: 1000
            }).html("<pre>" + $.debug.dump(obj) + "</pre><div id='close-debug'>Close</div>");

            $("#close-debug").css({ cursor: "pointer" }).click(function() {
                $("#" + contId).remove();
            });
        }
    };
})(jQuery);