using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using System;

public class TriggerPoint : MonoBehaviour
{
    public event Action<Collider> TriggerStay;
    public event Action<Collider> TriggerEnter;

    [SerializeField]
    private float frequency = 1;

    [SerializeField]
    private float amplitude = 1;

    [SerializeField]
    private OculusHaptics haptics;

    [SerializeField]
    private float velocity = 0f;
    public float Velocity
    {
        get { return this.velocity; }        
    }

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Vector3 previousPosition = Vector3.zero;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.previousPosition = this.transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        this.velocity = this._rigidbody.GetPointVelocity(this.transform.position).magnitude;
       // this.velocity = ((this.transform.position - this.previousPosition) / Time.deltaTime).magnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cane"))
            return;

        //Debug.Log("Vibrating...");
        TriggerStay?.Invoke(other);
        //haptics.Vibrate(VibrationForce.Hard);
        //OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.All);
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke(other);
    }
}
