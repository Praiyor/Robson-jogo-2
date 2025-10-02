using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoComum : MonoBehaviour, ILevarDano
{
    private NavMeshAgent agente;
    private GameObject player;
    private Animator anim;
    public float distanciaDoAtaque = 2.0f;
    public int vida = 50;
    public AudioClip somMorte;
    public AudioClip somPasso;
    private AudioSource audioSrc;
    private FieldOfView fov;
    private PatrulharAleatorio pal;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        fov = GetComponent<FieldOfView>();
        pal = GetComponent<PatrulharAleatorio>();
    }

    void Update()
    {

        if (vida <= 0)
        {
            Morrer();
            return;
        }

        if (fov.podeVerPlayer)
        {
            VaiAtrasJogador();
        }
        else
        {
            anim.SetBool("pararAtaque", true);
            CorrigirRigiSair();
            agente.isStopped = false;
            pal.Andar();
        }
    }

    private void VaiAtrasJogador()
    {
        GameObject alvo = fov.alvoAtual;
        
        bool alvoCarro = alvo.CompareTag("CorpoCarro");

        if (alvoCarro)
        {
            EntrarCarro entrar = alvo.GetComponentInParent<EntrarCarro>();
            if(entrar == null || !entrar.EstaDentro())
            {
                return;
            }

            Vector3 posAlvo = alvo.transform.position;
            BoxCollider box = alvo.GetComponent<BoxCollider>();
            if (box != null)
            {
                posAlvo = box.ClosestPoint(transform.position);
            }

            float distanciaAlvo = Vector3.Distance(transform.position, posAlvo);

            if (distanciaAlvo < 5.0f)
            {
                agente.isStopped = true;
                anim.SetTrigger("ataque");
                anim.SetBool("podeAndar", false);
                anim.SetBool("pararAtaque", false);
                CorrigirRigiEntrar();
            }
            else
            {
                anim.SetBool("podeAndar", true);
                anim.SetBool("pararAtaque", true);
                CorrigirRigiSair();
                agente.isStopped = false;
                agente.SetDestination(posAlvo);
                anim.ResetTrigger("ataque");
            }

        }

        float distanciaDoPlayer = Vector3.Distance(transform.position, alvo.transform.position);

        if (distanciaDoPlayer < distanciaDoAtaque)
        {
            agente.isStopped = true;

            anim.SetTrigger("ataque");
            anim.SetBool("podeAndar", false);
            anim.SetBool("pararAtaque", false);
            CorrigirRigiEntrar();
        }

        if (distanciaDoPlayer >= distanciaDoAtaque + 3)
        {
            anim.SetBool("pararAtaque", true);
            CorrigirRigiSair();
        }
        if (anim.GetBool("podeAndar"))
        {
            agente.isStopped = false;
            agente.SetDestination(alvo.transform.position);
            anim.ResetTrigger("ataque");
        }
    }


    private void CorrigirRigiEntrar()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void CorrigirRigiSair()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
        agente.isStopped = true;
        anim.SetTrigger("levouTiro");
        anim.SetBool("podeAndar", false);
    }

    private void Morrer()
    {
        GameManager.EnemyKilled(true);
        if (PlayerStatus.Instance != null)
        {
            PlayerStatus.Instance.AtualizarPontuacao(+10);
        }
        audioSrc.clip = somMorte;
        audioSrc.Play();

        agente.isStopped = true;
        anim.SetBool("podeAndar", false);
        anim.SetBool("pararAtaque", true);

        anim.SetBool("morreu", true);

        this.enabled = false;

    }

    public void DarDano()
    {
        if(PlayerStatus.Instance != null)
        {
            PlayerStatus.Instance.AtualizarVida(-10);
        }
        
    }

    public void Passo()
    {
        audioSrc.PlayOneShot(somPasso, 0.05f);
    }
}
