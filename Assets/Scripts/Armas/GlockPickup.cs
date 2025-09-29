using UnityEngine;

public class GlockPickup : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        GameObject playerGlock = GameObject.FindWithTag("Arma");
        if (playerGlock != null)
        {
            playerGlock.SetActive(true);
            Debug.Log("Player agora tem a Glock!");
        }

        Destroy(gameObject);
    }
}