using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Answer : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 100f; // Velocidade com que a resposta cai (ajustar para corresponder à altura do canvas)
    [SerializeField] private bool isCorrectAnswer = true; // Indica se esta é a resposta correta

    private TextMeshProUGUI value; // Texto da resposta
    private TextMeshProUGUI punctuation; // Texto da pontuação

    private float canvasHeight; // Altura do canvas onde a resposta é exibida

    private void Awake()
    {
        value = GetComponent<TextMeshProUGUI>(); // Obtém o componente TextMeshProUGUI da resposta
    }

    private void Start()
    {
        punctuation = GameObject.Find("Punctuation").GetComponent<TextMeshProUGUI>(); // Obtém o componente TextMeshProUGUI da pontuação

        // Obtém a altura do canvas onde a resposta é exibida
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    {
        // Move a resposta para baixo com a velocidade especificada
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Quando a resposta atinge a parte inferior e é a resposta correta, atualiza a expressão no GameManager
        if (transform.position.y < 0f && isCorrectAnswer)
        {
            isCorrectAnswer = false; // Marca a resposta como usada
            GameManager.instance.setExpression(); // Chama a função setExpression no GameManager para atualizar a expressão
        }
    }

    // Configura a resposta com o valor e se é a resposta correta
    public void setAnswer(int _value, bool _isCorrectAnswer)
    {
        if (value != null)
        {
            value.text = _value.ToString(); // Define o texto da resposta como o valor
            isCorrectAnswer = _isCorrectAnswer; // Define se é a resposta correta
            transform.position = new Vector3(transform.position.x, canvasHeight, transform.position.z); // Define a posição da resposta no topo do canvas
        }
        else
        {
            Debug.LogError($"Objeto value ainda não foi carregado"); // Mostra um erro se o objeto "value" não estiver carregado
        }
    }

    // Chamada quando o mouse entra na resposta
    public void OnPointerEnter()
    {
        if (isCorrectAnswer)
        {
            Debug.Log($"<color=green>Resposta correta!</color>");

            if (int.TryParse(punctuation.text, out int actualPunctuation))
            {
                punctuation.text = (actualPunctuation + 1).ToString(); // Incrementa a pontuação se a resposta for correta
            }
            else
            {
                Debug.LogError($"Falha ao converter texto de pontuação: {punctuation.text} para inteiro"); // Mostra um erro se a conversão da pontuação falhar
            }
        }
        else
        {
            Debug.Log($"<color=red>Resposta incorreta!</color>"); // Mensagem para resposta incorreta
        }

        GameManager.instance.setExpression(); // Atualiza a expressão no GameManager
    }
}