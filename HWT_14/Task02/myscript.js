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
    var outputText = CalculateExpression(inputText);
    var output = document.getElementById("outputData");
    output.textContent = outputText;
}

function CalculateExpression(inputText)
{
    inputText = inputText.replace(/\s/g, "");
    var stringData = inputText.split(/[\+ \- \* \/ =]/g);
    var floatData = [];

    for (var i = 0; i < stringData.length - 1; i++)
    {
        floatData.push(parseFloat(stringData[i]));

        if (isNaN(floatData[i]))
        {
            return "Некорректный ввод!";
        }
    }

    var operations = [].concat(inputText.match(/[\+ \- \* \/ =]/g));
    var result = Calculate(floatData, operations);
    return result;
}

function Calculate(data, operations)
{
    var result = data[0];

    for (var i = 0; i < operations.length; i++)
    {
        switch (operations[i])
        {
            case "=":
                return result.toFixed(2).toString();
                break;
            case "+":
                result += data[i + 1];
                break;
            case "-":
                result -= data[i + 1];
                break;
            case "*":
                result *= data[i + 1];
                break;
            case "/":
                result /= data[i + 1];
                break;
        }
    }

    return "Некорректный ввод!";
}

function ClearFields()
{
    var input = document.getElementById("inputData");
    input.value = "";
    var output = document.getElementById("outputData");
    output.textContent = "";
}