using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineNodeRange : MonoBehaviour
{
    public GameObject Nodes;
    public GameObject nodePrefab;

    public int nodeCount;

    LineRenderer line;
    public List<GameObject> nodesList = new List<GameObject>();

    private void Start()
    {
        nodesList.Clear();
        NodeSummon(nodePrefab, Nodes, nodeCount);
        CreateAllEdges();
    }

    public void NodeSummon(GameObject prefab, GameObject nodesParent, int count)
    {
        float totalWidth = 16f;
        float spacing = totalWidth / count; // 혹은 (a-1)로 해도 됨
        float centerOffset = spacing * (count - 1) / 2f;

        for (int i = 0; i < count; i++)
        {
            float x = i * spacing - centerOffset;
            GameObject node = Instantiate(prefab, new Vector3(x, 0, 1), Quaternion.identity, nodesParent.transform);
            node.transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();

            nodesList.Add(node);
        }
    }

    public void CreateAllEdges()
    {
        for (int i = 0; i < nodesList.Count - 1; i++)
        {
            GameObject lineObj = new GameObject("Line_" + i);
            LineRenderer line = lineObj.AddComponent<LineRenderer>();

            line.positionCount = 2;
            line.SetPosition(0, nodesList[i].transform.position);
            line.SetPosition(1, nodesList[i + 1].transform.position);

            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.gray;
            line.endColor = Color.gray;

            // 필요 시, lineObj를 이 스크립트의 자식으로
            lineObj.transform.parent = this.transform;
        }
    }
}
