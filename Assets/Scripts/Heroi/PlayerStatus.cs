using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    public int vida = 100;
    private int pontos = 0;

    public Slider sliderVida;
    public Text textoPontos;
    public Text textoMunicao;

    public int municaoPlayer = 17;
    public int carregadorPlayer = 3;

    public int municaoCarro = 10;
    public int municaoMaxCarro = 10;

    void Awake()
    {
        Instance = this;
    }

    public void AtualizarVida(int delta)
    {
        vida = Mathf.Clamp(vida + delta, 0, 100);
        if (sliderVida != null)
        {
            sliderVida.value = vida;
        }
            

        if (vida <= 0)
        {
            FimDeJogo();
        }
    }

    public void AtualizarPontuacao(int delta)
    {
        pontos += delta;
        if (textoPontos != null)
            textoPontos.text = "Pontos: " + pontos;
    }

    public void AtualizarMunicao(bool estaNoCarro = false)
    {
        if (textoMunicao != null)
        {
            if (estaNoCarro)
            {
                textoMunicao.text = municaoCarro + "/" + municaoMaxCarro;
            }
            else
            {
                textoMunicao.text = municaoPlayer + "/" + carregadorPlayer;
            }
        }
    }

    private void FimDeJogo()
    {
        Debug.Log("GAME OVER");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }
}
