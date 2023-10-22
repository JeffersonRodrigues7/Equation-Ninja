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
    public int points = 1; //Qtd de pontos que o player ganhará por level, o padrão seria 1 ponto por level
    public int operatorsQtd = 1; //Armazena a quantidade de operadores
    public int operators = 1;// Quais operators podem ser utilizados. 1 -> +; 2 -> +/-; 3 -> +/-/*
    public int minNumberValue = 0; //Valor minimo que poderá ser gerado
    public int maxNumberValue = 5; //Valor máximo que poderá ser gerado
    public int parentheses = 1; //1 -> Sem parenteses; 2 -> Pode ter parentheses
    public float WrongAnswerRangePorcentage = 5; // Porcentagem da variacao para as respostas incorretas
    public float fallSpeed = 50f; // Velocidade com que a resposta cai (ajustar para corresponder ? altura do canvas)
    public int bonusType = 1;/**
                          * 0 -> Não é possível ter bônus neste lvl
                          * 1 -> Fase Bônus será acionada quando tivermos uma multiplicação na conta
                          * 2 -> Fase Bônus será acionada quando tivermos duas multiplicações na conta
                          * 3 -> Fase Bônus será acionada quando tivermos uma ou mais multiplicações na conta com o número 7 envolvido
                          * 4 -> Fase Bônus será acionada quando tivermos uma ou mais multiplicações na conta com o número 7/-7 envolvido
                          */
}
