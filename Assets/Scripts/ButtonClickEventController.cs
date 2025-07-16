using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickEventController : MonoBehaviour
{
    public void BackToMainScene()
    {
        SceneLoadingScripts.Instance.LoadScene("Main_Scene");
    }
}
