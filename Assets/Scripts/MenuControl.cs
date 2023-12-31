using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MenuScene;
    public GameObject InstructionsScene;
    public GameObject mushroom;
    public TMPro.TextMeshProUGUI startGame;
    public TMPro.TextMeshProUGUI instructions;
    
    public AudioSource audioSource;
    public AudioClip changeOption;
    public AudioClip selectOption;
    public AudioClip backOption;
    private string optionSelected = "start"; 

    private float startGameHalfWidth;
    private float instructionsHalfWidth;

    private bool isMenu = true;

    void Start()
    {
        startGame.color = new Color32(255, 101, 255, 255);
        startGameHalfWidth = startGame.GetComponent<RectTransform>().rect.width / 2;
        instructionsHalfWidth = instructions.GetComponent<RectTransform>().rect.width / 2;
        mushroom.transform.localPosition = startGame.transform.localPosition + new Vector3(35.0f - startGameHalfWidth, 9, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (isMenu){
            Menu();
        }
        else{
            Instructions();
        }
        
    }

    void Menu(){
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            audioSource.PlayOneShot(changeOption);
            optionSelected = "instructions";
            startGame.color = new Color32(255, 255, 255, 255);
            instructions.color = new Color32(255, 101, 255, 255);
            mushroom.transform.localPosition = instructions.transform.localPosition + new Vector3(35.0f - instructionsHalfWidth, 9, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            audioSource.PlayOneShot(changeOption);
            optionSelected = "start";
            startGame.color = new Color32(255, 101, 255, 255);
            instructions.color = new Color32(255, 255, 255, 255);
            mushroom.transform.localPosition = startGame.transform.localPosition + new Vector3(35.0f - startGameHalfWidth, 9, 0);
        }
        if (Input.GetKeyDown(KeyCode.Return)){
            if (optionSelected == "start"){
                // TODO: Add transition to cutscene
                UnityEngine.SceneManagement.SceneManager.LoadScene("IntroCutscene");
            }
            else if (optionSelected == "instructions"){
                audioSource.PlayOneShot(selectOption);
                isMenu = false;
                MenuScene.SetActive(false);
                InstructionsScene.SetActive(true);
            }
        }
    }

    void Instructions(){
        if (Input.GetKeyDown(KeyCode.Return)){
            audioSource.PlayOneShot(backOption);
            isMenu = true;
            MenuScene.SetActive(true);
            InstructionsScene.SetActive(false);
        }
    }
}
