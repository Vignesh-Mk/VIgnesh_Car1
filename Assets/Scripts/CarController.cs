using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    #region DEFINITIONS

    public enum Axle  // Differentiate the Axle side
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel  // To add wheels into list
    {
        public GameObject WheelModel;
        public WheelCollider _WheelCollider;
        public Axle _Axle;
    }

    public float _MaxAcceleration;
    public float _BrakeAcceleration;
    private float _TorqueMultiplier = 941f;

    public List<Wheel> _Wheels;

    [SerializeField] float MoveInput;

    private Rigidbody _rb;

    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetPlayerInputs();
    }

    void LateUpdate()
    {
        RotateWheels();
    }

    void GetPlayerInputs()
    {
        MoveInput = Input.GetAxis("Vertical");
    }

    void RotateWheels()
    {
        foreach (Wheel wheel in _Wheels)
        {
            if(wheel._WheelCollider != null)
            {
                wheel._WheelCollider.motorTorque = MoveInput * _TorqueMultiplier *_MaxAcceleration * Time.deltaTime;
            }
        }
    }
}
