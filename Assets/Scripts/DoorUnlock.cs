using SunTemple;
using UnityEngine;

public enum DoorUnlockType { FirstDoor, BossDoor }

public class DoorUnlock : MonoBehaviour
{
    public DoorUnlockType unlockType;
    private Door door;
    private Outline outline;
    [Header("Som da Porta")]
    public AudioClip unlockSound;
    private AudioSource audioSrc;


    private void Awake()
    {
        door = GetComponent<Door>();
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
        if (audioSrc == null)
            audioSrc = gameObject.AddComponent<AudioSource>();
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
            if (unlockSound != null && audioSrc != null)
                audioSrc.PlayOneShot(unlockSound);
        }
    }
}
