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
        RotateWheels();
    }

    void LateUpdate()
    {
        AccelerateTruck();
        SteerTruck();
        BrakeSystem();
    }

    private void GetPlayerInputs()
    {
        MoveInput = Input.GetAxis("Vertical");
        SteerInput = Input.GetAxis("Horizontal");
    }

    private void AccelerateTruck()
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
            if (wheel._WheelCollider != null)
            {
                if (wheel._Axle == Axle.Front)
                {
                    float steerAngle = SteerInput * TurnSensitivity * MaxSteeringAngle;
                    wheel._WheelCollider.steerAngle = Mathf.Lerp(wheel._WheelCollider.steerAngle, steerAngle, 1f);
                }
            }
        }
    }

    private void RotateWheels()
    {
        foreach(Wheel wheel in _Wheels)
        {
            Quaternion rot;
            Vector3 pos;

            wheel._WheelCollider.GetWorldPose(out pos, out rot);

            if (wheel.WheelModel != null)
            {
                 wheel.WheelModel.transform.rotation = rot;
            }
        }
    }

    private void BrakeSystem()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            foreach(Wheel wheel in _Wheels)
            {
                if(wheel._WheelCollider != null)
                {
                    wheel._WheelCollider.brakeTorque = 941f * _BrakeAcceleration * Time.deltaTime;
                }
            }
        }

        else
        {
            foreach (Wheel wheel in _Wheels)
            {
                if (wheel._WheelCollider != null)
                {
                    wheel._WheelCollider.brakeTorque = 0;
                }
            }
        }
    }
}
