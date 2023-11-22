using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI skipText;
    AsyncOperation newScene;
    GameObject newSceneWrapper;
    GameObject sceneWrapper;


    // Update is called once per frame

    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
        sceneWrapper = GameObject.Find("Wrapper");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && newScene.isDone)
        {
            ChangeScene();
        }
    }

    IEnumerator LoadYourAsyncScene()
    {

        newScene = SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
        while (!newScene.isDone)
        {
            yield return null;
        }
        GameObject[] rootObjects = SceneManager.GetSceneByName("Level1").GetRootGameObjects();
        foreach (var obj in rootObjects)
        {
            if (obj.name == "Wrapper")
            {
                newSceneWrapper = obj;
                break;
            }
        }
        skipText.text = "Press Space to skip intro";
    }

    public void ChangeScene(){
        sceneWrapper.SetActive(false);
        newSceneWrapper.SetActive(true);
        SceneManager.UnloadSceneAsync("IntroCutscene");
    }
}
