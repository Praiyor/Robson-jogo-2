using SunTemple;
using UnityEngine;

public enum DoorUnlockType { FirstDoor, BossDoor }

public class DoorUnlock : MonoBehaviour
{
    public DoorUnlockType unlockType;
    private Door door;
    private Outline outline;

    [Header("Áudio da Porta")]
    public AudioSource audioSource;     
    public AudioClip somDestrancar;    

    private void Awake()
    {
        door = GetComponent<Door>();
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        if (unlockType == DoorUnlockType.FirstDoor)
        {
            GameManager.OnFirstDoorUnlocked += UnlockDoor;
        }
        else if (unlockType == DoorUnlockType.BossDoor)
        {
            GameManager.OnBossKilled += UnlockDoor;
        }
        if (door != null)
        {
            door.OnDoorOpened += HandleDoorOpened;
        }
    }

    private void OnDisable()
    {
        if (unlockType == DoorUnlockType.FirstDoor)
        {
            GameManager.OnFirstDoorUnlocked -= UnlockDoor;
        }
        else if (unlockType == DoorUnlockType.BossDoor)
        {
            GameManager.OnBossKilled -= UnlockDoor;
        }
        if (door != null)
        {
            door.OnDoorOpened -= HandleDoorOpened;
        }
    }
    private void HandleDoorOpened()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    private void UnlockDoor()
    {
        if (door != null)
        {
            door.IsLocked = false;

            if (outline != null)
            {
                outline.enabled = true;
            }

           
            if (audioSource != null && somDestrancar != null)
            {
                audioSource.PlayOneShot(somDestrancar);
            }
        }
    }
}