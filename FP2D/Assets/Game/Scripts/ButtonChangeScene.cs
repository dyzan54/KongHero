using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log(LevelManager.Instance == null);
        LevelManager.Instance.LoadScene(sceneName);
    }
}
