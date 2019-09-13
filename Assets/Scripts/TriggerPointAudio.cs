using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPointAudio : MonoBehaviour
{
    [SerializeField]
    private TriggerPoint triggerPoint;

    [SerializeField]
    private AudioSource hitSource;

    [SerializeField]
    private AudioSource scrapeSource;

    [SerializeField]
    private float velocity = 0f;

    private void OnEnable()
    {
        this.triggerPoint.TriggerEnter += TriggerPoint_TriggerEnter;
    }

    private void OnDisable()
    {
        this.triggerPoint.TriggerEnter -= TriggerPoint_TriggerEnter;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void TriggerPoint_TriggerEnter(Collider other)
    {
        float minVelocity = 0f;
        float maxVelocity = 30f;


        float volumeRatio = Mathf.InverseLerp(minVelocity, maxVelocity, this.triggerPoint.Velocity);

        hitSource.volume = volumeRatio;

        if (volumeRatio > 0.4f)
            Debug.Log(volumeRatio);


        hitSource.Play();
    }
}
