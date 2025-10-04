using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerVitoria : MonoBehaviour
{
    private bool venceu = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectou: " + other.name);
        if (venceu) return;

        
        GameObject rootObj = other.attachedRigidbody
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        
        if (rootObj.CompareTag("Entrar"))
        {
            venceu = true;

            Debug.Log("Vitória! Entrou na área com: " + rootObj.name);

            
            if (PlayerStatus.Instance != null)
            {
                PlayerPrefs.SetInt("PontuacaoFinal", PlayerStatus.Instance.GetPontuacao());
                PlayerPrefs.Save();
            }

            StartCoroutine(TransicaoVitoria());
        }
    }

    private System.Collections.IEnumerator TransicaoVitoria()
    {
        yield return new WaitForSeconds(1.2f);

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("VictoryGame");
    }
}
