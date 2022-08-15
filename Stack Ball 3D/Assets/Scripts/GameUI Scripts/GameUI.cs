using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // IgnoreUI ekledi�imiz
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject homeUI, inGameUI, finishUI, gameOverUI;
    public GameObject allbtns;

    private bool btns;

    [Header("PreGame")]
    public Button soundBtn;
    public Sprite soundOnSpr, soundOffSpr; // ses a� kapat


    [Header("InGame")]  // InGame = oyunda
    public Image levelSlider; // levelSlider = seviye kayd�rac�
    public Image currentLevelImg; // ba�lang�� leveli
    public Image nextLevelImg; // sonraki level
    public Text currentLevelText, nextLevelText;

    [Header("Finish")] // biti�
    public Text finishLevelText;

    [Header("GameOver")]
    public Text gameOverScoreText;
    public Text gameOverBestText;


    private Material playerMat;
    private Player player; // HomeUI ile yazd�k.
   
    void Awake()
    {
        playerMat = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<Player>();


        levelSlider.transform.parent.GetComponent<Image>().color = playerMat.color + Color.gray;  // bir rengi sabit tutup di�er rengin hareket etmesini sa�l�yoruz.
        levelSlider.color = playerMat.color;
        currentLevelImg.color = playerMat.color;
        nextLevelImg.color = playerMat.color;

        soundBtn.onClick.AddListener(() => SoundManager.instance.SoundOnOff());
    }


     private void Start()
    {
        currentLevelText.text = FindObjectOfType<LevelSpawner>().level.ToString();
        nextLevelText.text = FindObjectOfType<LevelSpawner>().level + 1 + "";
    } 

    void Update()
    {
        if(player.playerState == Player.PlayerState.Prepare)
        {
            if (SoundManager.instance.sound && soundBtn.GetComponent<Image>().sprite != soundOnSpr)
                soundBtn.GetComponent<Image>().sprite = soundOnSpr;
            else if (!SoundManager.instance.sound && soundBtn.GetComponent<Image>().sprite != soundOffSpr)
                soundBtn.GetComponent<Image>().sprite = soundOffSpr;
        }

        if(Input.GetMouseButtonDown(0) && !IgnoreUI()  && player.playerState == Player.PlayerState.Prepare)
        {
            player.playerState = Player.PlayerState.Playing;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);
            finishUI.SetActive(false);
            gameOverUI.SetActive(false);
        }
        if (player.playerState == Player.PlayerState.Finish)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(true);
            gameOverUI.SetActive(false);

            finishLevelText.text = "Level " + FindObjectOfType<LevelSpawner>().level;
        }

        if (player.playerState == Player.PlayerState.Died)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(false);
            gameOverUI.SetActive(true);

            gameOverScoreText.text = ScoreManager.instance.score.ToString();
            gameOverBestText.text = PlayerPrefs.GetInt("HighScore").ToString();

            if (Input.GetMouseButtonDown(0))
            {
                ScoreManager.instance.ResetScore();
                SceneManager.LoadScene(0);
            }
        }
        // LevelSliderFill(0.5f);
    }
    private bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.GetComponent<Ignore>() != null)
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }
     
     //   print(raycastResultsList.Count);
        return raycastResultsList.Count > 0;
    }

    public void LevelSliderFill(float fillAmount) // fill amount = doldurma miktar�
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void Setting() // men�deki ayarlar butonunda m�zik butonunun ��kmas�.
    {
        btns = !btns;
        allbtns.SetActive(btns);
    }
}
