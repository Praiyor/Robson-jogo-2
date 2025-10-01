using UnityEngine;

public class EntrarCarro : MonoBehaviour, IEntrar
{
    public GameObject player;
    public GameObject carro;
    public Camera cameraPlayer;
    public Camera cameraCarro;
    public Transform pontoSaida;
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
        if (dentroDoCarro && Input.GetKeyDown(KeyCode.F))
        {
            Sair();
        }
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

        cameraCarro.enabled = true;
        cameraCarro.GetComponent<AudioListener>().enabled = true;
        cameraCarro.GetComponent<CameraCarro>().enabled = true;
        cameraPlayer.GetComponent<AudioListener>().enabled = false;
    }

    public void Sair()
    {
        if (!dentroDoCarro)
        {
            return;
        }
        

        dentroDoCarro = false;

        player.GetComponent<MovimentarPersonagem>().enabled = true;


        if (pontoSaida != null)
        {
            player.transform.position = pontoSaida.position;
            player.transform.rotation = pontoSaida.rotation;
        }
        else
        {
            player.transform.position = carro.transform.position + carro.transform.right * 2f;
        }

        player.SetActive(true);

        carroController.enabled = false;
        canhao.GetComponent<CanhaoCarro>().enabled = false;
        cameraCarro.enabled = false;
        cameraCarro.GetComponent<AudioListener>().enabled = false;
        cameraCarro.GetComponent<CameraCarro>().enabled = false;
        cameraPlayer.GetComponent<AudioListener>().enabled = true;
    }

    public bool EstaDentro()
    {
        return dentroDoCarro;
    }
}
