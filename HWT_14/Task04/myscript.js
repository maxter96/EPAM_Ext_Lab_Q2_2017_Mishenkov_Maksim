$(function()
{
    var pages = ["page1", "page2", "page3", "page4", "page5"];
    var currentPage = ParseName(location.pathname);
    var isPaused = false;
    var delay = 3000;
    var $timer = $("#timer");
    var interval = setInterval(function () { Tick(); }, 100);

    $("#prevBtn").click(function () { PrevPage(); });
    $("#nextBtn").click(function () { NextPage(); });
    $("#pauseBtn").click(function () { SetPause(); });

    function PrevPage()
    {
        var index = pages.indexOf(currentPage);

        if (index > 0)
        {
            location.assign(pages[index - 1] + ".html");
        }
    }

    function NextPage()
    {
        var index = pages.indexOf(currentPage);

        if (index != pages.length - 1)
        {
            location.assign(pages[index + 1] + ".html");
        }
        else
        {
            if (confirm("Повторить пролистывание?"))
            {
                location.assign(pages[0] + ".html");
            }
            else
            {
		window.close();
            }
        }
    }

    function SetPause()
    {
        if (isPaused)
        {
            $("#pauseBtn").val("||");
            interval = setInterval(function () { Tick(); }, 100);
        }
        else
        {
            $("#pauseBtn").val(">");
            clearInterval(interval);
        }

        isPaused = !isPaused;
    }

    function Tick()
    {
        if (delay === 0)
        {
            clearInterval(interval);
            return;
        }

        delay -= 100;
        $timer.text(ParseTime(delay));

        if (delay === 0)
        {
            clearInterval(interval);
            NextPage();
        }
    }

    function ParseName(url)
    {
        return url.split("/").pop().split(".").shift();
    }

    function ParseTime(time)
    {
        var seconds = Math.floor(time / 1000);
        var hundredth = Math.floor((time - seconds * 1000) / 10);
        var result = "";

        if (seconds < 10)
        {
            result += "0";
        }

        result += seconds + ":";

        if (hundredth < 10)
        {
            result += "0";
        }

        return result + hundredth;
    }
});