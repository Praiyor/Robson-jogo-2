using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovimentarPersonagem : MonoBehaviour
{
    public CharacterController controle;
    public float velocidade = 6f;
    public float alturaPulo = 6f;
    public float gravidade = -20f;
    public AudioClip somPulo;
    public AudioClip somPasso;
    private AudioSource audioSrc;

    public Transform checaChao;
    public float raioEsfera = 0.4f;
    public LayerMask chaoMask;
    public bool estaNoChao;

    private Transform cameraTransform;
    private bool estahAbaixado = false;
    private bool levantarBloqueado;
    public float alturaLevantado, alturaAbaixado, posicaoCameraEmPe, posicaoCameraAbaixado;
    private int pontos = 0;
    public Text textoPontos;
    private PlayerStatus playerStatus;
    public GameObject arma;

    Vector3 velocidadeCai;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        audioSrc = GetComponent<AudioSource>();
        playerStatus = GetComponent<PlayerStatus>();
        arma.SetActive(false);

        if (textoPontos != null)
        {
            textoPontos.text = "Pontos: " + pontos;
        }
    }

    void Update()
    {
        if (playerStatus != null && playerStatus.vida <= 0)
        {
            return;
        }

        estaNoChao = Physics.CheckSphere(checaChao.position, raioEsfera, chaoMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;

        controle.Move(mover * velocidade * Time.deltaTime);

        if(estaNoChao && mover.magnitude > 0.1f)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.clip = somPasso;
                audioSrc.loop = true;
                audioSrc.Play();
            }
        }else
        {
            if(audioSrc.clip == somPasso && audioSrc.isPlaying)
            {
                audioSrc.Stop();
                audioSrc.loop = false;
            }
        }

            ChecarBloqueioAbaixo();

        if (!levantarBloqueado && estaNoChao && Input.GetButtonDown("Jump"))
        {
            velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
            audioSrc.clip = somPulo;
            audioSrc.loop = false;
            audioSrc.Play();
        }

        if (!estaNoChao)
        {
            velocidadeCai.y += gravidade * Time.deltaTime;
        }

        controle.Move(velocidadeCai * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AgacharLevantar();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checaChao.position, raioEsfera);
    }

    private void AgacharLevantar()
    {
        if( levantarBloqueado || estaNoChao == false)
        {
            return;
        }

        estahAbaixado = !estahAbaixado;
        if (estahAbaixado)
        {
            controle.height = alturaAbaixado;
            cameraTransform.localPosition = new Vector3(0, posicaoCameraAbaixado, 0);
        } else
        {
            controle.height = alturaLevantado;
            cameraTransform.localPosition = new Vector3(0, posicaoCameraEmPe, 0);
        }
    }

    public void PegarArma()
    {
        arma.SetActive(true);
        arma.GetComponent<Glock>().enabled = true;
        arma.GetComponent<Glock>().AtualizarTextoMunicao();
    }

    private void ChecarBloqueioAbaixo()
    {
        //Debug.DrawRay(cameraTransform.position, Vector3.up * 1.1f, Color.red);
        RaycastHit hit;
        levantarBloqueado = Physics.Raycast(cameraTransform.position, Vector3.up, out hit, 1.1f);
    }

}
