using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ExpressionGenerator : MonoBehaviour
{
    [SerializeField] private int minOperators = 2;  // N�mero m�nimo de operadores na express�o gerada.
    [SerializeField] private int maxOperators = 5;  // N�mero m�ximo de operadores na express�o gerada.

    private string[] operators = { "+", "-", "*" };  // Operadores matem�ticos poss�veis.
    private string[] openedSymbols = { "(", " " };        // S�mbolos de abertura (par�nteses ou "nada").
    private string[] closedSymbols = { ")", " " };        // S�mbolos de fechamento (par�nteses ou "nada").

    // Fun��o que gera uma express�o matem�tica aleat�ria com a quantidade especificada de operadores faltantes.
    private string createRandomExpression(int missingOperators)
    {
        if (missingOperators == 0)
        {
            // Se n�o h� mais operadores faltantes, retorna um n�mero aleat�rio entre 1 e 9 como um operando.
            return Random.Range(1, 10).ToString();
        }
        else
        {
            // Caso contr�rio, gera um s�mbolo de abertura e fechamento (par�nteses ou "nada") aleat�rio.
            int option = Random.Range(0, 2);
            string openSymbol = openedSymbols[option];
            string closeSymbol = closedSymbols[option];

            string expression = "";

            // Adiciona o s�mbolo de abertura � express�o.
            expression += openSymbol;

            // Adiciona um n�mero aleat�rio entre 0 e 9 � express�o como um operando.
            expression += Random.Range(0, 10);

            // Adiciona um operador aleat�rio � express�o.
            expression += operators[Random.Range(0, operators.Length)];

            // Recursivamente, gera a pr�xima parte da express�o com um operador a menos.
            expression += createRandomExpression(missingOperators - 1);

            // Adiciona o s�mbolo de fechamento correspondente ao de abertura.
            expression += closeSymbol;

            return expression;
        }
    }

    // Fun��o p�blica que gera e exibe uma express�o aleat�ria com o n�mero de operadores dentro dos limites especificados.
    public string getExpression()
    {
        int missingOperators = Random.Range(minOperators, maxOperators);

        Debug.Log($"<color=black>GERANDO UMA NOVA EXPRESS�O!</color>");

        // Registra o n�mero de operadores que a express�o ter�.
        Debug.Log("N�mero de Opera��es Geradas: " + missingOperators);

        // Gera a express�o com a quantidade de operadores faltantes.
        string randomExpression = createRandomExpression(missingOperators).Replace(" ", "");

        return randomExpression;
    }
}