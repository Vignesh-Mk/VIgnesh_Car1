using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Helper : MonoBehaviour
{
    #region DEFINITIONS

    // GAMEOBJECT COMPONENT REFERENCES

    [Header("GameObject Component References")]
    [SerializeField] TMP_Text Textbox;
    [SerializeField] Transform EndPosition;

    // GAME DATA

    [Header("Game Data")]
    [SerializeField] List<string> TextContents;

    [Range(0, 5)][SerializeField] int StepCount = 0;

    #endregion

    #region FRAME CYCLES

    // Start is called before the first frame update
    void Start()
    {
        InitializeTextContent(StepCount);
    }

    // Update is called once per frame
    void Update()
    {
        //TestFunction();
    }

    #endregion

    #region PUBLIC FUNCTIONS

    public void InitializeTextContent(int StepCount)
    {
        Textbox.text = TextContents[StepCount];

        if (StepCount == 4)
        {
            SwapUIPosition();
        }
    }

    #endregion

    #region PRIVATE FUNCTIONS

    private void SwapUIPosition()
    {
        gameObject.transform.position = EndPosition.position;
        gameObject.transform.rotation = EndPosition.rotation;
    }

    #endregion

    #region TEST CASE FUNCTIONS

    public void TestFunction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StepCount++;

            if (StepCount > 5)
            {
                StepCount = 5;
            }

            InitializeTextContent(StepCount);
        }
    }

    #endregion

}
