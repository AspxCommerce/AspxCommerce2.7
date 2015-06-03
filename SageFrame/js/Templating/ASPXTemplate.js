
function LoadAllAspxTemplate() {

    $.ajax({
        url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid.htm',
        data: ({}),
        async: false,
        success: function(template) {
            $.template("scriptResultGridTemp", template);
            $.ajax({
                url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultList.htm',
                data: ({}),
                async: false,
                success: function(template1) {
                    $.template("scriptResultListTemp", template1);
                    $.ajax({
                        url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid2.htm',
                        data: ({}),
                        async: false,
                        success: function(template2) {
                            $.template("scriptResultGrid2Temp", template2);
                            $.ajax({
                                url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultGrid3.htm',
                                data: ({}),
                                async: false,
                                success: function(template3) {
                                    $.template("scriptResultGrid3Temp", template3);
                                    $.ajax({
                                        url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptCompactList.txt',
                                        data: ({}),
                                        async: false,
                                        success: function(template4) {
//                                            var tableContent = $("<div></div>").append(template4).find("table > tbody").html();
//                                            var pattern = "%7B", re = new RegExp(pattern, "g");
//                                            var pattern2 = "%7D", re2 = new RegExp(pattern2, "g");
//                                            tableContent = tableContent.replace(re, "{");
//                                            tableContent = tableContent.replace(re2, "}");
                                        $.template("scriptCompactListTemp", template4);
                                            $.ajax({
                                                url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultProductGrid.htm',
                                                data: ({}),
                                                async: false,
                                                success: function(template5) {
                                                    $.template("scriptResultProductGridTemp", template5);
                                                    $.ajax({
                                                        url: aspxRootPath + 'Modules/AspxCommerce/AspxTemplate/scriptResultListWithoutOptions.htm',
                                                        data: ({}),
                                                        async: false,
                                                        success: function(template6) {
                                                            $.template("scriptResultListWithoutOptionsTemp", template6);
                                                        }
                                                    });
                                                }
                                            });
                                        }
                                    });
                                }
                            });

                        }
                    });

                }
            });
        }
    });
}
  