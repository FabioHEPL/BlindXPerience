using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip sound;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private ScannerEffectDemo scannerEffect;

    private void Awake()
    {
        if (this.source == null)
            this.source = this.GetComponent<AudioSource>();
    }

    [SerializeField]
    private bool isBeingPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.scannerEffect = this._camera.gameObject.AddComponent<ScannerEffectDemo>();
        this.scannerEffect.EffectMaterial = Resources.Load<Material>("ScannerEffect");
        Debug.Log(this.scannerEffect.EffectMaterial);
        this.scannerEffect.ScannerOrigin = this.transform;
        

        this.source.clip = sound;




     //   this.sound.PCMReaderCallback = myFunc;
    }

    private void myFunc(float[] data)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PlaySound();


        if (this.isBeingPlayed)
        {         



        }
    }

    private void PlaySound()
    {
        PlayVibration();
        this.source.Play();
        this.scannerEffect.Play();
    }

    private void PlayVibration()
    {
        OVRHapticsClip vibration = new OVRHapticsClip(this.sound);
        OVRHaptics.LeftChannel.Preempt(vibration);
        
       // OVRHaptics.LeftChannel.Mix()

//        vibration.
    }
}
