using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject controlPanel;
    public static bool GameIsPaused = false;
    public Button pausebutton;
    public GameObject slider;
    public GameObject skipButton;

    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                //Time.timeScale = 1f;
            }
            else
            {
                Pause();
                //Time.timeScale = 0f;
            }
        }

        if(PausePanel.activeInHierarchy)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(SceneManager.GetActiveScene().name == "Apocalypse")
        {
           // Application.targetFrameRate = 30;
        }
    }

    public void PauseResume()
    {
        if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        controlPanel.SetActive(true);
        GameIsPaused = false;
        Cursor.visible = false;
        //Time.timeScale = 1;
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        controlPanel.SetActive(false);
        GameIsPaused = true;
        Cursor.visible = true;
        //Time.timeScale = 0.01f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void Apocalypse()
    {
        StartCoroutine(LoadYourAsyncScene("Apocalypse"));
    }

    public void Subway()
    {
        StartCoroutine(LoadYourAsyncScene("Subway"));
    }

    public void Village()
    {
        StartCoroutine(LoadYourAsyncScene("Village"));
    }

    public void Dungeon1()
    {
        StartCoroutine(LoadYourAsyncScene("Dungeon1"));

    }

    public void Dungeon2()
    {
        StartCoroutine(LoadYourAsyncScene("Dungeon2"));

    }

    public void Dungeon3()
    {
        StartCoroutine(LoadYourAsyncScene("Dungeon3"));

    }

    public void Temple()
    {
        StartCoroutine(LoadYourAsyncScene("Temple"));

    }

    public void Lab1()
    {
        StartCoroutine(LoadYourAsyncScene("Lab1"));

    }

    public void Lab2()
    {
        StartCoroutine(LoadYourAsyncScene("Lab2"));

    }

    IEnumerator LoadYourAsyncScene(string SceneName)
    {
        yield return null;
        //loadingimage.SetActive(true);
        slider.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                skipButton.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
                //Activate the Scene
                {
                    asyncLoad.allowSceneActivation = true;
                    //SceneManager.LoadScene(SceneName);
                }
                skipButton.GetComponent<Button>().onClick.AddListener(Skip);
            }
            slider.GetComponent<Slider>().value = asyncLoad.progress;
            // yield return new WaitForEndOfFrame();
            yield return null;
        }

        void Skip()
        {
            asyncLoad.allowSceneActivation = true;
        }

        /*while (asyncLoad.progress <= 1)
        {
            
            
        }

        if (slider.GetComponent<Slider>().value <= 0.8)
        {
            
                
        }*/
    }

    IEnumerator LoadYourScene(string SceneName)
    {
        yield return null;
        //loadingimage.SetActive(true);
        slider.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        while (asyncLoad.progress <= 1)
        {
            slider.GetComponent<Slider>().value = asyncLoad.progress;
            yield return new WaitForEndOfFrame();

        }
    }


    public void Open(GameObject open)
    {
        open.SetActive(true);
    }

    public void Close(GameObject close)
    {
        close.SetActive(false);
    }
}
