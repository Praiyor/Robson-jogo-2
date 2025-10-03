using System.Collections;
using UnityEngine;

public class EntrarCarro : MonoBehaviour, IEntrar
{
    public GameObject player;
    public GameObject carro;
    public Camera cameraPlayer;
    public Camera cameraCarro;
    public GameObject canhao;

    [Header("Referências para animação")]
    public Animator portaEsquerdaAnimator;
    public Transform pontoCameraDentro;

    [Header("Áudio da Porta")]
    public AudioSource portaAudioSource;
    public AudioClip somAbrir;
    public AudioClip somFechar;

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

    public void Entrar()
    {
        if (dentroDoCarro) return;
        StartCoroutine(SequenciaEntrada());
    }

    private IEnumerator SequenciaEntrada()
    {
        dentroDoCarro = true;

        
        if (portaEsquerdaAnimator != null)
        {
            portaEsquerdaAnimator.SetTrigger("Abrir");
        }
        if (portaAudioSource != null && somAbrir != null)
        {
            portaAudioSource.PlayOneShot(somAbrir);
        }

        
        float duracao = 1.0f;
        float tempo = 0f;
        Vector3 posInicial = cameraPlayer.transform.position;
        Quaternion rotInicial = cameraPlayer.transform.rotation;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;

            cameraPlayer.transform.position = Vector3.Lerp(posInicial, pontoCameraDentro.position, t);
            cameraPlayer.transform.rotation = Quaternion.Slerp(rotInicial, pontoCameraDentro.rotation, t);

            yield return null;
        }

        
        if (portaEsquerdaAnimator != null)
        {
            portaEsquerdaAnimator.SetTrigger("Fechar");
        }
        if (portaAudioSource != null && somFechar != null)
        {
            portaAudioSource.PlayOneShot(somFechar);
        }

        yield return new WaitForSeconds(0.5f);

        
        player.SetActive(false);
        carroController.enabled = true;
        canhao.GetComponent<CanhaoCarro>().enabled = true;
        carro.layer = LayerMask.NameToLayer("IgnorePlayercast");

        cameraCarro.enabled = true;
        cameraCarro.tag = "MainCamera";
        cameraCarro.GetComponent<AudioListener>().enabled = true;
        cameraCarro.GetComponent<CameraCarro>().enabled = true;
        cameraPlayer.GetComponent<AudioListener>().enabled = false;

        SomMotor somMotor = carro.GetComponent<SomMotor>();
        if (somMotor != null)
        {
            somMotor.LigarMotor();
        }

        SomRodas somRodas = carro.GetComponent<SomRodas>();
        if (somRodas != null)
        {
            somRodas.LigarRodas();
        }
    }

    public bool EstaDentro()
    {
        return dentroDoCarro;
    }
}
