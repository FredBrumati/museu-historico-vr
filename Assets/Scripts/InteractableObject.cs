using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [TextArea]
    public string textoInformacao;

    public void Interagir()
    {
        Debug.Log(textoInformacao);
    }
}