using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
    public EventSound3D eventSound3DPrefab;
    public AudioClip[] footstepAudio;

    private UnityAction<Vector3> footstepEventListener;
    // Start is called before the first frame update
    void Awake()
    {
        footstepEventListener = new UnityAction<Vector3>(FootstepEventHandler);
    }

    // Update is called once per frame
    void OnEnable()
    {
        EventManager.StartListening<FootstepEvent, Vector3>(footstepEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<FootstepEvent, Vector3>(footstepEventListener);
    }

    void FootstepEventHandler(Vector3 pos) 
    {
            EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
            snd.audioSrc.clip = this.footstepAudio[Random.Range(0, footstepAudio.Length)];
            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;
            snd.audioSrc.Play();
    }

}
