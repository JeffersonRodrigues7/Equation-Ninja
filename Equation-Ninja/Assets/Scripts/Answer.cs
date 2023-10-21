using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    
    [SerializeField] private bool isCorrectAnswer = true; // Indica se esta � a resposta correta
    [SerializeField] private bool canMove = false; //Determina se o número pode começar a cair

    private TextMeshProUGUI value; // Texto da resposta

    private float canvasHeight; // Altura do canvas onde a resposta � exibida
    private float fallSpeed = 50f; // Velocidade com que a resposta cai
    private bool isAnswerRight = true; //Vai retornar ao GameManager se a resposta está certa ou errada


    private void Awake()
    {
        value = GetComponent<TextMeshProUGUI>(); // Obt�m o componente TextMeshProUGUI da resposta
    }

    private void Start()
    {
        // Obt�m a altura do canvas onde a resposta � exibida
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        transform.position = new Vector3(transform.position.x, canvasHeight, transform.position.z);
    }

    private void Update()
    {
        if(canMove)
        {
            // Move a resposta para baixo com a velocidade especificada
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            // Quando a resposta atinge a parte inferior e � a resposta correta, atualiza a express�o no GameManager
            if (transform.position.y < 0f && isCorrectAnswer)
            {
                canMove = false;
                isCorrectAnswer = false; // Marca a resposta como usada
                GameManager.instance.setExpression(); // Chama a fun��o setExpression no GameManager para atualizar a express�o
            }
        }

    }

    // Configura a resposta com o valor e se � a resposta correta
    public void setAnswer(int _value, bool _isCorrectAnswer, float _fallSpeed)
    {
        if (value != null)
        {
            value.text = _value.ToString(); // Define o texto da resposta como o valor
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
        canMove = false;
        
        if (isCorrectAnswer)
        {
            isAnswerRight = true;
        }
        else
        {
            isAnswerRight = false;
        }

        GameManager.instance.updatePunctuation(isAnswerRight); // Atualiza a express�o no GameManager
    }
}