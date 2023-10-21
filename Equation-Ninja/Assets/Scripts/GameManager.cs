using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Variáveis de Dificuldade")]
    [SerializeField] private int operatorsQtd = 1; //Armazena a quantidade de operadores
    [SerializeField] private int operators = 1;// Quais operators podem ser utilizados. 1 -> +; 2 -> +/-; 3 -> +/-/*
    [SerializeField] private int minNumberValue = 0; //Valor minimo que poderá ser gerado
    [SerializeField] private int maxNumberValue = 5; //Valor máximo que poderá ser gerado
    [SerializeField] private int parentheses = 1; //1 -> Sem parenteses; 2 -> Pode ter parentheses
    [SerializeField] private float WrongAnswerRangePorcentage = 5; // Porcentagem da variacao para as respostas incorretas
    [SerializeField] private float fallSpeed = 50f; // Velocidade com que a resposta cai (ajustar para corresponder � altura do canvas)

    [Header("Variáveis de Design")]
    [SerializeField] private int expressionByLevel = 3; //Vai armazenar a quantidade de expressões por level
    [SerializeField] private int maxWrongAnswers = 5; //Vai armazenar a quantidade de erros do jogador

    [Header("Variáveis apenas de visualização")]
    [SerializeField] private int ponctuationValue = 0; //Vai armazenar a quantidade de erros do jogador
    [SerializeField] private int qtdExpressionPassed = 0; //Vai armazenar a quantidade de expressões que passaram
    [SerializeField] private int gameLevel = 0; //Vai armazenar o level atual
    [SerializeField] private int correctedAnswers = 0; //Vai armazenar a quantidade de erros do jogador
    [SerializeField] private int wrongAnswers = 0; //Vai armazenar a quantidade de erros do jogador

    public static GameManager instance; // Referencia est�tica ao GameManager para facil acesso

    private TextMeshProUGUI expression; // Referencia ao texto da express�o
    private ExpressionGenerator expressionGenerator; // Refer�ncia ao gerador de express�es

    private Answer answer1; // Referencia a primeira resposta
    private Answer answer2; // Referencia a segunda resposta

    private TextMeshProUGUI punctuation; // Texto da pontua��o

    private void Awake()
    {
        // Certifica-se de que s� exista uma inst�ncia do GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m o GameManager ativo entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Encontra e obtem as referencias aos objetos do Unity
        expression = GameObject.Find("Expression")?.GetComponent<TextMeshProUGUI>();
        expressionGenerator = GameObject.Find("ExpressionGenerator")?.GetComponent<ExpressionGenerator>();
        answer1 = GameObject.Find("Answer1")?.GetComponent<Answer>();
        answer2 = GameObject.Find("Answer2")?.GetComponent<Answer>();
        punctuation = GameObject.Find("Punctuation").GetComponent<TextMeshProUGUI>(); // Obt�m o componente TextMeshProUGUI da pontua��o

        // Define a expressao inicial
        setExpression();
    }

    public void setExpression()
    {
        // Verifica se todas as refer�ncias necess�rias foram encontradas
        if (expression == null) { Debug.LogError($"Nao foi encontrado um local para inserir a expressao"); return; }
        if (expressionGenerator == null) { Debug.LogError($"Nao foi encontrado o gerador de expressao"); return; }
        if (answer1 == null) { Debug.LogError($"Nao foi encontrado Objeto de resposta 01"); return; }
        if (answer2 == null) { Debug.LogError($"Nao foi encontrado Objeto de resposta 02"); return; }

        // Obt�m uma express�o matem�tica do gerador
        string expresstionText = expressionGenerator.getExpression(operatorsQtd, operators, minNumberValue, maxNumberValue, parentheses);

        // Avalia a express�o e obtem a resposta correta
        if (ExpressionEvaluator.Evaluate(expresstionText, out int correctAnswer))
        {
            expression.text = expresstionText;//Coloca expressão na tela

            // Calcula a faixa de variacao para as respostas incorretas
            int WrongAnswerRange = (int)(correctAnswer * (WrongAnswerRangePorcentage / 100));

            // Se a faixa de variacao for zero, defina um valor m�nimo
            if (WrongAnswerRange == 0)
            {
                WrongAnswerRange = Random.Range(1, 5);
            }

            Debug.Log($"WrongAnswerRange: {WrongAnswerRange}");

            // Gera uma resposta incorreta dentro da faixa de variacao
            int wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);

            // Garante que a resposta incorreta seja diferente da correta
            while (wrongAnswer == correctAnswer)
            {
                wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);
            }

            // Escolhe aleatoriamente qual resposta e a correta
            int correctAnswerObject = Random.Range(1, 3); // Se 1, a Answer 1 sera correta; se 2, a Answer 2 ser� correta

            Debug.Log($"Resposta correta: {correctAnswer} - Resposta errada: {wrongAnswer}");

            // Define as respostas nas respectivas Answer objetos
            if (correctAnswerObject == 1)
            {
                answer1.setAnswer(correctAnswer, true, fallSpeed);
                answer2.setAnswer(wrongAnswer, false, fallSpeed);
            }
            else
            {
                answer1.setAnswer(wrongAnswer, false, fallSpeed);
                answer2.setAnswer(correctAnswer, true, fallSpeed);
            }

            //Aumentando quantidade de expressões que passaram e verificando se devemos aumentar de level
            qtdExpressionPassed++;
            if(qtdExpressionPassed % expressionByLevel == 0)
            {
                gameLevel++;
            }        
        }

        else
        {
            Debug.Log($"Erro ao tentar gerar express�o matemática a partir da string {expresstionText}, vamos tentar novamente.");
            setExpression(); // Tenta novamente se houver um erro na gera��o da express�o
        }
    }

    public void updatePunctuation(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
        {
            Debug.Log($"<color=green>Resposta correta!</color>");

            if (int.TryParse(punctuation.text, out int actualPunctuation))
            {
                ponctuationValue = (actualPunctuation + 1);
                punctuation.text = ponctuationValue.ToString(); // Incrementa a pontua��o se a resposta for correta
                correctedAnswers++;
            }
            else
            {
                Debug.LogError($"Falha ao converter texto de pontua��o: {punctuation.text} para inteiro"); // Mostra um erro se a convers�o da pontua��o falhar
            }
        }
        else
        {
            Debug.Log($"<color=red>Resposta incorreta!</color>"); // Mensagem para resposta incorreta
            wrongAnswers++;
        }

        setExpression();
    }
}