using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    [SerializeField] UI_Helper DialogueBox;

    [SerializeField]Step step;

    [SerializeField] UnityEvent EventList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Truck")
        {
            switch(step)
            {
                case Step.Mark_The_Indicator:
                    DialogueBox.InitializeTextContent(2);
                    EventList.Invoke();
                    gameObject.SetActive(false);
                    break;

                case Step.Park_The_Vehicle:
                    DialogueBox.InitializeTextContent(3);
                    EventList.Invoke();
                    gameObject.SetActive(false);
                    break;
            }
        }
    }

    enum Step
    {
        Mark_The_Indicator,
        Park_The_Vehicle
    }
}
