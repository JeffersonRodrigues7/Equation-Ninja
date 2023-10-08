using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float WrongAnswerRangePorcentage = 5; // Porcentagem da variação para as respostas incorretas

    public static GameManager instance; // Referência estática ao GameManager para fácil acesso

    private TextMeshProUGUI expression; // Referência ao texto da expressão
    private ExpressionGenerator expressionGenerator; // Referência ao gerador de expressões

    private Answer answer1; // Referência à primeira resposta
    private Answer answer2; // Referência à segunda resposta

    private void Awake()
    {
        // Certifica-se de que só exista uma instância do GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o GameManager ativo entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Encontra e obtém as referências aos objetos do Unity
        expression = GameObject.Find("Expression")?.GetComponent<TextMeshProUGUI>();
        expressionGenerator = GameObject.Find("ExpressionGenerator")?.GetComponent<ExpressionGenerator>();
        answer1 = GameObject.Find("Answer1")?.GetComponent<Answer>();
        answer2 = GameObject.Find("Answer2")?.GetComponent<Answer>();

        // Define a expressão inicial
        setExpression();
    }

    public void setExpression()
    {
        // Verifica se todas as referências necessárias foram encontradas
        if (expression == null) { Debug.LogError($"Não foi encontrado um local para inserir a expressão"); return; }
        if (expressionGenerator == null) { Debug.LogError($"Não foi encontrado o gerador de expressão"); return; }
        if (answer1 == null) { Debug.LogError($"Não foi encontrado Objeto de resposta 01"); return; }
        if (answer2 == null) { Debug.LogError($"Não foi encontrado Objeto de resposta 02"); return; }

        // Obtém uma expressão matemática do gerador
        string expresstionText = expressionGenerator.getExpression();
        expression.text = expresstionText;

        // Avalia a expressão e obtém a resposta correta
        if (ExpressionEvaluator.Evaluate(expresstionText, out int correctAnswer))
        {
            // Calcula a faixa de variação para as respostas incorretas
            int WrongAnswerRange = (int)(correctAnswer * (WrongAnswerRangePorcentage / 100));

            // Se a faixa de variação for zero, defina um valor mínimo
            if (WrongAnswerRange == 0)
            {
                WrongAnswerRange = Random.Range(1, 5);
            }

            Debug.Log($"WrongAnswerRange: {WrongAnswerRange}");

            // Gera uma resposta incorreta dentro da faixa de variação
            int wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);

            // Garante que a resposta incorreta seja diferente da correta
            while (wrongAnswer == correctAnswer)
            {
                wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);
            }

            // Escolhe aleatoriamente qual resposta é a correta
            int correctAnswerObject = Random.Range(1, 3); // Se 1, a Answer 1 será correta; se 2, a Answer 2 será correta

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
        }
        else
        {
            Debug.Log($"Erro ao tentar gerar expressão matemática a partir da string {expresstionText}, vamos tentar novamente.");
            setExpression(); // Tenta novamente se houver um erro na geração da expressão
        }
    }
}