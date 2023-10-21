using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Levels", menuName = "Game Levels")]
public class Levels : ScriptableObject
{
    public Parameters[] parameters;
}

[System.Serializable]
public class Parameters
{
    public int level = 0;
    public int operatorsQtd = 1; //Armazena a quantidade de operadores
    public int operators = 1;// Quais operators podem ser utilizados. 1 -> +; 2 -> +/-; 3 -> +/-/*
    public int minNumberValue = 0; //Valor minimo que poderá ser gerado
    public int maxNumberValue = 5; //Valor máximo que poderá ser gerado
    public int parentheses = 1; //1 -> Sem parenteses; 2 -> Pode ter parentheses
    public float WrongAnswerRangePorcentage = 5; // Porcentagem da variacao para as respostas incorretas
    public float fallSpeed = 50f; // Velocidade com que a resposta cai (ajustar para corresponder ? altura do canvas)
}
