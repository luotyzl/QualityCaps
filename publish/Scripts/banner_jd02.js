$(document).ready(function () {

    var img_length89 = $(".imgBox img").length; //获取图片张数


    /////////////////////请根据具体情况修改这里的参数//////////////////////////////////////////////////////////////////////////////////////////////////////////


    //参数初始化设置
    img_animate(1000, img_length89, 3000, 'ddd'); //////依次为动画区域的宽度，一共有几张图片，图片切换间隔几秒（1000为一秒），图片下方的小按钮获得焦点时的类名(这个类名修改后，须在CSS文件中修改相应的CSS设置，建议不修改)。


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    var img_number = 1;  //图片当前处于第几张

    function next_btn(img_width, img_quantity, pic_btn_name) {///////下一张、、、、、、、三个参数分别是图片宽度，图片总数量，跟图片相对应的小按钮的类名。


        $(".pic_btn_list li").removeClass(pic_btn_name); //去掉类名为.pic_btn_list元素下的li标签的pic_btn_name类名;
        $(".pic_btn_list li:eq(" + img_number + ")").addClass(pic_btn_name); //给下一张图片相对应的li标签小按钮设置一个类名pic_btn_name，因为img_number从1开始，而li标签的遍历是从0开始。


        if (img_number == img_quantity) {//如果已经是最大数（指最后一张图片）

            $(".pic_btn_list li:eq(0)").addClass(pic_btn_name); //给第一个li标签设置类名。



            //////执行相应的动画

            $(".imgBox img").animate({ left: "+=" + 10 + "px" }, 100); $(".imgBox img").animate({ left: "+=" + (img_width * (img_quantity - 1) - 15) + "px" }, 400); $(".imgBox img").animate({ left: "+=" + 5 + "px" }, 400); img_number = 1;

        } else { $(".imgBox img").animate({ left: "-=" + 10 + "px" }, 100); $(".imgBox img").animate({ left: "-=" + (img_width - 15) + "px" }, 300); $(".imgBox img").animate({ left: "-=" + 5 + "px" }, 400); img_number += 1; }


    };





    function prev_btn(img_width, img_quantity, pic_btn_name) {//上一张


        $(".pic_btn_list li").removeClass(pic_btn_name);
        $(".pic_btn_list li:eq(" + (img_number - 2) + ")").addClass(pic_btn_name);





        if (img_number == 1) {//如果已经是第一张

            $(".pic_btn_list li:eq(" + (img_quantity - 1) + ")").addClass(pic_btn_name);

            $(".imgBox img").animate({ left: "-=" + 10 + "px" }, 100); $(".imgBox img").animate({ left: "-=" + (img_width * (img_quantity - 1) - 15) + "px" }, 400); $(".imgBox img").animate({ left: "-=" + 5 + "px" }, 400); img_number = img_quantity;

        } else { $(".imgBox img").animate({ left: "+=" + 10 + "px" }, 100); $(".imgBox img").animate({ left: "+=" + (img_width - 15) + "px" }, 300); $(".imgBox img").animate({ left: "+=" + 5 + "px" }, 400); img_number -= 1; }




    }



    function img_animate(a, b, c, d) {

        $(".play_box").css({ "width": a, "overflow": "hidden" }); ////动画盒子的宽度是图片的宽度

        $(".play_box .imgBox img ").css({ "width": a }); 


        var img_height89 = $(".play_box .imgBox img ").height();

        $(".play_box .imgBox").css({ "height": img_height89 });

        $(".play_box .prev,.play_box .next").css({ "top": img_height89 / 2 - 20 }); ///动态设置上一张，下一张按钮距离顶部的距离


        //写入图片下方的小按钮，有多少张图片就写入多少个
        $(".imgBox").after("<ul class='pic_btn_list'></ul>");

        for (i = 1; i <= b; i++) {

            $(".pic_btn_list").append("<li>" + i + "</li>");

        }


        $(".pic_btn_list").css({ "left": a / 2 - 75, "margin": 0 }); //动态设置图片下方的小按钮的位置

        $(".pic_btn_list li:eq(0)").addClass(d);




        //下一张
        $(".next").click(function () {



            clearInterval(_h); //清除自动播放

            next_btn(a, b, d); //执行next_btn
            play();            //恢复自动播放


        })


        //上一张


        $(".prev").click(function () {

            clearInterval(_h);
            prev_btn(a, b, d);
            play();


        })


        //自动播放
        function play() {
            _h = setInterval(function () { next_btn(a, b, d) }, c);



        }

        play();

        //鼠标悬停事件
        $(".imgBox img").hover(function () { clearInterval(_h); $(".prev,.next").show(); }, function () { $(".prev,.next").hide(); play(); })

        $(".prev,.next").hover(function () { $(this).show(); }, function () { $(this).hide(); });

        //小按钮点击事件
        $(".pic_btn_list li").click(function () {

            clearInterval(_h);

            $(this).siblings("li").removeClass(d); $(this).addClass(d);

            var pic_btn_number = $(this).text();
            var pic_btn_number = parseInt(pic_btn_number);  //计算出点击的是第几个按钮

            if (pic_btn_number < img_number) { //如果点击的小按钮位于 当前图片相对应的小按钮的前面（左边）
                var bb = img_number - pic_btn_number;  //取得它们之间相隔几个按钮

                //执行相应的动画
                $(".imgBox img").animate({ left: "+=" + 10 + "px" }, 100); $(".imgBox img").animate({ left: "+=" + (a * bb - 15) + "px" }, 400); $(".imgBox img").animate({ left: "+=" + 5 + "px" }, 400);



            } else if (pic_btn_number > img_number) { //如果点击的小按钮位于当前图片相对应的小按钮的后面(右边)
                var bb = pic_btn_number - img_number; //取得它们之间相隔几个按钮

                //执行相应的动画
                $(".imgBox img").animate({ left: "-=" + 10 + "px" }, 100); $(".imgBox img").animate({ left: "-=" + (a * bb - 15) + "px" }, 400); $(".imgBox img").animate({ left: "-=" + 5 + "px" }, 400);

            }

            img_number = pic_btn_number; //改变img_number的值

            play(); //恢复自动播放


        });




    }



});