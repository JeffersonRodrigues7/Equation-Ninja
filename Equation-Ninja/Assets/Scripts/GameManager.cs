using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Changeable")]
    [SerializeField] private float fallSpeed = 50f; // Velocidade com que a resposta cai (ajustar para corresponder � altura do canvas)
    [SerializeField] private int expressionByLevel = 3; //Vai armazenar a quantidade de expressões por level
    [SerializeField] private float WrongAnswerRangePorcentage = 5; // Porcentagem da varia��o para as respostas incorretas
    [SerializeField] private int maxWrongAnswers = 5; //Vai armazenar a quantidade de erros do jogador


    [Header("Not Changeable")]
    [SerializeField] private int qtdExpressionPassed = 0; //Vai armazenar a quantidade de expressões que passaram
    [SerializeField] private int gameLevel = 0; //Vai armazenar o level atual
    [SerializeField] private int wrongAnswers = 0; //Vai armazenar a quantidade de erros do jogador

    public static GameManager instance; // Refer�ncia est�tica ao GameManager para f�cil acesso

    private TextMeshProUGUI expression; // Refer�ncia ao texto da express�o
    private ExpressionGenerator expressionGenerator; // Refer�ncia ao gerador de express�es

    private Answer answer1; // Refer�ncia � primeira resposta
    private Answer answer2; // Refer�ncia � segunda resposta

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
        // Encontra e obt�m as refer�ncias aos objetos do Unity
        expression = GameObject.Find("Expression")?.GetComponent<TextMeshProUGUI>();
        expressionGenerator = GameObject.Find("ExpressionGenerator")?.GetComponent<ExpressionGenerator>();
        answer1 = GameObject.Find("Answer1")?.GetComponent<Answer>();
        answer2 = GameObject.Find("Answer2")?.GetComponent<Answer>();

        // Define a express�o inicial
        setExpression();
    }

    public void setExpression()
    {
        // Verifica se todas as refer�ncias necess�rias foram encontradas
        if (expression == null) { Debug.LogError($"N�o foi encontrado um local para inserir a express�o"); return; }
        if (expressionGenerator == null) { Debug.LogError($"N�o foi encontrado o gerador de express�o"); return; }
        if (answer1 == null) { Debug.LogError($"N�o foi encontrado Objeto de resposta 01"); return; }
        if (answer2 == null) { Debug.LogError($"N�o foi encontrado Objeto de resposta 02"); return; }

        // Obt�m uma express�o matem�tica do gerador
        string expresstionText = expressionGenerator.getExpression();
        expression.text = expresstionText;

        // Avalia a express�o e obt�m a resposta correta
        if (ExpressionEvaluator.Evaluate(expresstionText, out int correctAnswer))
        {
            // Calcula a faixa de varia��o para as respostas incorretas
            int WrongAnswerRange = (int)(correctAnswer * (WrongAnswerRangePorcentage / 100));

            // Se a faixa de varia��o for zero, defina um valor m�nimo
            if (WrongAnswerRange == 0)
            {
                WrongAnswerRange = Random.Range(1, 5);
            }

            Debug.Log($"WrongAnswerRange: {WrongAnswerRange}");

            // Gera uma resposta incorreta dentro da faixa de varia��o
            int wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);

            // Garante que a resposta incorreta seja diferente da correta
            while (wrongAnswer == correctAnswer)
            {
                wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);
            }

            // Escolhe aleatoriamente qual resposta � a correta
            int correctAnswerObject = Random.Range(1, 3); // Se 1, a Answer 1 ser� correta; se 2, a Answer 2 ser� correta

            Debug.Log($"Resposta correta: {correctAnswer} - Resposta errada: {wrongAnswer}");

            // Define as respostas nas respectivas Answer objetos
            if (correctAnswerObject == 1)
            {
                answer1.setAnswer(correctAnswer, true);
                answer2.setAnswer(wrongAnswer, false);
            }
            else
            {
                answer1.setAnswer(wrongAnswer, false);
                answer2.setAnswer(correctAnswer, true);
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
            Debug.Log($"Erro ao tentar gerar express�o matem�tica a partir da string {expresstionText}, vamos tentar novamente.");
            setExpression(); // Tenta novamente se houver um erro na gera��o da express�o
        }
    }
}