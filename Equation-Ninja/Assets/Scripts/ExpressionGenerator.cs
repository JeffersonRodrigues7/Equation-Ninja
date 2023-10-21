using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ExpressionGenerator : MonoBehaviour
{
    private string[] operatorsSymbols = { "+", "-", "*" };  // Operadores matem�ticos poss�veis.
    private string[] openedSymbols = { " ", "(" };        // S�mbolos de abertura (par�nteses ou "nada").
    private string[] closedSymbols = { " ", ")" };        // S�mbolos de fechamento (par�nteses ou "nada").

    // Fun��o que gera uma express�o matem�tica aleat�ria com a quantidade especificada de operadores faltantes.
    private string createRandomExpression(int missingOperators, int operators, int minNumberValue, int maxNumberValue, int parentheses)
    {
        if (missingOperators == 0)
        {
            // Se n�o h� mais operadores faltantes, retorna um n�mero aleat�rio entre 1 e 9 como um operando.
            return Random.Range(minNumberValue, maxNumberValue).ToString();
        }
        else
        {
            // Caso contr�rio, gera um s�mbolo de abertura e fechamento (par�nteses ou "nada") aleat�rio.
            int option = Random.Range(0, parentheses);
            string openSymbol = openedSymbols[option];
            string closeSymbol = closedSymbols[option];

            string expression = "";

            // Adiciona o s�mbolo de abertura � express�o.
            expression += openSymbol;

            // Adiciona um n�mero aleat�rio entre 0 e 9 � express�o como um operando.
            expression += Random.Range(minNumberValue, maxNumberValue);

            // Adiciona um operador aleat�rio � express�o.
            expression += operatorsSymbols[Random.Range(0, operators)];

            // Recursivamente, gera a pr�xima parte da express�o com um operador a menos.
            expression += createRandomExpression(missingOperators - 1, operators, minNumberValue, maxNumberValue, parentheses);

            // Adiciona o s�mbolo de fechamento correspondente ao de abertura.
            expression += closeSymbol;

            return expression;
        }
    }

    // Fun��o p�blica que gera e exibe uma express�o aleat�ria com o n�mero de operadores dentro dos limites especificados.
    public string getExpression(int operatorsQtd, int operators, int minNumberValue, int maxNumberValue, int parentheses)
    {
        int missingOperators = operatorsQtd;

        //Debug.Log($"<color=black>GERANDO UMA NOVA EXPRESS�O!</color>");

        // Registra o n�mero de operadores que a express�o ter�.
        //Debug.Log("N�mero de Opera��es Geradas: " + missingOperators);

        // Gera a express�o com a quantidade de operadores faltantes.
        string randomExpression = createRandomExpression(missingOperators, operators, minNumberValue, maxNumberValue, parentheses).Replace(" ", "");

        return randomExpression;
    }
}