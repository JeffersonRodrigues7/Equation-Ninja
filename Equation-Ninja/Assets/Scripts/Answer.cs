using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Answer : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 100f; // Velocidade com que a resposta cai (ajustar para corresponder � altura do canvas)
    [SerializeField] private bool isCorrectAnswer = true; // Indica se esta � a resposta correta

    private TextMeshProUGUI value; // Texto da resposta
    private TextMeshProUGUI punctuation; // Texto da pontua��o

    private float canvasHeight; // Altura do canvas onde a resposta � exibida

    private bool canMove = true; //Determina se o número pode começar a cair

    private void Awake()
    {
        value = GetComponent<TextMeshProUGUI>(); // Obt�m o componente TextMeshProUGUI da resposta
    }

    private void Start()
    {
        punctuation = GameObject.Find("Punctuation").GetComponent<TextMeshProUGUI>(); // Obt�m o componente TextMeshProUGUI da pontua��o

        // Obt�m a altura do canvas onde a resposta � exibida
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
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
                canMove = true;
            }
        }

    }

    // Configura a resposta com o valor e se � a resposta correta
    public void setAnswer(int _value, bool _isCorrectAnswer)
    {
        if (value != null)
        {
            value.text = _value.ToString(); // Define o texto da resposta como o valor
            isCorrectAnswer = _isCorrectAnswer; // Define se � a resposta correta
            transform.position = new Vector3(transform.position.x, canvasHeight, transform.position.z); // Define a posi��o da resposta no topo do canvas
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
            Debug.Log($"<color=green>Resposta correta!</color>");

            if (int.TryParse(punctuation.text, out int actualPunctuation))
            {
                punctuation.text = (actualPunctuation + 1).ToString(); // Incrementa a pontua��o se a resposta for correta
            }
            else
            {
                Debug.LogError($"Falha ao converter texto de pontua��o: {punctuation.text} para inteiro"); // Mostra um erro se a convers�o da pontua��o falhar
            }
        }
        else
        {
            Debug.Log($"<color=red>Resposta incorreta!</color>"); // Mensagem para resposta incorreta
        }

        GameManager.instance.setExpression(); // Atualiza a express�o no GameManager
        canMove = true;
    }
}