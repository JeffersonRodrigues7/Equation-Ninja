using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Answer : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 100f;//precisa deixar coerente com a altura do canvas
    [SerializeField] private bool isCorrectAnswer = true;
    private float canvasHeight;

    private void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, canvasHeight, transform.position.z);
        }
    }

    public void OnPointerEnter()
    {
        if (isCorrectAnswer)
        {

            Debug.Log($"<color=green>Resposta correta!</color>");
        }
        else
        {
            Debug.Log($"<color=red>Resposta incorreta!</color>");
        }
    }
}
