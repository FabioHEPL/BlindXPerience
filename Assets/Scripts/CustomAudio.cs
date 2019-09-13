using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudio : MonoBehaviour
{
    [SerializeField]
    protected AudioClip sound;

    [SerializeField]
    protected AudioSource source;

    [SerializeField]
    protected Camera _camera;

    [SerializeField]
    protected ScannerEffectDemo scannerEffect;

    [SerializeField]
    protected Transform player;

    [SerializeField]
    protected AnimationCurve audioSourceCurve;

    protected void Awake()
    {
        if (this.source == null)
            this.source = this.GetComponent<AudioSource>();

        if (this.player == null)
            this.player = GameObject.FindGameObjectWithTag("Player").transform;

        if (this._camera == null)
            this._camera = GameObject.FindGameObjectWithTag("OVR Camera").GetComponent<Camera>();

        this.audioSourceCurve = this.source.GetCustomCurve(AudioSourceCurveType.CustomRolloff);
    }

    // Start is called before the first frame update
    protected void Start()
    {
        this.scannerEffect = this._camera.gameObject.AddComponent<ScannerEffectDemo>();
        this.scannerEffect.EffectMaterial = Resources.Load<Material>("ScannerEffect");
        this.scannerEffect.ScannerOrigin = this.transform;
        this.source.clip = sound;
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.Play();
        }   
    }

    public void Play()
    {
        if (this.source.isPlaying)
            return;

        this.source.Play();
        this.scannerEffect.Play();
        StartCoroutine(SynchronizeVibration(0.01f));
    }


    protected IEnumerator SynchronizeVibration(float seconds)
    {
        while (this.source.isPlaying)
        {
            int sampleRate = (int)(source.clip.frequency * seconds);            

            float[] samples = new float[sampleRate * this.sound.channels];

            //Debug.Log(samples.Length + " " + this.sound.channels);
            this.source.GetOutputData(samples, this.sound.channels);

            // calculate spatialized volume
            float distanceFromSourceToPlayer = Vector3.Distance(this.source.transform.position, this.player.position);
            float distanceFromSourceToPlayerNormalized = Mathf.InverseLerp(this.source.minDistance, this.source.maxDistance, distanceFromSourceToPlayer);
            float spatializedVolume = audioSourceCurve.Evaluate(distanceFromSourceToPlayerNormalized);

            // multiply each audio sample by calculated volume
            //for (int i = 0; i < samples.Length; i++)
            //{
            //    samples[i] *= spatializedVolume * this.source.volume;
            //}
            // maybe we don't need it

            AudioClip audioSection = AudioClip.Create("portion", samples.Length, source.clip.channels, source.clip.frequency, false);
            audioSection.SetData(samples, 0);


            // multiply by 
            OVRHapticsClip vibration = new OVRHapticsClip(audioSection);
            OVRHaptics.LeftChannel.Mix(vibration);
            OVRHaptics.RightChannel.Mix(vibration);

            //Debug.Log($"Distance To Player : {distanceFromSourceToPlayer} | Spatialized volume : {spatializedVolume} | Samples : {samples.Length}");


            yield return new WaitForSecondsRealtime(seconds);
        }
    }
}
