using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanhaoCarro : MonoBehaviour
{
    private bool estahAtirando;
    private RaycastHit hit;

    [Header("Referências")]
    public GameObject posTiro;
    public GameObject efeitoTiro;
    public GameObject explosaoPrefab;
    public LayerMask layerMaskRaycast = ~0;

    [Header("Parâmetros do Tiro")]
    public float alcance = 100f;
    public float cooldown = 1.5f;
    public int danoBase = 50;
    public float raioExplosao = 5f;

    [Header("Áudio")]
    public AudioClip[] clips;
    private AudioSource somTiro;

    public Camera cameraCarro;


    void Start()
    {
        estahAtirando = false;
        somTiro = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(PlayerStatus.Instance != null)
        {
            PlayerStatus.Instance.AtualizarMunicao(true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (!estahAtirando)
            {
                if (PlayerStatus.Instance.municaoCarro > 0)
                {
                    PlayerStatus.Instance.municaoCarro--;
                    somTiro.clip = clips[0];
                    estahAtirando = true;
                    StartCoroutine(Atirando());
                }
                else
                {
                    somTiro.clip = clips[1];
                    somTiro.time = 0;
                    somTiro.Play();
                }
            }
        }
    }

    IEnumerator Atirando()
    {
        float screenX = Screen.width / 2;
        float screenY = Screen.height / 2;
        Ray ray = cameraCarro.ScreenPointToRay(new Vector3(screenX, screenY, 0));

        somTiro.time = 0;
        somTiro.Play();

        GameObject efeitoTiroObj = null;
        if (efeitoTiro != null && posTiro != null)
        {
            efeitoTiroObj = Instantiate(efeitoTiro, posTiro.transform.position, posTiro.transform.rotation);
            efeitoTiroObj.transform.parent = posTiro.transform;
        }

        Vector3 pontoExplosao;
        if (Physics.Raycast(ray, out hit, alcance))
        {
            pontoExplosao = hit.point;
        }
        else
        {
            pontoExplosao = ray.origin + ray.direction * alcance;
        }

        GameObject explosaoObj = null;
        if (explosaoPrefab != null)
        {
            explosaoObj = Instantiate(explosaoPrefab, pontoExplosao, Quaternion.identity);
            Destroy(explosaoObj, 5f);
        }

        Collider[] colliders = Physics.OverlapSphere(pontoExplosao, raioExplosao);
        foreach (Collider col in colliders)
        {

            ILevarDano levar = col.GetComponent<ILevarDano>();
            if (levar != null)
            {
                levar.LevarDano(danoBase);
            }
        }

        yield return new WaitForSeconds(0.3f);
        if (efeitoTiroObj != null)
        {
            Destroy(efeitoTiroObj);
        }

        yield return new WaitForSeconds(cooldown);
        estahAtirando = false;
    }

}
