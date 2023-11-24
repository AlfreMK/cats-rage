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


    private bool isMenu = true;


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
            instructions.color = new Color32(50, 255, 11, 255);
            mushroom.transform.position = new Vector3(-2, -3.7f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            audioSource.PlayOneShot(changeOption);
            optionSelected = "start";
            startGame.color = new Color32(50, 255, 11, 255);
            instructions.color = new Color32(255, 255, 255, 255);
            mushroom.transform.position = new Vector3(-1.8f, -2.4f, 0);
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
