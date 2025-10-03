using UnityEngine;

public class SomPorta : MonoBehaviour
{
    public AudioClip somAbrir;
    public AudioClip somFechar;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TocarSomAbrir()
    {
        if (somAbrir != null)
            audioSource.PlayOneShot(somAbrir);
    }

    public void TocarSomFechar()
    {
        if (somFechar != null)
            audioSource.PlayOneShot(somFechar);
    }
}
