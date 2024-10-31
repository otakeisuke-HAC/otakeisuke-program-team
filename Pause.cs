using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseCanvas;
    private Scene loadScene;
    public GameObject Setumei;
    public GameObject SetumeiBotton;
    public GameObject SetumeiBottonBack;


    void Start()
    {
        // 現在のSceneを取得
        loadScene = SceneManager.GetActiveScene();
        
        //Setumei.SetActive(false);
        //SetumeiBotton.SetActive(true);
        //SetumeiBottonBack.SetActive(false);
    }

    private void Update()
    {
        if(PauseCanvas.activeSelf == true)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                ContinueButton();

            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Retry();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                ToTitle();
            }
            else if (Input.GetKeyDown(KeyCode.I) && Setumei.activeSelf == false)
            {
                Setumai();
            }
            else if (Input.GetKeyDown(KeyCode.I) && Setumei.activeSelf == true)
            {
                SetumaiBack();
            }

        }
    }


    public void ContinueButton()
    {
        //ゲームオブジェクト非表示
        PauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(loadScene.name);
        Time.timeScale = 1;
    }
    

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }

    void Setumai()
    {
        Setumei.SetActive(true);
        SetumeiBotton.SetActive(false);
        SetumeiBottonBack.SetActive(true);
    }

    void SetumaiBack()
    {
        Setumei.SetActive(false);
        SetumeiBotton.SetActive(true);
        SetumeiBottonBack.SetActive(false);
    }
}
