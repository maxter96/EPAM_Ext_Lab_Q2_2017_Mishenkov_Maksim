$(function()
{
    BindButtonsForList("list1");
    BindButtonsForList("list2");
});

function BindButtonsForList(id)
{
    var $list = $("#" + id);
    var $moveLeftBtn = $list.find(".moveLeftBtn");
    var $moveRightBtn = $list.find(".moveRightBtn");
    var $moveAllLeftBtn = $list.find(".moveAllLeftBtn");
    var $moveAllRightBtn = $list.find(".moveAllRightBtn");
    var $leftList = $list.find(".leftList select");
    var $rightList = $list.find(".rightList select");

    $moveRightBtn.click(function ()
    {
        Move($moveRightBtn, $leftList, $rightList);
    });

    $moveLeftBtn.click(function ()
    {
        Move($moveLeftBtn, $rightList, $leftList);
    });

    $moveAllRightBtn.click(function ()
    {
        $leftList.find("option").each(function () { this.selected = true; });
        Move($moveAllRightBtn, $leftList, $rightList);
    });

    $moveAllLeftBtn.click(function ()
    {
        $rightList.find("option").each(function () { this.selected = true; });
        Move($moveAllLeftBtn, $rightList, $leftList);
    });
}

function Move($sender, $from, $to)
{
    var $elements = $from.find("option:selected");

    if ($elements.size() === 0)
    {
        alert("Не выбрано ни одного элемента!");
        return;
    }

    $elements.each(function ()
    {
        var $element = $(this).remove();
        $to.append($element);
    });
}