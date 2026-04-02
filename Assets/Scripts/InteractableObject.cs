using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string titulo;

    [TextArea(5, 10)]
    public string textoInformacao;

    public AudioClip narracao;
}