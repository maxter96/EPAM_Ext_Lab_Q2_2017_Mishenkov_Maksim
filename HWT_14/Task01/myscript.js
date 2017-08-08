window.onload = OnLoad;

function OnLoad()
{
    var button = document.getElementById("procButton");
    button.onclick = ProcessString;
    var clrButton = document.getElementById("clearButton");
    clrButton.onclick = ClearFields;
}

function ProcessString()
{
    var input = document.getElementById("inputData");
    var inputText = input.value;
    var outputText = RemoveDuplicatedSymbols(inputText);
    var output = document.getElementById("outputData");
    output.textContent = outputText;
}

function ClearFields()
{
    var input = document.getElementById("inputData");
    input.value = "";
    var output = document.getElementById("outputData");
    output.textContent = "";
}

function CheckSymbols(symbol, array)
{
    if (array.indexOf(symbol) != -1)
    {
        return true;
    }

    return false;
}

function RemoveSymbols(inputText, symbols)
{
    var result = "";

    for (var i = 0; i < inputText.length; i++) 
    {
        if (!CheckSymbols(inputText[i], symbols))
        {
            result += inputText[i];
        }
    }

    return result;
}

function SplitString(inputText)
{
    var punctuation = ["?", "!", ":", ";", ",", "."];
    inputText = RemoveSymbols(inputText, punctuation);
    var splited = inputText.split(" ");
    var result = [];

    for (var i = 0; i < splited.length; i++)
    {
        if (splited[i] != "")
        {
            result.push(splited[i]);
        }
    }

    return result;
}

function GetDuplicatedSymbols(str, duplicatedSymbols)
{
    var currentSymbols = [];

    for (var i = 0; i < str.length; i++)
    {
        if (currentSymbols.indexOf(str[i]) != -1)
        {
            if (duplicatedSymbols.indexOf(str[i] != -1))
            {
                duplicatedSymbols.push(str[i]);
            }
        }
        else 
        {
            currentSymbols.push(str[i]);
        }
    }

    return duplicatedSymbols;
}

function GetAllDuplicatedSymbols(strArray)
{
    var duplicatedSymbols = [];

    for (var i = 0; i < strArray.length; i++)
    {
        dublicatedSymbols = GetDuplicatedSymbols(strArray[i], duplicatedSymbols);
    }

    return duplicatedSymbols;
}

function RemoveDuplicatedSymbols(inputText)
{
    var strArray = SplitString(inputText);
    var duplicatedSymbols = GetAllDuplicatedSymbols(strArray);
    return RemoveSymbols(inputText, duplicatedSymbols);
}