using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenulist : PanelBase
{

    public AudioSource musicSource;   // ���� Audio Source
    public Slider volumeSlider;       // ���� Slider
    public GameObject menulist;//menu list

    [SerializeField] private bool menuKeys = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Init()
    {
        if (volumeSlider != null && musicSource != null)
        {
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (menuKeys)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menulist.SetActive(true);
                menuKeys = false;
                Time.timeScale = 0;//ʱ����ͣ

                UnityEngine.Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menulist.SetActive(false);
            menuKeys = true;
            Time.timeScale = 1;//ʱ������
            UnityEngine.Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void Return()
    {
        menulist.SetActive(false);
        menuKeys = true;
        Time.timeScale = 1;//ʱ������
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
