$("img.imggallery").each(function (i, obj) {

        var clone;
        var position;
        clone = $(obj).clone();
        $(clone).addClass("clonedItem");
        

        $(obj).bind("mouseover", function (e) {
            position = $(obj).position();
            $(".clonedItem").animate({
                height: "200px",
                width: "200px"
            }, 200, function () { $(this).remove(); });

            $("img.imggallery").css("z-index", 1);
            $(clone).css("top", position.top).css("left", position.left).css("z-index", 1000);

            $(clone).appendTo("#imageContainer").css("position", "absolute").animate({
                height: "370px",
                width: "370px"
            }, 200, function () {

                $(clone).bind("mouseout", function (e) {
                    $(clone).animate({
                        height: "200px",
                        width: "200px"
                    }, 200, function () { $(clone).remove(); });

                }); // end mouseout

            }); // end animate callback

        }); // end mouseover

    }); // end each