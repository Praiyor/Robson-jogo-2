using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerStatus.Instance.AtualizarVida(+50);
    }

}
