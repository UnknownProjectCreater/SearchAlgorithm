using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinearSearchVisualization : MonoBehaviour
{
    public TMP_InputField searchbValue;
    public Button visualizationButton;
    public Material mat;

    List<GameObject> nodesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject myObject = GameObject.Find("nodes");
        nodesList = myObject.gameObject.GetComponent<LineNodeRange>().nodesList;
    }

    // Update is called once per frame
    void Update()
    {
        VisualizationButton(searchbValue);
    }

    public void Initialized()
    {
        for (int i = 0; i < nodesList.Count; i++)
        {
            Color buttonColor = nodesList[i].GetComponent<SpriteRenderer>().color;
            buttonColor = Color.white;
            nodesList[i].GetComponent<SpriteRenderer>().color = buttonColor;
        }

        GameObject[] arrowObjects = GameObject.FindGameObjectsWithTag("Arrow");
        for (int i = 0; i < arrowObjects.Length; i++)
        {
            Destroy(arrowObjects[i]);
        }
    }

    public void VisualizationButton(TMP_InputField inputText)
    {
        int i = 0;
        if (inputText.text == "" || int.TryParse(searchbValue.text, out i).Equals(false))
        {
            visualizationButton.interactable = false;
            Color buttonColor = visualizationButton.GetComponent<Image>().color;
            buttonColor.a = 0.5f;
            visualizationButton.GetComponent<Image>().color = buttonColor;
        }
        else
        {
            visualizationButton.interactable = true;
            Color buttonColor = visualizationButton.GetComponent<Image>().color;
            buttonColor.a = 1f;
            visualizationButton.GetComponent<Image>().color = buttonColor;
        }
    }

    public void StartVisualization()
    {
        Initialized();
        StartCoroutine(Visualization());
    }
 
    IEnumerator Visualization()
    {
        int targetIndex = int.Parse(searchbValue.text);

        for (int i = 0; i < nodesList.Count - 1; i++)
        {
            if(i == targetIndex)
            {
                Color color = nodesList[i].GetComponent<SpriteRenderer>().color;
                color = Color.green;
                nodesList[i].GetComponent<SpriteRenderer>().color = color;
                break;
            }

            yield return StartCoroutine(NodeColorChange(i, i + 1, Color.green, 0.2f));
            yield return StartCoroutine(CreateAnimateEdge(i, i + 1, Color.green, 0.2f));

            if (i + 1 == targetIndex)
                break;
        }
    }


    IEnumerator AnimateLineDraw(LineRenderer ir, Vector2 startPos, Vector2 endPos, float duration)
    {
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float t = elapsed / duration;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);

            ir.SetPosition(0, startPos);
            ir.SetPosition(1, currentPos);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator NodeColorChange(int nodeA, int nodeB, Color color, float duration)
    {
        Color a = nodesList[nodeA].GetComponent<SpriteRenderer>().color;
        a = Color.green;
        nodesList[nodeA].GetComponent<SpriteRenderer>().color = a;

        yield return new WaitForSeconds(duration);

        Color b = nodesList[nodeB].GetComponent<SpriteRenderer>().color;
        b = Color.green;
        nodesList[nodeB].GetComponent<SpriteRenderer>().color = b;
    }

    IEnumerator CreateAnimateEdge(int nodeA, int nodeB, Color color, float duration)
    {
        GameObject lineObj = new GameObject("Edge_" + nodeB);
        lineObj.tag = "Arrow";
        lineObj.transform.position = new Vector3(lineObj.transform.position.x, lineObj.transform.position.x, -2);
        LineRenderer ir = lineObj.AddComponent<LineRenderer>();

        Vector3 start = nodesList[nodeA].transform.position;
        Vector3 end = nodesList[nodeB].transform.position;

        ir.positionCount = 2;
        ir.startWidth = 0.5f;
        ir.endWidth = 0.5f;
        ir.material = mat;
        ir.startColor = color;
        ir.endColor = color;

        yield return StartCoroutine(AnimateLineDraw(ir, start, end, duration));
    }
}
