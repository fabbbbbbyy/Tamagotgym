using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalendarManager : MonoBehaviour
{

    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadScene());
        }
    }

    public void buttonBehaviour(int i)
    {
        switch (i)
        {
            case (0):
            default:
                break;
            case (1):
                SceneManager.LoadScene("Game");
                break;
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }
}
