using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{

    public CinemachineVirtualCamera CurrentCam;
    public GameObject loadingimage;
    public AudioClip[] audioclip;
    public AudioSource audioSource;
    public GameObject slider;
    public GameObject skipButton;

    public void MenuButtons(CinemachineVirtualCamera NextCam)
    {
        CurrentCam.Priority = 0;
        NextCam.Priority = 1;
        CurrentCam = NextCam;
    }

    public void Practice()
    {
        //StartCoroutine(LoadYourScene("Backyard"));
        StartCoroutine(LoadYourAsyncScene("Backyard"));
        //StartCoroutine(Cutscene(videos[0]));
    }

    public void NewGame()
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

    public void Temple()
    {
        StartCoroutine(LoadYourAsyncScene("Temple"));

    }

    public void Lab1()
    {
        StartCoroutine(LoadYourAsyncScene("Lab1"));

    }

    IEnumerator Cutscene(GameObject video)
    {
        video.SetActive(true);
        yield return null;
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

    public void Quit()
    {
        Application.Quit();
    }

    public void OnMouseEnter()
    {
        audioSource.clip = audioclip[0];
        audioSource.Play();
    }

    public void OnMouseDown()
    {
        audioSource.clip = audioclip[1];
        audioSource.Play();
    }

    public void Graphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}

