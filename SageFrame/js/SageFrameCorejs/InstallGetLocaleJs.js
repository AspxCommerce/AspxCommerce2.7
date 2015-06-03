////////////////////Added BY Niranjan ?//////////

function GetSystemLocale(text) {
    return SystemLocale[text];
}

function getLocale(moduleKey, text) {
    return moduleKey[text];
}

$.fn.SystemLocalize = function() {
    return this.each(function() {
        var t = $(this);
        //alert(t.tagName);
        //   if (t.is("a") || t.is("span") || t.is("p") || t.is("div") || t.is("label") || t.is("h1") || t.is("h2") || t.is("h3") || t.is("h4") || t.is("h5") || t.is("h6")) {
        //  alert(p.moduleKey[t.html()]);
        t.html(SystemLocale[t.html()] == undefined ? t.html() : SystemLocale[t.html()]);
        //  }
    });
};


$.fn.localize = function(p) {
    return this.each(function() {
        var t = $(this);
        // if (t.is("a") || t.is("span") || t.is("p") || t.is("div") || t.is("label") || t.is("h1") || t.is("h2") || t.is("h3") || t.is("h4") || t.is("h5") || t.is("h6")) {
        //  alert(p.moduleKey[t.html()]);
        //t.html(p.moduleKey[t.html().replace(/^\s+|\s+$/g,"")] == undefined ? t.html().replace(/^\s+|\s+$/g,"") : p.moduleKey[t.html().replace(/^\s+|\s+$/g,"")]);
        //   }
        t.html(p.moduleKey[t.html().replace(/^\s+|\s+$/g, "")] == undefined ? t.html().replace(/^\s+|\s+$/g, "") : p.moduleKey[t.html().replace(/^\s+|\s+$/g, "")]);
        if (t.is("input[type='button']")) {
            t.val(p.moduleKey[t.attr("value")] == undefined ? t.attr("value") : p.moduleKey[t.attr("value")]);
        }
    });

};

