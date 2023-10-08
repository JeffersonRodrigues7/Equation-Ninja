using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ExpressionGenerator : MonoBehaviour
{
    [SerializeField] private int minOperators = 2;  // Número mínimo de operadores na expressão gerada.
    [SerializeField] private int maxOperators = 5;  // Número máximo de operadores na expressão gerada.

    private string[] operators = { "+", "-", "*" };  // Operadores matemáticos possíveis.
    private string[] openedSymbols = { "(", " " };        // Símbolos de abertura (parênteses ou "nada").
    private string[] closedSymbols = { ")", " " };        // Símbolos de fechamento (parênteses ou "nada").

    // Função que gera uma expressão matemática aleatória com a quantidade especificada de operadores faltantes.
    private string createRandomExpression(int missingOperators)
    {
        if (missingOperators == 0)
        {
            // Se não há mais operadores faltantes, retorna um número aleatório entre 1 e 9 como um operando.
            return Random.Range(1, 10).ToString();
        }
        else
        {
            // Caso contrário, gera um símbolo de abertura e fechamento (parênteses ou "nada") aleatório.
            int option = Random.Range(0, 2);
            string openSymbol = openedSymbols[option];
            string closeSymbol = closedSymbols[option];

            string expression = "";

            // Adiciona o símbolo de abertura à expressão.
            expression += openSymbol;

            // Adiciona um número aleatório entre 0 e 9 à expressão como um operando.
            expression += Random.Range(0, 10);

            // Adiciona um operador aleatório à expressão.
            expression += operators[Random.Range(0, operators.Length)];

            // Recursivamente, gera a próxima parte da expressão com um operador a menos.
            expression += createRandomExpression(missingOperators - 1);

            // Adiciona o símbolo de fechamento correspondente ao de abertura.
            expression += closeSymbol;

            return expression;
        }
    }

    // Função pública que gera e exibe uma expressão aleatória com o número de operadores dentro dos limites especificados.
    public string getExpression()
    {
        int missingOperators = Random.Range(minOperators, maxOperators);

        Debug.Log($"<color=black>GERANDO UMA NOVA EXPRESSÃO!</color>");

        // Registra o número de operadores que a expressão terá.
        Debug.Log("Número de Operações Geradas: " + missingOperators);

        // Gera a expressão com a quantidade de operadores faltantes.
        string randomExpression = createRandomExpression(missingOperators).Replace(" ", "");

        return randomExpression;
    }
}