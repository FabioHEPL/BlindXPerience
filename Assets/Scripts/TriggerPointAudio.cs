using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPointAudio : CustomAudio
{
    [SerializeField]
    private TriggerPoint triggerPoint;

    //[SerializeField]
    //private AudioSource hitSource;

    //[SerializeField]
    //private AudioSource scrapeSource;

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



    // Update is called once per frame
    void Update()
    {
        
    }


    private void TriggerPoint_TriggerEnter(Collider other)
    {
        float minVelocity = 0f;
        float maxVelocity = 18f;
        Debug.Log(other);

        float volumeRatio = Mathf.InverseLerp(minVelocity, maxVelocity, this.triggerPoint.Velocity);

        this.source.volume = volumeRatio;


        this.Play();
    }
}
