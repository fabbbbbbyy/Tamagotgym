using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class BasicCat : MonoBehaviour
{

    [SerializeField]
    private int agility;
    [SerializeField]
    private int strength;

    private bool serverTime;
    private int touchCount;
    private int pressesToday; 

    public ParticleSystem partSystem;

    // Start is called before the first frame update
    void Start()
    {
        partSystem.Stop();

        updateTime();

        updateStatus();

        checkEvolution();
    }

    void checkEvolution()
    {
    }

    void updateTime()
    {
        if (!PlayerPrefs.HasKey("ticks"))
        {
            PlayerPrefs.SetInt("ticks", (int)DateTime.Today.Ticks);
        }

        if (!PlayerPrefs.HasKey("pressesToday") || isNewDay())
        {
            PlayerPrefs.SetInt("pressesToday", 0);
        }
    }

        // Update is called once per frame
        void Update()
    {

        /* Refresh ability to add stats if appropriate. */
        if (isNewDay())
        {
            PlayerPrefs.SetInt("pressesToday", 0);
        }

        /* Jump Animation if appropriate. */
        GetComponent<Animator>().SetBool("jump", gameObject.transform.position.y > -2.5f);

        checkJump();

        recordStats();
    }

    public int agilityGS
    {
        get { return agility; }
        set { agility = value; }
    }

    public int strengthGS
    {
        get { return strength; }
        set { strength = value; }
    }

    public void updateAgility()
    {
        pressesToday = PlayerPrefs.GetInt("pressesToday");
        if (pressesToday <= 1)
        {
            pressesToday++;
            agility++;
            PlayerPrefs.SetInt("pressesToday", pressesToday);
        }
    }

    public void updateStrength()
    {
        pressesToday = PlayerPrefs.GetInt("pressesToday");
        if (pressesToday <= 1)
        {
            pressesToday++;
            strength++;
            PlayerPrefs.SetInt("pressesToday", pressesToday);
        }
    }

    private bool isNewDay()
    {
        int todayTicks = (int)DateTime.Today.Ticks;

        if (PlayerPrefs.GetInt("ticks") != todayTicks)
        {
            PlayerPrefs.SetInt("ticks", todayTicks);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void saveState()
    {
        if (!serverTime)
        {
            updateDevice();
        }

        recordStats();
    }

    void recordStats()
    {
        PlayerPrefs.SetInt("agility", agility);
        PlayerPrefs.SetInt("strength", strength);
        PlayerPrefs.Save();
    }

    void updateStatus()
    {
        updateLastPlayed();

        //reduceStats();

        updateStats();
    }

    void updateLastPlayed()
    {
        if (!PlayerPrefs.HasKey("lastPlayed"))
        {
            PlayerPrefs.SetString("lastPlayed", getStringTime());
        }

        if (serverTime)
        {
            updateServer();
        }
        else
        {
            InvokeRepeating("updateDevice", 0f, 15f);
        }
    }

    void updateStats()
    {
        if (!PlayerPrefs.HasKey("agility"))
        {
            agility = 0;
            PlayerPrefs.SetInt("agility", agility);
        }
        else
        {
            agility = PlayerPrefs.GetInt("agility");
        }

        if (!PlayerPrefs.HasKey("strength"))
        {
            strength = 0;
            PlayerPrefs.SetInt("strength", strength);
        }
        else
        {
            strength = PlayerPrefs.GetInt("strength");
        }
    }

    void reduceStats()
    {
        /* Reducing the agility & strength dependent of time. */
        TimeSpan ts = getTimeSpan();

        agility -= (int)(ts.TotalDays);
        if (agility < 0)
        {
            agility = 0;
        }

        strength -= (int)(ts.TotalDays);
        if (strength < 0)
        {
            strength = 0;
        }
    }

    TimeSpan getTimeSpan()
    {
        if (serverTime || !PlayerPrefs.HasKey("lastPlayed"))
        {
            return new TimeSpan();
        }
        else
        {
            return DateTime.Now.Subtract(DateTime.ParseExact(PlayerPrefs.GetString("lastPlayed"), "MM/dd/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture));
        }
    }

    string getStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " +
            now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    void updateServer()
    {
    }

    void updateDevice()
    {
        PlayerPrefs.SetString("lastPlayed", getStringTime());
    }

    void checkJump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            if (hit)
            {
                if (isCat(hit))
                {
                    touchCount++;
                    if (touchCount == 2)
                    {
                        touchCount = 0;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1400));
                    }
                }

            }
        }
    }

    bool isCat(RaycastHit2D hit)
    {
        return hit.transform.gameObject.tag == "Cat" || hit.transform.gameObject.tag == "armsOneCat"
            || hit.transform.gameObject.tag == "legsOneCat" || hit.transform.gameObject.tag == "equalOneCat"
            || hit.transform.gameObject.tag == "armsTwoCat" || hit.transform.gameObject.tag == "legsTwoCat"
            || hit.transform.gameObject.tag == "equalTwoCat" || hit.transform.gameObject.tag == "armsThreeCat"
            || hit.transform.gameObject.tag == "legsThreeCat" || hit.transform.gameObject.tag == "equalThreeCat";
    }
}

