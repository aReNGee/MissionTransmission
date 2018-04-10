using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Button startText;
    public Button exitText;

    public Canvas rulesMenu;
    public Canvas quitMenu;

    public Canvas creditsMenu;

    // Use this for initialization
    void Start ()
    {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        rulesMenu = rulesMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();

        rulesMenu.enabled = false;
        quitMenu.enabled = false;
        creditsMenu.enabled = false;
    }
	
    public void RulesPress()
    {
        rulesMenu.enabled = true;
        quitMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        creditsMenu.enabled = false;
    }

    public void ExitPress()
    {
        rulesMenu.enabled = false;
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
        creditsMenu.enabled = false;
    }

    public void NoPress()
    {
        rulesMenu.enabled = false;
        quitMenu.enabled = false;
        creditsMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void creditPress(){
        rulesMenu.enabled = false;
        quitMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        creditsMenu.enabled = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void ExitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }
}
