using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineGlock : MonoBehaviour, IPegavel
{

    private GameObject arma;
    private CanhaoCarro armaCarro;

    
    public void Pegar()
    {
        var entrarCarro = GameObject.FindWithTag("Entrar").GetComponent<EntrarCarro>();

        if(entrarCarro.EstaDentro())
        {
            PlayerStatus.Instance.municaoCarro = PlayerStatus.Instance.municaoMaxCarro;
            PlayerStatus.Instance.AtualizarMunicao(true);
        }else
        {
            arma = GameObject.FindWithTag("Arma");
            arma.GetComponent<Glock>().AddCarregador();
        }
            
    }

}
