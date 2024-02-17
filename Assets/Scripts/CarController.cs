using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    #region DEFINITIONS

    /* WHEEL CONFIGURATION */

    public enum Axle
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject WheelModel;
        public WheelCollider _WheelCollider;
        public Axle _Axle;
    }

    public List<Wheel> _Wheels;

    /* SENSITIVITY AND PHYSICS */

    [Header("Acceleration and Torque")]
    public float _MaxAcceleration;
    public float _BrakeAcceleration;
    private float _TorqueMultiplier = 941f;

    [Header("Steering Configuration")]
    public float TurnSensitivity = 1f;
    public float MaxSteeringAngle = 30.0f;

    /* PLAYER INPUTS */

    [Header("Player Inputs")]
    [SerializeField] float MoveInput;
    [SerializeField] float SteerInput;


    /* GAMEOBJECT COMPONENT REFERENCES */

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
        SteerTruck();
    }

    private void GetPlayerInputs()
    {
        MoveInput = Input.GetAxis("Vertical");
        SteerInput = Input.GetAxis("Horizontal");
    }

    private void RotateWheels()
    {
        foreach (Wheel wheel in _Wheels)
        {
            if(wheel._WheelCollider != null)
            {
                wheel._WheelCollider.motorTorque = MoveInput * _TorqueMultiplier *_MaxAcceleration * Time.deltaTime;
            }
        }
    }

    private void SteerTruck()
    {
        foreach (Wheel wheel in _Wheels)
        {
            if(wheel._Axle == Axle.Front)
            {
                float steerAngle = SteerInput * TurnSensitivity * MaxSteeringAngle;
                wheel._WheelCollider.steerAngle = Mathf.Lerp(wheel._WheelCollider.steerAngle, steerAngle, 1f);
            }
        }
    }
}
