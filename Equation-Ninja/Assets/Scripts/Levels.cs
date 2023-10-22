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
    public int points = 1; //Qtd de pontos que o player ganhar� por level, o padr�o seria 1 ponto por level
    public int operatorsQtd = 1; //Armazena a quantidade de operadores
    public int operators = 1;// Quais operators podem ser utilizados. 1 -> +; 2 -> +/-; 3 -> +/-/*
    public int minNumberValue = 0; //Valor minimo que poder� ser gerado
    public int maxNumberValue = 5; //Valor m�ximo que poder� ser gerado
    public int parentheses = 1; //1 -> Sem parenteses; 2 -> Pode ter parentheses
    public float WrongAnswerRangePorcentage = 5; // Porcentagem da variacao para as respostas incorretas
    public float fallSpeed = 50f; // Velocidade com que a resposta cai (ajustar para corresponder ? altura do canvas)
    public int bonusType = 1;/**
                          * 0 -> N�o � poss�vel ter b�nus neste lvl
                          * 1 -> Fase B�nus ser� acionada quando tivermos uma multiplica��o na conta
                          * 2 -> Fase B�nus ser� acionada quando tivermos duas multiplica��es na conta
                          * 3 -> Fase B�nus ser� acionada quando tivermos uma ou mais multiplica��es na conta com o n�mero 7 envolvido
                          * 4 -> Fase B�nus ser� acionada quando tivermos uma ou mais multiplica��es na conta com o n�mero 7/-7 envolvido
                          */
}
