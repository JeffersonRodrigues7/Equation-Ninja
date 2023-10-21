using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Levels levels;  //ScriptableObject com os parametros por level
    [SerializeField] private int expressionByLevel = 3; //Vai armazenar a quantidade de expressões por level
    [SerializeField] private int maxWrongAnswers = 3; //Vai armazenar a quantidade de erros do jogador

    [Header("Scripts")]
    [SerializeField] private ExpressionGenerator expressionGenerator; // Refer�ncia ao gerador de express�es
    [SerializeField] private Answer answer1; // Referencia a primeira resposta
    [SerializeField] private Answer answer2; // Referencia a segunda resposta

    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI expressionTextMeshPro; // Referencia ao texto da express�o
    [SerializeField] private TextMeshProUGUI punctuationTextMeshPro; // Texto da pontua��o
    [SerializeField] private TextMeshProUGUI levelTextMeshPro; // Texto ddo level
    [SerializeField] private GameObject inputField;//Input onde o jogador irá digitar seu nome
    [SerializeField] private GameObject finalPonctuationText;//Texto com a pontuação final do jogador

    [Header("Imagens")]
    [SerializeField] private Image[] booksLife; // Vai armazenar as imafgens de vida
    [SerializeField] private Sprite book; // Vai armazenar imagem do livro
    [SerializeField] private Sprite bookLost; // Vai armazenar imagem do livro perdido

    [Header("Variáveis apenas de visualização")]
    [SerializeField] private int ponctuationValue = 0; //Vai armazenar a quantidade de erros do jogador
    [SerializeField] private int qtdExpressionPassed = 0; //Vai armazenar a quantidade de expressões que passaram
    [SerializeField] private int gameLevel = 0; //Vai armazenar o level atual
    [SerializeField] private int correctedAnswers = 0; //Vai armazenar a quantidade de erros do jogador
    [SerializeField] private int wrongAnswers = 0; //Vai armazenar a quantidade de erros do jogador

    public static GameManager instance; // Referencia est�tica ao GameManager para facil acesso
    private Parameters[] levelsParameters; //atalho para o array de parametros por level

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
        inputField.SetActive(false);//Desativa input no inicio do jogo
        finalPonctuationText.SetActive(false);//Desativa pontuação final no inicio do jogo

        //Setando valores iniciais
        foreach (Image bookLife in booksLife)
        {
            bookLife.sprite = book;
        }

        ponctuationValue = 0; //Vai armazenar a quantidade de erros do jogador
        qtdExpressionPassed = 0; //Vai armazenar a quantidade de expressões que passaram
        gameLevel = 0; //Vai armazenar o level atual
        correctedAnswers = 0; //Vai armazenar a quantidade de erros do jogador
        wrongAnswers = 0; //Vai armazenar a quantidade de erros do jogador

        punctuationTextMeshPro.text = ponctuationValue.ToString();
        levelTextMeshPro.text = "Level: " + gameLevel.ToString();

        levelsParameters = levels.parameters;

        // Define a expressao inicial
        setExpression();
    }

    public void setExpression()
    {
        // Verifica se todas as refer�ncias necess�rias foram encontradas
        if (expressionTextMeshPro == null) { Debug.LogError($"Nao foi encontrado um local para inserir a expressao"); return; }
        if (expressionGenerator == null) { Debug.LogError($"Nao foi encontrado o gerador de expressao"); return; }
        if (answer1 == null) { Debug.LogError($"Nao foi encontrado Objeto de resposta 01"); return; }
        if (answer2 == null) { Debug.LogError($"Nao foi encontrado Objeto de resposta 02"); return; }

        /** Este condicional verifica se o nível atual do jogo está dentro dos limites definidos em levelsParameters e, se estiver, 
        * ele configura várias variáveis com os parâmetros associados a esse nível.*/
        if (levels != null && gameLevel < levelsParameters.Length)
        {
            Debug.Log("GameLevel: " + gameLevel + " - Level Length" + levelsParameters.Length);
            operatorsQtd = levelsParameters[gameLevel].operatorsQtd;
            operators = levelsParameters[gameLevel].operators;
            minNumberValue = levelsParameters[gameLevel].minNumberValue;
            maxNumberValue = levelsParameters[gameLevel].maxNumberValue;
            parentheses = levelsParameters[gameLevel].parentheses;
            WrongAnswerRangePorcentage = levelsParameters[gameLevel].WrongAnswerRangePorcentage;
            fallSpeed = levelsParameters[gameLevel].fallSpeed;
        }

        // Obt�m uma express�o matem�tica do gerador
        string expresstionText = expressionGenerator.getExpression(operatorsQtd, operators, minNumberValue, maxNumberValue, parentheses);

        // Avalia a express�o e obtem a resposta correta
        if (ExpressionEvaluator.Evaluate(expresstionText, out int correctAnswer))
        {
            expressionTextMeshPro.text = expresstionText;//Coloca expressão na tela

            // Calcula a faixa de variacao para as respostas incorretas
            int WrongAnswerRange = (int)(correctAnswer * (WrongAnswerRangePorcentage / 100));

            // Se a faixa de variacao for zero, defina um valor m�nimo
            if (WrongAnswerRange == 0)
            {
                WrongAnswerRange = Random.Range(1, 5);
            }

           // Debug.Log($"WrongAnswerRange: {WrongAnswerRange}");

            // Gera uma resposta incorreta dentro da faixa de variacao
            int wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);

            // Garante que a resposta incorreta seja diferente da correta
            while (wrongAnswer == correctAnswer)
            {
                wrongAnswer = Random.Range(correctAnswer - WrongAnswerRange, correctAnswer + WrongAnswerRange + 1);
            }

            // Escolhe aleatoriamente qual resposta e a correta
            int correctAnswerObject = Random.Range(1, 3); // Se 1, a Answer 1 sera correta; se 2, a Answer 2 ser� correta

            //Debug.Log($"Resposta correta: {correctAnswer} - Resposta errada: {wrongAnswer}");

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
        }

        else
        {
            Debug.Log($"Erro ao tentar gerar express�o matemática a partir da string {expresstionText}, vamos tentar novamente.");
            setExpression(); // Tenta novamente se houver um erro na gera��o da express�o
        }
    }

    //Atualiza pontuação do jogador
    public void updatePunctuation(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
        {
            Debug.Log($"<color=green>Resposta correta!</color>");

            if (int.TryParse(punctuationTextMeshPro.text, out int actualPunctuation))
            {
                ponctuationValue = (actualPunctuation + 1);
                punctuationTextMeshPro.text = ponctuationValue.ToString(); // Incrementa a pontua��o se a resposta for correta
                correctedAnswers++;
            }
            else
            {
                Debug.LogError($"Falha ao converter texto de pontua��o: {punctuationTextMeshPro.text} para inteiro"); // Mostra um erro se a convers�o da pontua��o falhar
            }
        }
        else //Jogador errou
        {

            if(wrongAnswers >= maxWrongAnswers) //GameOver
            {
                //Abaixo vamos parar o movimento dos números e impedir que a contagem continue e habilitar os objetos de input e placar final
                answer1.setGameOver();
                answer2.setGameOver();
                finalPonctuationText.SetActive(true);
                inputField.SetActive(true);
                finalPonctuationText.GetComponent<TextMeshProUGUI>().text = "Placar Final: " + ponctuationValue;
            }

            //Setando imagem de erro
            if(wrongAnswers < booksLife.Length)
            {
                booksLife[wrongAnswers].sprite = bookLost;
            }

            Debug.Log($"<color=red>Resposta incorreta!</color>"); // Mensagem para resposta incorreta
            wrongAnswers++;
        }

        //Aumentando quantidade de expressões que passaram e verificando se devemos aumentar de level
        qtdExpressionPassed++;
        if (qtdExpressionPassed % expressionByLevel == 0)
        {
            gameLevel++;
            levelTextMeshPro.text = "Level: " + gameLevel.ToString();
        }

        setExpression();
    }
}