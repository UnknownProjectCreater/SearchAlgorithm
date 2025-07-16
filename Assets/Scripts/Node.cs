using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Node : MonoBehaviour
{
    public string nodeName;
    public List<Node> neighbers = new List<Node>();
    public bool visited = false;
    public TextMeshPro text;

    Color defaultColor = Color.white;
    Color visitedColor = Color.green;

    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameObject.name = nodeName;
    }

    public void ResetNode()
    {
        visited = false;
        if (rend != null)
            rend.color = defaultColor;
    }

    public void Visit()
    {
        visited = true;
        if (rend != null)
            rend.color = visitedColor;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = nodeName;
    }
}
