using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    private Color originalColor;
    private Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Cor mais escura para o efeito de hover

    void Start()
    {
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Escurece o texto quando o mouse entra no botão
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura a cor original do texto quando o mouse sai do botão
        buttonText.color = originalColor;
    }
}
