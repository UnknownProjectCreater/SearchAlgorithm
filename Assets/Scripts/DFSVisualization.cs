using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DFSVisualization : MonoBehaviour
{
    public TMP_InputField searchbValue;
    public Button visualizationButton;
    public Material mat;

    public List<GameObject> nodesList;
    public float delay = 0.2f;

    public Dictionary<string, List<string>> graphData = new Dictionary<string, List<string>>()
{
    { "A", new List<string> { "B", "C" } },
    { "B", new List<string> { "D", "E" } },
    { "C", new List<string> { "F", "G" } },
    { "D", new List<string> { "H" } },
    { "E", new List<string> { "I" } },
    { "F", new List<string> { "J" } },
    { "G", new List<string> { "K" } },
    { "H", new List<string>() },
    { "I", new List<string>() },
    { "J", new List<string>() },
    { "K", new List<string>() }
};

    private Dictionary<string, Node> nodeLookup = new Dictionary<string, Node>();

    private HashSet<string> visitedNodes = new HashSet<string>();

    // Update is called once per frame
    void Update()
    {
        VisualizationButton(searchbValue);
    }

    public void Initialized()
    {
        foundTarget = false;
        visitedNodes.Clear();
        foreach (var node in nodeLookup.Values)
        {
            node.ResetNode();
        }

        nodeLookup.Clear();
        foreach(var node in GameObject.FindObjectsOfType<Node>())
        {
            node.ResetNode();
            nodeLookup[node.nodeName] = node;
        }

        GameObject[] arrowObjects = GameObject.FindGameObjectsWithTag("Arrow");
        for (int i = 0; i < arrowObjects.Length; i++)
        {
            Destroy(arrowObjects[i]);
        }
    }

    public void VisualizationButton(TMP_InputField inputText)
    {
        if (inputText.text == "")
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
        if (!nodeLookup.ContainsKey("A"))
        {
            Debug.LogError($"탐색 노드 '{searchbValue.text}'가 존재하지 않습니다.");
            return;
        }

        StartCoroutine(DFS("A", "A"));
    }

    void LoadNodes()
    {
        Node[] allnodes = GameObject.FindObjectsOfType<Node>();

        foreach(var node in allnodes)
        {
            node.ResetNode();
            nodeLookup[node.nodeName] = node;
        }
    }

    private bool foundTarget = false;

    IEnumerator DFS(string currentName, string startName)
    {
        Node pastNode = nodeLookup[startName];

        if (foundTarget || visitedNodes.Contains(currentName))
        {
            yield break;
        }

        visitedNodes.Add(currentName);
        Node currentNode = nodeLookup[currentName];
        currentNode.Visit();

        if(startName != null)
        {
            yield return StartCoroutine(CreateAnimateEdge(pastNode, currentNode, Color.green, 0.2f));
        }

        yield return new WaitForSeconds(delay); // 방문 상태를 보여줄 시간

        if (currentName == searchbValue.text)
        {
            foundTarget = true;
            Debug.Log($"목표 노드 {currentName}를 찾았습니다!");
            yield break;
        }

        foreach (string neighborName in graphData[currentName])
        {
            yield return StartCoroutine(DFS(neighborName, currentName));
        }
    }

    IEnumerator AnimateLineDraw(LineRenderer ir, Vector2 startPos, Vector2 endPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);

            ir.SetPosition(0, startPos);
            ir.SetPosition(1, currentPos);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CreateAnimateEdge(Node a, Node b, Color color, float duration)
    {
        GameObject lineObj = new GameObject("Edge_" + b.nodeName);
        lineObj.tag = "Arrow";
        lineObj.transform.position = new Vector3(lineObj.transform.position.x, lineObj.transform.position.x, -2);
        LineRenderer ir = lineObj.AddComponent<LineRenderer>();

        Vector3 start = a.transform.position + new Vector3(0, 0, -1);
        Vector3 end = b.transform.position + new Vector3(0, 0, -1);

        ir.positionCount = 2;
        ir.startWidth = 0.5f;
        ir.endWidth = 0.5f;
        ir.material = mat;
        ir.startColor = color;
        ir.endColor = color;

        yield return StartCoroutine(AnimateLineDraw(ir, start, end, duration));
    }
}
