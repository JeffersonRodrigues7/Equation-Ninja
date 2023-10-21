using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImageHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage;
    private Color originalColor;

    void Start()
    {
        originalColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Escurece a imagem
        buttonImage.color = originalColor * 0.8f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura a cor original da imagem
        buttonImage.color = originalColor;
    }
}
