using UnityEngine;

public class EntrarCarro : MonoBehaviour, IEntrar
{
    public GameObject player;
    public GameObject carro;
    public Camera cameraPlayer;
    public Camera cameraCarro;
    public GameObject canhao;

    private bool dentroDoCarro = false;
    private CarroController carroController;

    void Start()
    {
        carroController = carro.GetComponent<CarroController>();
        carroController.enabled = false;
        cameraCarro.GetComponent<CameraCarro>().enabled = false;
        cameraCarro.enabled = false;
        cameraPlayer.GetComponent<AudioListener>().enabled = false;
        canhao.GetComponent<CanhaoCarro>().enabled = false;
    }

    void Update()
    {

    }

    public void Entrar()
    {
        if (dentroDoCarro) 
        {
            return;
        }

        dentroDoCarro = true;

        player.GetComponent<MovimentarPersonagem>().enabled = false;
        

        var identificar = cameraPlayer.GetComponent<IdentificarObjeto>();
        if (identificar != null)
        {
            identificar.EsconderTexto();
        }

        player.SetActive(false);
        carroController.enabled = true;
        canhao.GetComponent<CanhaoCarro>().enabled = true;
        carro.layer = LayerMask.NameToLayer("IgnorePlayercast");

        cameraCarro.enabled = true;
        cameraCarro.tag = "MainCamera";
        cameraCarro.GetComponent<AudioListener>().enabled = true;
        cameraCarro.GetComponent<CameraCarro>().enabled = true;
        cameraPlayer.GetComponent<AudioListener>().enabled = false;
    }

    public bool EstaDentro()
    {
        return dentroDoCarro;
    }
}
