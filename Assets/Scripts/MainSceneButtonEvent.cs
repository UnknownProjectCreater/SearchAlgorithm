using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneButtonEvent : MonoBehaviour
{
    public CanvasGroup start_UI_CanvasGroup;
    public CanvasGroup choose_UI_CanvasGroup;

    private void Start()
    {
        start_UI_CanvasGroup.gameObject.SetActive(true);
        choose_UI_CanvasGroup.gameObject.SetActive(true);
        CanvasGroupSetting(choose_UI_CanvasGroup, 0, false, false);
        CanvasGroupSetting(start_UI_CanvasGroup, 1, true, true);
    }

    public void startButton_Click()
    {
        CanvasGroupSetting(start_UI_CanvasGroup, 0, false, false);
        CanvasGroupSetting(choose_UI_CanvasGroup, 1, true, true);
    }
    
    public void BackToStart_Click()
    {
        CanvasGroupSetting(choose_UI_CanvasGroup, 0, false, false);
        CanvasGroupSetting(start_UI_CanvasGroup, 1, true, true);
    }

    void CanvasGroupSetting(CanvasGroup canvasGroup, float alpha, bool isInteractable, bool isBlocksRaycasts)
    {
        canvasGroup.alpha = alpha;
        canvasGroup.interactable = isInteractable;
        canvasGroup.blocksRaycasts = isBlocksRaycasts;
    }

    public void LinearButton_Click()
    {
        SceneLoadingScripts.Instance.LoadScene("LinearSearch_Scene");
    }

    public void BinaryButton_Click()
    {
        SceneLoadingScripts.Instance.LoadScene("BinarySearch_Scene");
    }

    public void DFS_Click()
    {
        SceneLoadingScripts.Instance.LoadScene("DFS_Scene");
    }

    public void BFS_Click()
    {
        SceneLoadingScripts.Instance.LoadScene("BFS_Scene");
    }
}
