using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Toggle : MonoBehaviour
{
    [SerializeField] bool IsDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        IsDoorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
         
    }    
}
