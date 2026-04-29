using UnityEngine;


public enum soundtype{
    monkeywalk,
    knightwalk, 
    kingwalk,
    bite,
    bookpickup
}
[RequireComponent(typeof (AudioSource))]
public class Soundmanager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static Soundmanager instance;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();   
    }
    private void Start()
    {
    }

    // Update is called once per frame
    public static void playSound(soundtype sound, float volume = 1)
    {
        Debug.Log("Playing sound: " + sound);
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
