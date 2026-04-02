using UnityEngine;
using TMPro;

public class MuseumUIController : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public AudioSource audioSource;

    private DesktopFirstPersonController playerController;

    void Start()
    {
        playerController = FindObjectOfType<DesktopFirstPersonController>();

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void Update()
    {
        if (infoPanel != null && infoPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            FecharPainel();
        }
    }

    public void AbrirPainel(string titulo, string descricao, AudioClip narracao)
    {
        if (infoPanel != null)
            infoPanel.SetActive(true);

        if (titleText != null)
            titleText.text = titulo;

        if (descriptionText != null)
            descriptionText.text = descricao;

        if (playerController != null)
            playerController.podeMover = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (audioSource != null)
        {
            audioSource.Stop();

            if (narracao != null)
            {
                audioSource.clip = narracao;
                audioSource.Play();
            }
        }
    }

    public void FecharPainel()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);

        if (playerController != null)
            playerController.podeMover = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}