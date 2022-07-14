using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] GameObject _loaderCanvas;
    [SerializeField] Image _loaderField;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        
        _loaderCanvas.SetActive(value: true);

        do
        {
            await Task.Delay(200);
            _loaderField.fillAmount = scene.progress;
            Debug.Log("Progress:" + scene.progress);

        } while (scene.progress > 0.9f);

        await Task.Delay(5000);
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(value: false);
    }
}
