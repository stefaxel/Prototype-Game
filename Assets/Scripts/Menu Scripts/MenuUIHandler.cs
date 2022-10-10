using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        //startButton.interactable = false;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }

    public void StoreName()
    {
        startButton.interactable = true;
        playerName = inputField.text;
        DataManager.Instance.currentPlayer = playerName;
    }
}
