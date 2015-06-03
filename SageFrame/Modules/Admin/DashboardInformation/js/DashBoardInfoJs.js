////////// SageFrame Take A Tour 

// Here the tourTimeOut varible is taken as global and assign all the setTimeout Methods Used to Synchonize the Animation to it
// so that clearSetTimeOut method can easily destroy the SetTimeout Method at any Context being proccessed...

$(function () {
    var windowWidth;
    var windowHeight;
    var resize = 0;
    var tourTimeOut;
    var tourIdList = 0;
    var tourHide = 0;
    var tourInitiate = 0;

    function getdocDimension() {
        windowWidth = $(document).width();
        windowHeight = $(document).height();
    }

    //-- Method for the outer Contents
    function tourContentNext(contentid) {
        $('.portableTourDiv').show();

        if (resize == 0) {
            getdocDimension(); // for normal condition

        }
        else {
            windowWidth = $(window).width();
            windowHeight = $(document).height(); // for window resize
        }

        var tourContentID = $(contentid);
        getTourContent(tourContentID);
        changeTourMessage();
        resize = 0;
    }

    //-- Method to Find the Specific Content dimension from the tourContentID Array and call drawTourDivMatrix Method 
    // which create 8 Dark Div and 1 white div representing content to desribe at a time 


    function getTourContent(tourContentID) {
        x = tourContentID.offset();
        divTourHeight = tourContentID.outerHeight();
        divTourWidth = tourContentID.outerWidth();

        divTourTop = x.top;
        divTourLeft = x.left;
        drawTourDivMatrix(divTourHeight, divTourWidth, divTourTop, divTourLeft);

        $('html, body').animate({ scrollTop: divTourTop - 50 }, 900);

        //message at top right
        //if (tourIdList == 8 || tourIdList == 10) {
        if (tourIdList == 8 ) {
            $(".tourMsg").animate({

                "top": divTourTop + (divTourHeight / 2) - 60,
                "left": divTourLeft + divTourWidth
            }, 1000);
        }
        // for message position at the top left

        else if (tourIdList == 9 || tourIdList == 10) {
            $(".tourMsg").animate({

                "top": divTourTop + (divTourHeight / 2) - 60,
                "left": divTourLeft - 290
            }, 1000);
        }

        else if (tourIdList == 0) {  // for message position  at Center
            $(".tourMsg").animate({
                "top": divTourTop + (divTourHeight),
                "left": divTourLeft + (divTourWidth / 2) - 125

            }, 1000);

        }

        // $(window).scrollTop(tourContentID.position().top - 50);
        else if (divTourLeft >= (windowWidth / 2) && tourIdList != 11) {  // condition for the message position to the left
            $(".tourMsg").animate({
                "top": divTourTop + (divTourHeight - 20),
                "left": divTourLeft - 290

            }, 1000);

        }

        else if ((windowWidth / 2) <= divTourLeft + (divTourWidth / 2) && divTourWidth >= (windowWidth / 2)) { // for message position  at Center
            $(".tourMsg").animate({
                "top": divTourTop + (divTourHeight),
                "left": divTourLeft + (divTourWidth / 2) - 125

            }, 1000);
        }
        else {
            $(".tourMsg").animate({ // message position at the right

                "top": divTourTop + (divTourHeight - 20),
                "left": divTourLeft + divTourWidth
            }, 1000);


            //        

        }
        //        else if (windowHeight / 2 < divTourTop && divTourLeft >= (windowWidth / 2)) // for message position at the top left
        //        {
        //            $(".tourMsg").animate({

        //                "top": divTourTop + (divTourHeight / 2) - 60,
        //                "left": divTourLeft - 280
        //            }, 1000);
        //        }
        //        else if (windowHeight / 2 < divTourTop) // for message position at the top right
        //        {
        //            $(".tourMsg").animate({

        //                "top": divTourTop + (divTourHeight / 2) - 60,
        //                "left": divTourLeft + divTourWidth
        //            }, 1000);

        //        }



    }

    //-- Method to draw Div Matrix (3*3)
    function drawTourDivMatrix(divTourHeight, divTourWidth, divTourTop, divTourLeft) {

        $("#div_1").css({ "top": "0px", "left": "0px", "height": divTourTop, "width": divTourLeft });
        $("#div_2").css({ "top": "0px", "left": divTourLeft, "height": divTourTop, "width": divTourWidth });
        $("#div_3").css({ "top": "0px", "left": divTourLeft + divTourWidth, "height": divTourTop, "width": windowWidth - (divTourLeft + divTourWidth) });
        $("#div_4").css({ "top": divTourTop, "left": "0px", "height": divTourHeight, "width": divTourLeft });

        $("#div_5").addClass("div_5");

        $("#div_5").animate({ "background-color": "rgba(255,255,255,0.2)", "opactiy": "1" }, 600);
        $("#div_5").css({ "top": divTourTop, "left": divTourLeft, "height": divTourHeight, "width": divTourWidth });
        $("#div_6").css({ "top": divTourTop, "left": divTourLeft + divTourWidth, "height": divTourHeight, "width": windowWidth - (divTourLeft + divTourWidth) });
        $("#div_7").css({ "top": divTourTop + divTourHeight, "left": "0px", "height": windowHeight - (divTourTop + divTourHeight), "width": divTourLeft });
        $("#div_8").css({ "top": divTourTop + divTourHeight, "left": divTourLeft, "height": windowHeight - (divTourTop + divTourHeight), "width": divTourWidth });
        $("#div_9").css({ "top": divTourTop + divTourHeight, "left": divTourLeft + divTourWidth, "height": windowHeight - (divTourTop + divTourHeight), "width": windowWidth - (divTourLeft + divTourWidth) });
    };

    // Method to Load the Specific Message from the tourMessageList Array
    function changeTourMessage() {

        $('#paraTourMsg').html(tourMessageList[tourIdList]);
        CreateEvent();
    }
    //Method to load Message for the Menu Tour
    function msgMenu(message, pos) {

        $(".tourMsg").animate({
            "top": divTourTop + 40 + pos,
            "left": divTourLeft + divTourWidth
        }, 1000);
        $('#paraTourMsg').html(message);


    }
    // Hide the Next and Previous Button
    function HideDirectionBtn() {
        $('#btnSfTourNext').hide();
        $('#btnSfTourPrev').hide();
    }
    // Show Next And Previous Button
    function ShowDirectionBtn() {
        $('#btnSfTourNext').show();
        $('#btnSfTourPrev').show();
    }

    // Method for Creating Event To handle the Tour of Inner Contents 

    function CreateEvent() {
        switch (tourIdList) {

            /* // Top Sticky Bar  

            case 0:
            HideDirectionBtn();

                
            $('#paraTourMsg').html("TopSticky barr !!!");
            tourTimeOut = setTimeout(function () {
            $('#btnSfTourNext').show();
                   
            var topStickyTourList = new Array(".left", ".templateChange", ".right");
            var topStickyTourMsg = new Array("logo", "Change the Theme", "status");
            var topTourInnerID = 0;
            var innerContent;
            topStickyFirstLoop(topTourInnerID);

            //Method to Tour the Inner Tutorial Contents
            function topStickyContent(topTourInnerID) {
            innerContent = $(topStickyTourList[topTourInnerID]);
            getTourContent(topTourInnerID);
            $('#paraTourMsg').html(topStickyTourMsg[topTourInnerID]);
            topTourInnerID++;
            }

            // Method to direct load the first content 
            function topStickyFirstLoop(topTourInnerID) {
            topStickyContent(tutIntopTourInnerIDnerID);
            topStickyLoop(topTourInnerID);
            }
                    
            // Method to load the later content after 3 Second
            function topStickyLoop(topTourInnerID) {

            if (topTourInnerID < topStickyTourList.length) {
            if(topTourInnerID = 1 && $('#templateChangeWrapper').hasClass('Off')) {
            $('.sfMiddle').trigger('click');
            }
            tourTimeOut = setInterval(function () {
            topStickyContent(topTourInnerID);
            }, 3000);
            }
            else {
            tourTimeOut = setTimeout(function () {

            return false;
            }, 1000);
            }
            }
            }, 3000); */ 


            //Menu                                                                                                                                        
            case 4:
                $('body').off('click', '#btnSfTourMenu').on('click', '#btnSfTourMenu', function () {

                    tourHide = 0;
                    $('.Grandparent').find('a').eq(0).trigger('click');

                    tourTimeOut = setTimeout(function () {
                        tourContentNext(".menu");
                        $('#paraTourMsg').html(' While In Collapse Mode Hover the Portal Management to Get Menu List');
                        $('#btnSfTourMenu').remove();
                        $('.Grandparent').find('a').eq(0).trigger('hover');
                        return false;
                    }, 300);

                    //////////////////

                    //The  Method Commented below can be used to carry out the Animation describing each Menu items
                    // On the Portal Management at the leftcorner of Dasboard

                    /////////////////

                    // Animating the Menu list Messages
                    // Timeout Event to Sync the animated Messages

                    /*  setTimeout(function () {
                    tourTimeOut = tourContentNext(".menu");
                    $('#paraTourMsg').html("This is Menu List !!!");
                    tourTimeOut = setTimeout(function () {
                    //$('.parent').find('a').eq(0).trigger('click');
                    tourTimeOut = setTimeout(function () {
                    var menuMsg = new Array("user account", "manage site", "setting", "tools", "system", "portal", "site analytics"),
                    i = 0;
                    pos = 0;
                    msgloop(i);
                    function msgloop(i) {
                    if (i < menuMsg.length) {

                    tourTimeOut = setTimeout(function () {
                    pos = pos + 45;
                    msgMenu(menuMsg[i], pos);
                    i++;
                    msgloop(i);
                    }, 2000);
                    }
                    else {
                    tourTimeOut = setTimeout(function () {
                    $('.Grandparent').find('a').eq(0).trigger('click');
                    setTimeout(function () {
                    tourContentNext(".menu");
                    return false;
                    }, 300);


                    }, 2000);
                    }
                    }
                    }, 500);
                    }, 500);
                    }, 500);*/

                });
                break;

            // Side Bar Hide Pannel                                                                                                                                        
            case 5:

                $('body').off('click', '#btnSfTourHide').on('click', '#btnSfTourHide', function () {
                    tourHide = 1;
                    $('#btnSfTourHide').remove();
                    HideDirectionBtn();
                    if ($('.sfHidepanel').find('i').hasClass('sidebarExpand')) {

                        $('.sfHidepanel').trigger('click');
                        $("#div_5").removeClass("div_5");
                        $("#div_5").animate({ "background-color": "black", "opacity": "0.5" }, 120);
                    }
                    tourTimeOut = setTimeout(function () {
                        tourContentNext(".sidebarCollapse");
                        $("#btnSfTourHide").remove();
                        ShowDirectionBtn();
                    }, 500);
                });
                break;

            // SageFrame Theme                                                                                                                                        
            /* case 4:

            $(document).on('click', '#btnSfTourTheme', function () {
            $('.sfMiddle').trigger('click');
            $('#btnSfTourTheme').remove();
            tourTimeOut = setTimeout(function () {
            tourContentNext(".sfMiddle");
            }, 300);

            });

            break; */ 

            // Page Help                                                                                                     
            case 3:
                $('body').off('click', '#btnSfTourHelp').on('click', '#btnSfTourHelp', function () {
                    $('#btnSfTourHelp').remove();
                    if ($('.pageHelp').hasClass('off')) {
                        $('.pageHelp').trigger('click');
                        $('#ui-id-1').trigger('click');
                    }
                    timeOut = setTimeout(function () {
                        tourContentNext('.sfPageHelpHolder');
                        $('#paraTourMsg').html("Page Help is available for each and every page to guide you through the site.");
                    }, 600);
                    //The Commented method below can be used to animate the Inner Content of Page Help
                    /*
                    tourTimeOut = setTimeout(function () {
                    var pageHelpMsg = new Array("", "admin help 1", "admin help 2", "admin help 3");
                    var i = 1;
                    pageHelpLoop(i);
                    function pageHelpLoop(i) {

                    if (i < pageHelpMsg.length) {
                    tourTimeOut = setTimeout(function () { // Timeout Event to sync the messages
                    $('#ui-id-' + i).trigger('click');
                    tourContentNext('#ui-id-' + i);
                    $('#paraTourMsg').html(pageHelpMsg[i]);
                    i++;
                    pageHelpLoop(i);
                    }, 2000);
                    }
                    }
                    }, 500); */
                });
                break;
            /*
            //SageFrame Tutorial                                                    
            case 4:
            HideDirectionBtn();
            $('#paraTourMsg').html("Sageframe tutorials are available here.");
            tourTimeOut = setTimeout(function () {
            $('#btnSfTourNext').show();
            $('#btnSfTourPrev').show();
            var tutorialList = new Array(".sfTutorialA", ".sfTutorialB", ".sfTutorialC", ".sfModuleList");
            var tutorialMsg = new Array("Take a Tour", "Video Tutorial", "Document Library", "Module List");
            var tutInnerID = 0;
            var innerContent;
            tutorialFirstLoop(tutInnerID);

            //Method to Tour the Inner Tutorial Contents
            function tutContent(innerContent) {
            innerContent = $(tutorialList[tutInnerID]);
            getTourContent(innerContent);
            $('#paraTourMsg').html(tutorialMsg[tutInnerID]);
            tutInnerID++;
            }

            // Method to direct load the first content 
            function tutorialFirstLoop(tutInnerID) {
            tutContent(tutInnerID);
            tutorialLoop(tutInnerID);
            }

            // Method to load the later content after 3 Second
            function tutorialLoop(tutInnerID) {
            if (tutInnerID < tutorialList.length) {
            tourTimeOut = setInterval(function () {
            tutContent(tutInnerID);
            }, 3000);
            }
            else {
            tourTimeOut = setTimeout(function () {

            return false;
            }, 1000);
            }
            }
            }, 3000);

            break;
            */ 
            /*
            // DashBoard Content                                                                                           
            case 7:
            HideDirectionBtn();
            var innerContentID;
            var msgLoop;
            //hide the play, pause and Back Button
            function HideInnerControlBtn() {
            $('#btnTourBack').hide();
            $('#btnTourPlay').hide();
            $('#btnTourPause').hide();
            }
            //// Messages to Display
            tourTimeOut = setTimeout(function () {

            var dashInnerID = 0;
            var Msg = new Array(
            "Roles: Manages the default roles and even create new ones.",
            "Users: Allows to manage the users.",
            "Modules: Create new and composite modules.",
            "Module Message: Provides an information bar when a new page is opened.",
            "Menu: You can add, remove and manage different types of menus.",
            "Pages: Create and manage pages for the site.",
            "Templates: You can install or create new templates.",
            "Files: Allows to manage files and folders. ",
            "Site Analytics: Tracks the site traffic based on country, page or browser visit.",
            "Settings: Site admin can configure basic, advanced and superuser settings.",
            "Portals: Allows to create new portals and manage the existing ones.",
            "Links: Manage the frequently visited pages and modules on the dashboard.",
            "EO: Allows you to generate Google Analytics, Sitemaps and Robots.",
            "Lists: You can maintain the records in list format.",
            "Localization: Maintains the site content according to the location and preferable language.",
            "Message Template: Provides facilities for management of emails and messages boards and their messages.",
            "SQL: Executes the SQL queries against the server database.",
            "Event Log: Updates all the events in the site.",
            "System Event StartUp: You can make any page as your startup page.",
            "Cache Maintenance: Sets the cache priorities and maintains cache performance",
            "Scheduler: Schedules the task to run on a predefined time.",
            "CDN: Adds JS and CSS URL to maintain high availability and performance.");

            firstDashMsgloop(dashInnerID);
            var pauseCount;

            // method to Tour the DashBoard Inner Content
            function dashInnerContent(dashInnerID) {
            innerContentID = $('.sfDashboard li').eq(dashInnerID);
            getTourContent(innerContentID);
            $('#paraTourMsg').html(Msg[dashInnerID] + '<button id="btnTourBack" style="display:none" class="sfBtn">Back</button><button id="btnTourPause" class="sfBtn">pause</button><button id="btnTourPlay" style="display:none" class="sfBtn">play</button>');
            if (dashInnerID == 21) {
            HideInnerControlBtn();
            $('#btnTourBack').show();
            }

            }
            // method to load the First Inner DashBoard Content
            function firstDashMsgloop(dashInnerID) {

            dashInnerContent(dashInnerID);
            dashInnerID++;
            LaterDashMsgloop(dashInnerID);
            pauseCount = dashInnerID;
            }
            // method to load the later Inner DashBoard Content each after 3 second
            function LaterDashMsgloop(dashInnerID) {
            $('#btnSfTourNext').show();
            $('#btnSfTourPrev').show();
            if (dashInnerID < Msg.length) {
            tourTimeOut = setInterval(function () {
            dashInnerContent(dashInnerID);
            dashInnerID++;
            pauseCount = dashInnerID;
            }, 3000);
            }
            else {
            return false;
            }
            }

            $('body').off('click', '#btnTourPause').on('click', '#btnTourPause', function () {
            clearInterval(tourTimeOut);
            HideInnerControlBtn();
            $('#btnTourBack').show();
            $('#btnTourPlay').show();
            });
            $('body').off('click', '#btnTourPlay').on('click', '#btnTourPlay', function () {
            firstDashMsgloop(pauseCount);
            HideInnerControlBtn();
            $('#btnTourPause').show();
            });
            $('body').off('click', '#btnTourBack').on('click', '#btnTourBack', function () {
            if (pauseCount != 1) {
            HideInnerControlBtn();
            $('#btnTourPause').show();
            firstDashMsgloop(pauseCount - 2);
            }
            else {
            return false;
            }
            });
            }, 3000);

            break;

            */ 
        }
    }

    // array of ID OR Class of the Content to Tour
   /* var tourContentID = new Array("#divAdminControlPanel", ".left", ".templateChange", ".right", ".menu", ".sidebarExpand ", ".pageHelp", ".sfWelcomeWrap", "#sfToppane .sfModulecontent",
                                  "#sfTopfivecountry", "#_udp__11 ", "#divRight"),*/
    var tourContentID = new Array("#divAdminControlPanel", ".left", ".templateChange", ".right", ".menu", ".sidebarExpand ", ".pageHelp", ".sfWelcomeWrap", "#sfToppane .sfModulecontent",
                                 "#sfTopfivecountry",  "#divRight"),

    // Message to Display for Each Content to Tour
     tourMessageList = new Array(
                            "<span class='sfTrHeading'>Welcome to SageFrame tour.</span>This is the Top Sticky Bar which contains the SageEver logo and its version description. Along with that you can access the UI (User Interface) setting, Preview and User Profile.",
                            "Here, you can see SageFrame logo and the version you are using.",
                            "Click here to change the dashboard theme and sidebar position from the available options.",
                            "This section contains the Dashboard home link, site Preview, user details and settings, and logout menu.",
							 "<span class='sfTrHeading'>Sidebar Menu</span>This is a collapsible side bar menu which contains all the necessary controls to build and customize your site.<br/><br/><button id='btnSfTourMenu' class='sfBtn' >Collapse</button>",
                           "Click on the arrow to hide/unhide the side bar menu.<br/><br/><button id='btnSfTourHide' class='sfBtn' >Hide</button>",
						   "<span class='sfTrHeading'>Page Help</span>To know what the respective pages do, click on the Page Help. This page help option is available for every pages included on the dashboard.<br/><br/><button id='btnSfTourHelp' class='sfBtn' >Expand</button>",
                            "<span class='sfTrHeading'>SageFrame Welcome Screen</span>You can take a short tour to SageFrame, watch video tutorials, read user manuals and get an insight of available modules.",
                            "<span class='sfTrHeading'>C-Panel</span>This is your C-Panel for easy access of frequently used controls available in the system.",
                            "<span class='sfTrHeading'>Site Analytics</span>The Visitor’s List gives you the site’s data and web usage in graphical form. You can however see full statistics from the link given.",
                            //"<span class='sfTrHeading'>Portal Snapshot</span>Get each and every information of pages, modules and users with the Portal Snapshot.",
                            "<span class='sfTrHeading'>To-Do-List</span>Make a note of the tasks to be done as well as see the previous, present and future tasks list."),
   prevTourBtnClick = 0;
    tourIdList = 0;

    tourIdLength = tourContentID.length;

    //Destroy Tour -- Removes all The Apended Nine Divs and TourMsg Div from the body 

    function DestroyTour() {
        clearTimeout(tourTimeOut);
        clearInterval(tourTimeOut);
        $('#divTourWrapper').remove();
        $('html, body').animate({ scrollTop: 0 }, 1000);
        tourIdList = 0;

    }

    //Skip Take A Tour !!
    $('body').on('click', '#btnSfTourSkip', function () {
        DestroyTour();
    });

    // Tour Initiating  Click Event
    $('#sfTakeTour').off('click').on('click', function () {

        tourIdList = 0;
        $('html, body').scrollTop(0);
        if ($('.sfHidepanel').find('i').hasClass('sidebarCollapse')) {
            $('.sfHidepanel').trigger('click');
        }
        if (!$('.Grandparent').find('a').eq(0).hasClass('active')) {
            $('.Grandparent').find('a').eq(0).trigger('click');
        }
        if ($('#templateChangeWrapper').hasClass('On')) {
            $('.sfMiddle').trigger('click');
        }

        if (!$('.pageHelp').hasClass('off')) {

            $('.pageHelp').trigger('click');

        }
        sfTourBodyHtml();

        tourContentNext(tourContentID[tourIdList]);



    });

    function sfTourBodyHtml() {
        var html = '';
        html += '<div id="divTourWrapper" class="divwrapper">';
        for (var i = 1; i < 10; i++) {
            html += '<div class="portableTourDiv" id="div_' + i + '"></div>';
        }
        html += '<div class="tourMsg"><div id="divTourMsg"><p id="paraTourMsg"> </p></div>';
        html += '<button id="btnSfTourPrev" class="sfBtn icon-arrow-slim-w" style="display:none"></button>';
        html += '<button id="btnSfTourNext" class="sfBtn icon-arrow-slim-e"></button>';
        html += '<i id="btnSfTourSkip" class="icon-close sfDelete"></i></div>';
        html += '</div>';

        $('body').append(html);

    }
    /// Method to Arrange the Content to Tour in the Required States !!!!
    function tourContentArrange(tourIdList) {
        $("#div_5").removeClass("div_5");
        $("#div_5").animate({ "background-color": "black", "opacity": "0.5" }, 500);
        if ($('.sfHidepanel').find('i').hasClass('sidebarCollapse') && (tourIdList == 5 || tourIdList == 4 && tourHide == 1)) {
            $('.Grandparent').find('a').eq(0).trigger('click');
            $('.sfHidepanel').trigger('click');
            tourHide == 0;
        }
        else if ($('.Grandparent').find('a').eq(0).hasClass('') && tourHide == 0) {
            $('.Grandparent').find('a').eq(0).trigger('click');

        }

        if (!$('.pageHelp').hasClass('off') && tourIdList != 4) {

            timeOut = setTimeout(function () {
                $('.pageHelp').trigger('click');
                // timeOut = setTimeout(function () {
                //  $('#divTourWrapper').remove();
                //   sfTourBodyHtml();
                //   $("#btnSfTourPrev").show();
                //    tourContentNext(tourContentID[tourIdList]);

                //   }, 1500);
            }, 100);


        }

        if (tourIdList == 2 && $('#templateChangeWrapper').hasClass('Off')) {

            $('.sfMiddle').trigger('click');
            

        }
        else if (tourIdList != 2 && $('#templateChangeWrapper').hasClass('On')) {
            setTimeout(function () {
               
                $('.sfMiddle').trigger('click');
            }, 600);
        }

    }

    // Next Content Click Event
    $('body').off('click','#btnSfTourNext').on('click', '#btnSfTourNext', function () {
        clearTimeout(tourTimeOut);
        clearInterval(tourTimeOut);
        tourIdList = tourIdList + 1;
        tourContentArrange(tourIdList);

        $('#divSfTourIntro').remove();
        if (tourIdList == 1) {
            $('#btnSfTourPrev').show();
        }
        if (tourIdList < tourIdLength) {

            if (tourIdList == 8) {
                $('#divTourWrapper').remove();
                sfTourBodyHtml();
                $("#btnSfTourPrev").show();
                tourContentNext(tourContentID[tourIdList]);


            }
            else {
                setTimeout(function () {
                    tourContentNext(tourContentID[tourIdList]);
                }, 600);
            }


        } else if (tourIdList == tourIdLength) {
            var scrollHeight = $(window).scrollTop();
            $('.portableTourDiv').hide();
            $('body').append('<div id="divSfTourIntro" class="divSfTourIntro"></div>');
            $("#divSfTourIntro").css({
                "height": windowHeight,
                "width": windowWidth

            });
            $(".tourMsg").animate({
                "top": scrollHeight + 200,
                "left": windowWidth / 2 - 125
            }, "slow");


            $('#paraTourMsg').html("Congratulations!!! Your tour is complete. You are now ready to use Sageframe.<br/><button id='btnSfFinish' class='sfBtn' >Finish</button>");
            $('#btnSfTourNext').hide();
        }
        else if (tourIdList > tourIdLength) {
            DestroyTour()
        }


        prevTourBtnClick = 0;
    });

    // Previous Content Click Event
    $('body').off('click', ".tourMsg #btnSfTourPrev").on('click', ".tourMsg #btnSfTourPrev", function () {

        $('#btnSfTourNext').show();
        clearTimeout(tourTimeOut);
        clearInterval(tourTimeOut);
        $("#divSfTourIntro").remove();
        $('.portableTourDiv').show();
        tourIdList = tourIdList - 1;
        tourContentArrange(tourIdList);
        if (tourIdList == 0) {
            $("#btnSfTourPrev").hide();
        }
        setTimeout(function () {

            tourContentNext(tourContentID[tourIdList]);
        }, 700);
        prevTourBtnClick = 1;
    });

    $('body').on('click', '#btnSfFinish', function () {
        $("#divSfTourIntro").remove();
        DestroyTour();
    });

    // Method to Handle the WIndow Resize
    $(window).resize(function () {
        if (!($.browser.msie)) {

            resize = 1;
            tourContentNext(tourContentID[tourIdList]);
        }


    });

});

