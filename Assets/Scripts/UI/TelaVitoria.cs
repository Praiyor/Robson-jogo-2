using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TelaVitoria : MonoBehaviour
{
    public TMP_Text textoPontuacao;
    public Button botaoNovoJogo;

    void Start()
    {        
        int pontuacaoFinal = PlayerPrefs.GetInt("PontuacaoFinal", 0);
        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontuação: " + pontuacaoFinal;
        }
        
        if (botaoNovoJogo != null)
        {
            botaoNovoJogo.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("SampleScene");
            });
        }
    }
}
