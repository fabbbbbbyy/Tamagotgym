using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static int FIRST_EVOLUTION = 10;
    private static int SECOND_EVOLUTION = 30;
    private static int THIRD_EVOLUTION = 60;

    public GameObject agilityText;
    public GameObject strengthText;

    public GameObject tutorialManager;

    public Animator transitionAnim;
    public ParticleSystem partSystem;
    public GameObject backgroundMusic;
    public GameObject evolutionMusic;

    public GameObject currentCat;
    public GameObject armsOneCat;
    public GameObject legsOneCat;
    public GameObject equalOneCat;
    public GameObject armsTwoCat;
    public GameObject legsTwoCat;
    public GameObject equalTwoCat;
    public GameObject armsThreeCat;
    public GameObject legsThreeCat;
    public GameObject equalThreeCat;

    public GameObject background;
    public GameObject backgroundPanel;
    public Sprite[] backgroundSprites;

    public GameObject tilePanel;
    public Sprite[] tileSprites;
    public GameObject[] tileList;

    public ParticleSystem armsOnePartSys;
    public ParticleSystem legsOnePartSys;
    public ParticleSystem equalOnePartSys;
    public ParticleSystem armsTwoPartSys;
    public ParticleSystem legsTwoPartSys;
    public ParticleSystem equalTwoPartSys;


    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("armsOneCat"))
        {
            currentCat.SetActive(false);
            armsOneCat.SetActive(true);
            partSystem = armsOnePartSys;
        }
        if (PlayerPrefs.HasKey("legsOneCat"))
        {
            currentCat.SetActive(false);
            legsOneCat.SetActive(true);
            partSystem = legsOnePartSys;
        }
        if (PlayerPrefs.HasKey("equalOneCat"))
        {
            currentCat.SetActive(false);
            equalOneCat.SetActive(true);
            partSystem = equalOnePartSys;
        }
        if (PlayerPrefs.HasKey("armsTwoCat"))
        {
            currentCat.SetActive(false);
            armsTwoCat.SetActive(true);
            partSystem = armsTwoPartSys;
        }
        if (PlayerPrefs.HasKey("legsTwoCat"))
        {
            currentCat.SetActive(false);
            legsTwoCat.SetActive(true);
            partSystem = legsTwoPartSys;
        }
        if (PlayerPrefs.HasKey("equalTwoCat"))
        {
            currentCat.SetActive(false);
            equalTwoCat.SetActive(true);
            partSystem = equalTwoPartSys;
        }
        if (PlayerPrefs.HasKey("armsThreeCat"))
        {
            currentCat.SetActive(false);
            armsThreeCat.SetActive(true);
        }
        if (PlayerPrefs.HasKey("legsThreeCat"))
        {
            currentCat.SetActive(false);
            legsThreeCat.SetActive(true);
        }
        if (PlayerPrefs.HasKey("equalThreeCat"))
        {
            currentCat.SetActive(false);
            equalThreeCat.SetActive(true);
        }


        if (!PlayerPrefs.HasKey("background"))
        {
            PlayerPrefs.SetInt("background", 0);
        } 
        createBackground(PlayerPrefs.GetInt("background"));
        if (!PlayerPrefs.HasKey("tiles"))
        {
            PlayerPrefs.SetInt("tiles", 0);
        }
        createTiles(PlayerPrefs.GetInt("tiles"));
    }

    public void createBackground(int i)
    {
        background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[i];

        if (backgroundPanel.activeInHierarchy)
        {
            backgroundPanel.SetActive(false);
        }

        PlayerPrefs.SetInt("background", i);
    }

    public void createTiles(int t)
    {
        for (int i = 0; i < tileList.Length; i++)
        {
            tileList[i].GetComponent<SpriteRenderer>().sprite = tileSprites[t];
        }

        if (tilePanel.activeInHierarchy)
        {
            tilePanel.SetActive(false);
        }

        PlayerPrefs.SetInt("tiles", t);
    }



        // Start is called before the first frame update
        void Start()
    {
        if (!PlayerPrefs.HasKey("doneTutorial"))
        {
            tutorialManager.SetActive(true);
        }
        else
        {
            tutorialManager.SetActive(false);
        }

        if (armsOneCat.activeInHierarchy)
        {
            currentCat = armsOneCat;
        }
        if (legsOneCat.activeInHierarchy)
        {
            currentCat = legsOneCat;
        }
        if (equalOneCat.activeInHierarchy)
        {
            currentCat = equalOneCat;
        }
        if (armsTwoCat.activeInHierarchy)
        {
            currentCat = armsTwoCat;
        }
        if (legsTwoCat.activeInHierarchy)
        {
            currentCat = legsTwoCat;
        }
        if (equalTwoCat.activeInHierarchy)
        {
            currentCat = equalTwoCat;
        }
        if (armsThreeCat.activeInHierarchy)
        {
            currentCat = armsThreeCat;
        }
        if (legsThreeCat.activeInHierarchy)
        {
            currentCat = legsThreeCat;
        }
        if (equalThreeCat.activeInHierarchy)
        {
            currentCat = equalThreeCat;
        }

    }

    // Update is called once per frame
    void Update()
    {
        agilityText.GetComponent<Text>().text = currentCat.GetComponent<BasicCat>().agilityGS.ToString();
        strengthText.GetComponent<Text>().text = currentCat.GetComponent<BasicCat>().strengthGS.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentCat.GetComponent<BasicCat>().saveState();
            Application.Quit();
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
                
                break;
            case (2):
                currentCat.GetComponent<BasicCat>().updateAgility();

                if (canEvolve())
                {
                    startEvolution();
                }

                break;
            case (3):
                currentCat.GetComponent<BasicCat>().updateStrength();

                if (canEvolve())
                {
                    startEvolution();
                }

                break;
            case (4):
                tilePanel.SetActive(!tilePanel.activeInHierarchy);
                break;      
            case (5):
                backgroundPanel.SetActive(!backgroundPanel.activeInHierarchy);
                break;
            case (6):
                currentCat.GetComponent<BasicCat>().saveState();
                Application.Quit();
                break;
            }
    }

    private bool canEvolve()
    {
        int strength = currentCat.GetComponent<BasicCat>().strengthGS;
        int agility = currentCat.GetComponent<BasicCat>().agilityGS;
        int totalStats = strength + agility;

        return (totalStats == FIRST_EVOLUTION) || (totalStats == SECOND_EVOLUTION) || (totalStats == THIRD_EVOLUTION);
    }

    private void startEvolution()
    {
        int strength = currentCat.GetComponent<BasicCat>().strengthGS;
        int agility = currentCat.GetComponent<BasicCat>().agilityGS;
        int totalStats = strength + agility;

        if (totalStats == FIRST_EVOLUTION && !hasOneEvo())
        {
            firstEvolution();
        }

        if (totalStats == SECOND_EVOLUTION && !hasTwoEvo())
        {
            secondEvolution();
        }

        if (totalStats == THIRD_EVOLUTION && !hasThreeEvo())
        {
            thirdEvolution();
        }
    }

    bool hasOneEvo() {
        return PlayerPrefs.HasKey("armsOneCat") || PlayerPrefs.HasKey("legsOneCat")
            || PlayerPrefs.HasKey("equalOneCat");
    }
    bool hasTwoEvo()
    {
        return PlayerPrefs.HasKey("armsTwoCat") || PlayerPrefs.HasKey("legsTwoCat")
            || PlayerPrefs.HasKey("equalTwoCat");
    }
    bool hasThreeEvo()
    {
        return PlayerPrefs.HasKey("armsThreeCat") || PlayerPrefs.HasKey("legsThreeCat")
            || PlayerPrefs.HasKey("equalThreeCat");
    }

    void firstEvolution()
    {
        int strength = currentCat.GetComponent<BasicCat>().strengthGS;
        int agility = currentCat.GetComponent<BasicCat>().agilityGS;

        if (strength > agility)
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(armsOneCat));
            updateCatStats(currentCat, armsOneCat);
            partSystem = armsOnePartSys;
            PlayerPrefs.SetInt("armsOneCat", 1);
        }
        else if (strength < agility)
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(legsOneCat));
            updateCatStats(currentCat, legsOneCat);
            partSystem = legsOnePartSys;
            PlayerPrefs.SetInt("legsOneCat", 1);
        }
        else
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(equalOneCat));
            updateCatStats(currentCat, equalOneCat);
            partSystem = equalOnePartSys;
            PlayerPrefs.SetInt("equalOneCat", 1);
        }
    }

    void secondEvolution()
    {
        int strength = currentCat.GetComponent<BasicCat>().strengthGS;
        int agility = currentCat.GetComponent<BasicCat>().agilityGS;

        if (strength > agility)
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(armsTwoCat));
            updateCatStats(currentCat, armsTwoCat);
            deleteCurrentKey();
            partSystem = armsTwoPartSys;
            PlayerPrefs.SetInt("armsTwoCat", 1);
        }
        else if (strength < agility)
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(legsTwoCat));
            updateCatStats(currentCat, legsTwoCat);
            deleteCurrentKey();
            partSystem = legsTwoPartSys;
            PlayerPrefs.SetInt("legsTwoCat", 1);
        }
        else
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(equalTwoCat));
            updateCatStats(currentCat, equalTwoCat);
            deleteCurrentKey();
            partSystem = equalTwoPartSys;
            PlayerPrefs.SetInt("equalTwoCat", 1);
        }
    }

    void thirdEvolution()
    {
        int strength = currentCat.GetComponent<BasicCat>().strengthGS;
        int agility = currentCat.GetComponent<BasicCat>().agilityGS;

        if (strength > agility)
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(armsThreeCat));
            updateCatStats(currentCat, armsThreeCat);
            deleteCurrentKey();
            PlayerPrefs.SetInt("armsThreeCat", 1);
        }
        else if (strength < agility)
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(legsThreeCat));
            updateCatStats(currentCat, legsThreeCat);
            deleteCurrentKey();
            PlayerPrefs.SetInt("legsThreeCat", 1);
        }
        else
        {
            Sprite currSprite = currentCat.GetComponent<SpriteRenderer>().sprite;
            partSystem.textureSheetAnimation.SetSprite(0, currSprite);
            StartCoroutine(evolutionAnimation(equalThreeCat));
            updateCatStats(currentCat, equalThreeCat);
            deleteCurrentKey();
            PlayerPrefs.SetInt("equalThreeCat", 1);
        }
    }

    void deleteCurrentKey()
    {
        if (currentCat.tag == "armsOneCat")
        {
            PlayerPrefs.DeleteKey("armsOneCat");
        }
        if (currentCat.tag == "armsTwoCat")
        {
            PlayerPrefs.DeleteKey("armsTwoCat");
        }
        if (currentCat.tag == "armsThreeCat")
        {
            PlayerPrefs.DeleteKey("armsThreeCat");
        }
        if (currentCat.tag == "legsOneCat")
        {
            PlayerPrefs.DeleteKey("legsOneCat");
        }
        if (currentCat.tag == "legsTwoCat")
        {
            PlayerPrefs.DeleteKey("legsTwoCat");
        }
        if (currentCat.tag == "legsThreeCat")
        {
            PlayerPrefs.DeleteKey("legsThreeCat");
        }
        if (currentCat.tag == "equalOneCat")
        {
            PlayerPrefs.DeleteKey("equalOneCat");
        }
        if (currentCat.tag == "equalTwoCat")
        {
            PlayerPrefs.DeleteKey("equalTwoCat");
        }
        if (currentCat.tag == "equalThreeCat")
        {
            PlayerPrefs.DeleteKey("equalThreeCat");
        }
    }

        void updateCatStats(GameObject prevCat, GameObject currCat)
    {
        currCat.GetComponent<BasicCat>().agilityGS = prevCat.GetComponent<BasicCat>().agilityGS;
        currCat.GetComponent<BasicCat>().strengthGS = prevCat.GetComponent<BasicCat>().strengthGS;
    }

        IEnumerator evolutionAnimation(GameObject evolutionCat)
    {
        partSystem.Play();
        backgroundMusic.GetComponent<AudioSource>().mute = true;
        evolutionMusic.GetComponent<AudioSource>().mute = false;

        yield return new WaitForSeconds(10f);

        partSystem.Stop();
        evolutionMusic.GetComponent<AudioSource>().mute = true;
        backgroundMusic.GetComponent<AudioSource>().mute = false;

        Vector3 pos = currentCat.transform.position;
        currentCat.SetActive(false);
        evolutionCat.transform.position = pos;
        evolutionCat.SetActive(true); 

        evolutionCat.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1400));
        currentCat = evolutionCat;
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Calendar");
    }
}
