using TMPro;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{

    [SerializeField] private bool isCorrectAnswer = true; // Indica se esta � a resposta correta
    [SerializeField] public bool canMove = false; //Determina se o número pode começar a cair
    [SerializeField] private Transform TopPointer; //Caso o jogador acerte a resposta acima deste ponto ele ganhará + 3 pontos
    [SerializeField] private Transform BottomPointer; //Caso o jogador acerte a resposta acima deste ponto ele ganhará + 2 pontos
    [SerializeField] private int topCanvasPoints = 3;
    [SerializeField] private int mediumCanvasPoints = 2;
    [SerializeField] private int bottomCanvasPoints = 1;
    [SerializeField] private GameObject imgRisco;

    private GameControl gameControll;
    private TextMeshProUGUI value; // Texto da resposta

    private float canvasHeight; // Altura do canvas onde a resposta � exibida
    private float fallSpeed = 50f; // Velocidade com que a resposta cai
    private bool isAnswerRight = true; //Vai retornar ao GameControl se a resposta está certa ou errada
    private bool gameOver = false;
    public Blade blade;

    private void Awake()
    {
        value = GetComponent<TextMeshProUGUI>(); // Obt�m o componente TextMeshProUGUI da resposta
    }

    private void Start()
    {
        gameControll = GameObject.Find("GameControl").GetComponent<GameControl>();

        // Obt�m a altura do canvas onde a resposta � exibida
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        transform.position = new Vector3(transform.position.x, canvasHeight, transform.position.z);
        gameOver = false;
    }

    private void Update()
    {
        if (gameOver) return;


        if (canMove)
        {
            // Move a resposta para baixo com a velocidade especificada

            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);


            // Quando a resposta atinge a parte inferior e � a resposta correta, atualiza a express�o no GameControl
            if (transform.position.y < 0f && isCorrectAnswer)
            {
                canMove = false;
                isCorrectAnswer = false; // Marca a resposta como usada
                gameControll.updatePunctuation(isCorrectAnswer, 0); // Chama a fun��o setExpression no GameControl para atualizar a express�o
            }
        }

    }

    // Configura a resposta com o valor e se � a resposta correta
    public void setAnswer(int _value, bool _isCorrectAnswer, float _fallSpeed, Color textColor)
    {
        if (value != null)
        {
            imgRisco.SetActive(false);
            value.text = _value.ToString(); // Define o texto da resposta como o valor
            value.color = textColor;//Cor do texto
            isCorrectAnswer = _isCorrectAnswer; // Define se � a resposta correta
            transform.position = new Vector3(transform.position.x, canvasHeight, transform.position.z); // Define a posi��o da resposta no topo do canvas
            fallSpeed = _fallSpeed;
            canMove = true;
        }
        else
        {
            Debug.LogError($"Objeto value ainda n�o foi carregado"); // Mostra um erro se o objeto "value" n�o estiver carregado
        }
    }

    // Chamada quando o mouse entra na resposta
    public void OnPointerEnter()
    {
        int increasedPoints = 0;

        if (gameOver) return;



        if (isCorrectAnswer)
        {
            isAnswerRight = true;

            if (transform.position.y >= TopPointer.position.y) //Jogador acertou na parte mais alta
                increasedPoints = topCanvasPoints;
            else if (transform.position.y >= BottomPointer.position.y) //Jogador acertou no meio
                increasedPoints = mediumCanvasPoints;
            else
                increasedPoints = bottomCanvasPoints;
        }
        else
        {
            isAnswerRight = false;

        }

        if (blade.isCutting)
        {
            imgRisco.SetActive(true);
            StartCoroutine(gameControll.createNewExpression(isAnswerRight, increasedPoints));
        }
    }

    public void setGameOver()
    {
        gameOver = true;
    }


}
