// Author: Vignesh Manikandan
// Date: 17 Feb 2024


using DG.Tweening;
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

    [SerializeField] bool IsDoorOpen;


    /* GAMEOBJECT COMPONENT REFERENCES */

    private Rigidbody _rb;

    [Header("GameObject References")]
    [SerializeField] Transform DriverDoor;

    [SerializeField] Vector3 DoorRotation;

    #endregion

    #region FRAME CYCLES

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        IsDoorOpen = false;
    }

    void Update()
    {
        GetPlayerInputs();
        RotateWheels();
        DoorToggleInputs();
    }

    void LateUpdate()
    {
        AccelerateTruck();
        SteerTruck();
        BrakeSystem();
    }

    #endregion

    #region INPUT VALUES

    private void GetPlayerInputs()
    {
        MoveInput = Input.GetAxis("Vertical");
        SteerInput = Input.GetAxis("Horizontal");
    }

    private void DoorToggleInputs()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!IsDoorOpen)
            {
                ToggleDoors(true);

            }

            else
            {
                ToggleDoors(false);
            }
        }
    }

    #endregion

    #region VEHICLE MOVEMENTS

    private void AccelerateTruck()
    {
        foreach (Wheel wheel in _Wheels)
        {
            if (wheel._WheelCollider != null)
            {
                wheel._WheelCollider.motorTorque = MoveInput * _TorqueMultiplier * _MaxAcceleration * Time.deltaTime;
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

    private void BrakeSystem()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (Wheel wheel in _Wheels)
            {
                if (wheel._WheelCollider != null)
                {
                    wheel._WheelCollider.brakeTorque = _TorqueMultiplier * _BrakeAcceleration * Time.deltaTime;
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

    #endregion

    #region WHEEL ANIMATION

    private void RotateWheels()
    {
        foreach (Wheel wheel in _Wheels)
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

    #endregion

    #region DOOR ANIMATION

    private void ToggleDoors(bool val)
    {
        if(DriverDoor != null)
        {
            if (val)
            {
                DriverDoor.DOLocalRotate(DoorRotation, 1.5f, RotateMode.LocalAxisAdd);
                IsDoorOpen = true;
            }

            else
            {
                DriverDoor.DOLocalRotate(DoorRotation * -1f, 1.5f, RotateMode.LocalAxisAdd);
                IsDoorOpen = false;
            }
        }
        
    }

    #endregion
}