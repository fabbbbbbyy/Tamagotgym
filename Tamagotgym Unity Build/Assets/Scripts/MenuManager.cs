using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject flashText;
    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("flashTheText", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Game");
            //StartCoroutine(LoadScene());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

        void flashTheText()
    {
        if (flashText.activeInHierarchy)
        {
            flashText.SetActive(false);
        }
        else
        {
            flashText.SetActive(true);
        }
    }
}
