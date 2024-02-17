using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Helper : MonoBehaviour
{
    [SerializeField] TMP_Text Textbox;

    [SerializeField] List<string> TextContents;

    [SerializeField] Transform EndPosition;

    [Range(0, 5)][SerializeField] int StepCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTextContent(StepCount);
    }

    // Update is called once per frame
    void Update()
    {
        TestFunction();
    }

    public void InitializeTextContent(int StepCount)
    {
        Textbox.text = TextContents[StepCount];

        if (StepCount == 4)
        {
            SwapUIPosition();
        }
    }

    public void SwapUIPosition()
    {
        gameObject.transform.position = EndPosition.position;
        gameObject.transform.rotation = EndPosition.rotation;
    }

    public void TestFunction()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StepCount++;

            if(StepCount > 5)
            {
                StepCount = 5;
            }

            InitializeTextContent(StepCount);
        }
    }
}
