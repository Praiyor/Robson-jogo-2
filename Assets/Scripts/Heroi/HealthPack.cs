using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<MovimentarPersonagem>().AtualizarVida(+50);
    }

}
