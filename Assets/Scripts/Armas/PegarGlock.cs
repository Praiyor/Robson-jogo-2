using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarGlock : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<MovimentarPersonagem>().PegarArma();
    }
}
