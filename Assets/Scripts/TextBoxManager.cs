using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public TextAsset textFile;
    public string[] textLines;
    public GameObject textBox;
    public Text theText;

    public int currentLine;
    public int endAtLine;

    public GameObject firepoint;
    RectTransform m_RectTransform;
    float m_XAxis, m_YAxis;

    void Start()
    {

        m_RectTransform = GetComponent<RectTransform>();

        //m_XAxis = firepoint.transform.position.x - 50f;
        //m_YAxis = firepoint.transform.position.y + 50f;

        m_RectTransform.anchoredPosition = new Vector2(m_XAxis, m_YAxis);

        if (textFile != null)
        {
            textLines = textFile.text.Split('\n');
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    private void Update()
    {
        theText.text = textLines[currentLine];
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }
        if (currentLine > endAtLine)
        {
            textBox.SetActive(false);
        }

        
    }
}
