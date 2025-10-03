using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SomRodas : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioRodas;
    private AudioSource audioDerrapagem;

    [Header("Áudio das rodas")]
    public AudioClip somRodas;
    public float volumeRodasMin = 0.1f;
    public float volumeRodasMax = 0.8f;
    public float velocidadeMax = 50f;

    [Header("Áudio da derrapagem")]
    public AudioClip somDerrapagem;
    public float intensidadeCurvaMin = 0.5f;
    public float volumeDerrapagem = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        audioRodas = gameObject.AddComponent<AudioSource>();
        audioRodas.clip = somRodas;
        audioRodas.loop = true;
        audioRodas.playOnAwake = false;
        audioRodas.volume = 0f;

        
        audioDerrapagem = gameObject.AddComponent<AudioSource>();
        audioDerrapagem.clip = somDerrapagem;
        audioDerrapagem.loop = true;
        audioDerrapagem.playOnAwake = false;
        audioDerrapagem.volume = 0f;
    }

    void Update()
    {
        if (!audioRodas.isPlaying || !audioDerrapagem.isPlaying) return;

        
        float velocidade = rb.velocity.magnitude;
        float t = Mathf.InverseLerp(0f, velocidadeMax, velocidade);
        audioRodas.volume = Mathf.Lerp(volumeRodasMin, volumeRodasMax, t);

        
        float curva = Mathf.Abs(Input.GetAxis("Horizontal"));
        if (curva > intensidadeCurvaMin && velocidade > 5f)
        {
            audioDerrapagem.volume = volumeDerrapagem * curva;
        }
        else
        {
            audioDerrapagem.volume = 0f;
        }
    }

    public void LigarRodas()
    {
        if (!audioRodas.isPlaying) audioRodas.Play();
        if (!audioDerrapagem.isPlaying) audioDerrapagem.Play();
    }

    public void DesligarRodas()
    {
        if (audioRodas.isPlaying) audioRodas.Stop();
        if (audioDerrapagem.isPlaying) audioDerrapagem.Stop();
    }
}
