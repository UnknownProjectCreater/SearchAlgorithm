using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BinarySearchVisualization : MonoBehaviour
{
    public TMP_InputField searchbValue;
    public Button visualizationButton;

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
        StartCoroutine(Visualization(searchbValue, nodesList));
    }

    IEnumerator Visualization(TMP_InputField searchValue, List<GameObject> arr)
    {
        int targetIndex = int.Parse(searchbValue.text);
        int currentNode = 0;

        int left = 0;
        int right = arr.Count - 1;

        while (left <= right)
        {
            double a = (left + right) / 2;
            currentNode = (int)System.Math.Truncate(a);

            if (currentNode == targetIndex)
            {
                yield return StartCoroutine(NodeColorChange(currentNode, Color.green, 0.2f));
                break;
            }
            else if (currentNode < targetIndex)
            {
                left = currentNode + 1;
                yield return StartCoroutine(NodeColorChange(currentNode, Color.blue, 0.2f));
            }
            else
            {
                right = currentNode - 1;
                yield return StartCoroutine(NodeColorChange(currentNode, Color.blue, 0.2f));
            }
        }
    }

    IEnumerator NodeColorChange(int nodeA, Color color, float duration)
    {
        nodesList[nodeA].GetComponent<SpriteRenderer>().color = color;

        yield return new WaitForSeconds(duration);
    }
}
