using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI textoPontuacao;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        int pontuacaoFinal = PlayerPrefs.GetInt("PontuacaoFinal", 0);
        textoPontuacao.text = "Pontuação: " + pontuacaoFinal;
    }

    public void NovoJogo()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
