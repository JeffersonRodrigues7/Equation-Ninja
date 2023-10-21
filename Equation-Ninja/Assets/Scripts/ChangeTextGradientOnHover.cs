using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChangeTextGradientOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textMesh; // Usando TMPro para TextMesh
    public Color hoverColor = Color.red; // Cor do texto quando o mouse está sobre ele
    private Color originalColor; // Cor original do texto

    void Start()
    {
        originalColor = textMesh.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // O mouse entrou na área do texto
        textMesh.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // O mouse saiu da área do texto
        textMesh.color = originalColor;
    }
}
