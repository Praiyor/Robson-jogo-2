using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineGlock : MonoBehaviour, IPegavel
{

    private GameObject arma;
    private CanhaoCarro armaCarro;

    
    public void Pegar()
    {
        arma = GameObject.FindWithTag("Arma");
        arma.GetComponent<Glock>().AddCarregador();
    }

}
