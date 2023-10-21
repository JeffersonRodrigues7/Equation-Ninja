using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudioHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip hoverSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = hoverSound;
        audioSource.playOnAwake = false; // Configura para não tocar no início
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        audioSource.Stop();
    }
}
