using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SomMotor : MonoBehaviour
{
    private AudioSource motorAudio;
    private Rigidbody rb;

    [Header("Configuração do som do motor")]
    public float pitchMin = 0.8f;   // motor parado
    public float pitchMax = 2.0f;   // motor acelerado
    public float volumeMin = 0.2f;
    public float volumeMax = 1.0f;
    public float velocidadeMax = 50f; // em m/s (~180 km/h)

    void Start()
    {
        motorAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        motorAudio.loop = true;
        motorAudio.playOnAwake = false;
        
    }

    void Update()
    {
        float velocidade = rb.velocity.magnitude; // velocidade real em m/s
        float t = Mathf.InverseLerp(0f, velocidadeMax, velocidade);

        motorAudio.pitch = Mathf.Lerp(pitchMin, pitchMax, t);
        motorAudio.volume = Mathf.Lerp(volumeMin, volumeMax, t);
    }
    public void LigarMotor()
    {
        if (!motorAudio.isPlaying)
            motorAudio.Play();
    }

    public void DesligarMotor()
    {
        if (motorAudio.isPlaying)
            motorAudio.Stop();
    }
}
