using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering.LookDev;

public class UIController : PanelBase
{
    public static UIController instance; // Singleton instance of UIController
    public Slider HealthSlider;
    public Slider AmrorSlider; // Armor slider to show remaining armor
    
  
    [Header("UI Reference")]
    public TMPro.TextMeshProUGUI ammoText;
    [Header("Weapon UI")]
    public Image weaponIcon; // 显示当前武器图标的Image组件
    public Sprite[] weaponSprites; // 4种武器的图标（按顺序对应）

    public TMPro.TextMeshProUGUI HealthPackText; // 显示当前武器名称的Text组件
    public TMPro .TextMeshProUGUI AdrenalineText; // 显示当前血包数量的Text组件
    public TMPro.TextMeshProUGUI AmmunitionBox; // 显示当前血包数量的Text组件
    public TMPro.TextMeshProUGUI GunDamage; // 显示当前弹药盒数量的Text组件

    [Header("Death UI")]
    public CanvasGroup deathCanvasGroup;
    public GameObject gameOverPanel;
    public List<Image> heartsUI;



    [Header("Game Over Buttons")]
    public Button exitButton;       // 退出游戏按钮
    public Button homeButton;       // 返回主菜单按钮
    public Button replayButton;     // 重新开始按钮

    public AudioSource musicSource;   // ���� Audio Source
    public Slider volumeSlider;       // ���� Slider
    public GameObject menulist;//menu list
    
    [SerializeField] private GameObject ESCguidePanel;
    [SerializeField] private Button ESCcloseGuideButton;

    private Coroutine currentFadeCoroutine;
    private bool isDeathPanelActive = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
            DontDestroyOnLoad(gameObject); // 确保UI控制器持久化
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
        if (deathCanvasGroup != null)
        {
            deathCanvasGroup.gameObject.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            
        }
        menulist.SetActive(false);
    }

    protected override void Init()
    {
        HealthSlider.value = (float)PlayerHealthController.instance.currentHealth / PlayerHealthController.instance.maxHealth;
        AmrorSlider.value = (float)PlayerHealthController.instance.remainingArmorAbsorb / PlayerHealthController.instance.maxArmorAbsorb;
        HealthPackText.text = PlayerHealthController.instance.HealthBoxAmount.ToString();
        AmmunitionBox.text = PlayerHealthController.instance.AmmunitionBoxAmount.ToString();
        AdrenalineText.text = PlayerHealthController.instance.AdrenalineBoxAmount.ToString();
        BulletController bullet = PlayerController.instance.activeGun.bulletPrefab.GetComponent<BulletController>();
        GunDamage.text = bullet.Damage.ToString();

        if (volumeSlider != null && musicSource != null)
        {
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        if (ESCguidePanel != null)
        {
            ESCguidePanel.SetActive(false);
        }

        // 设置关闭按钮事件
        if (ESCcloseGuideButton != null)
        {
            ESCcloseGuideButton.onClick.AddListener(CloseGuidePanel);
        }
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menulist.activeSelf)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void UpdateDamageUI()
    {
        if (GunDamage != null)
        {
            BulletController bullet = PlayerController.instance.activeGun.bulletPrefab.GetComponent<BulletController>();
            GunDamage.text = "Damage: " + bullet.Damage;
        }
        else
        {
            Debug.LogWarning("GunDamage UI reference is null, cannot update damage UI.");
        }
    }

    private void OpenMenu()
    {
        menulist.SetActive(true);
        Time.timeScale = 0;

        // 显示鼠标并解锁
        UnityEngine.Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // 确保按钮可交互
        exitButton.interactable = true;
        homeButton.interactable = true;
        replayButton.interactable = true;
        volumeSlider.interactable = true;

        // 设置默认选中按钮（解决键盘导航问题）
        if (exitButton != null)
        {
            exitButton.Select();
            exitButton.OnSelect(null); // 触发高亮状态
        }
    }

    private void CloseMenu()
    {
        menulist.SetActive(false);
        Time.timeScale = 1;

        // 隐藏鼠标并锁定
        UnityEngine.Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Return()
    {
        CloseMenu();
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
    public void OpenGuidePanel()
    {
        if (ESCguidePanel != null)
        {
            ESCguidePanel.SetActive(true);
        }
    }

    public void CloseGuidePanel()
    {
        if (ESCguidePanel != null && ESCguidePanel.activeSelf)
        {
            ESCguidePanel.SetActive(false);
        }
    }

    public void UpdateAmmoBoxUI()
    {
        // 确保UI引用不为空
        if (AmmunitionBox != null)
        {
            // 更新弹药盒UI文本
            AmmunitionBox.text = PlayerHealthController.instance.AmmunitionBoxAmount.ToString();
        }
        else
        {
            Debug.LogWarning("AmmunitionBox UI reference is null, cannot update ammo box UI.");
        }
    }

    public void UpdateAdrenalineUI()
    {
        // 确保UI引用不为空
        if (AdrenalineText != null)
        {
            // 更新肾上腺素UI文本
            AdrenalineText.text = PlayerHealthController.instance.AdrenalineBoxAmount.ToString();
        }
        else
        {
            Debug.LogWarning("AdrenalineText UI reference is null, cannot update adrenaline UI.");
        }
    }

    public void UpdateAmmoUI()
    {
        // 确保UI引用不为空
        if (ammoText != null)
        {
            Debug.Log("Max = " + PlayerController.instance.activeGun.maxAmmo);
            // 更新弹药UI文本（格式："当前弹药/最大容量"）
            ammoText.text = (PlayerController.instance.activeGun.currentAmmo + "/" + PlayerController.instance.activeGun.maxAmmo);
              // $"{PlayerController.instance.activeGun.currentAmmo}/{PlayerController.instance.activeGun.maxAmmo}";
        }
        else
        {
            Debug.LogWarning("PlayerController or activeGun is null, cannot update ammo UI.");
        }
    }

    public void UpdateAmmunitionUI()
    {
        if (AmmunitionBox != null)
        {
            AmmunitionBox.text = PlayerHealthController.instance.AmmunitionBoxAmount.ToString();
        }
    }

    public void UpdateWeaponUI()
    {
        // 直接替换为当前武器的图标
        if (weaponIcon != null)
        {
            weaponIcon.sprite = weaponSprites[PlayerController.instance.currentGunIndex];
        }
    }

    public void UpdateHealthPack()
    {
        HealthPackText.text = PlayerHealthController.instance.HealthBoxAmount.ToString();
    }
    public void UpdateHeartsUI(int deathCount)
    {
        if (heartsUI == null) return;

        for (int i = 0; i < heartsUI.Count; i++)
        {
            if (heartsUI[i] != null)
                heartsUI[i].enabled = i < deathCount;
        }
    }
    // 显示死亡面板（带淡入效果）
    public IEnumerator ShowDeathPanel(float duration = 1f)
    {
        if (deathCanvasGroup == null) yield break;
        if (isDeathPanelActive) yield break;

        // 停止正在进行的淡出效果
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        deathCanvasGroup.gameObject.SetActive(true);
        isDeathPanelActive = true;

        currentFadeCoroutine = StartCoroutine(FadeCanvasGroup(deathCanvasGroup, 0, 1, duration));
        yield return currentFadeCoroutine;
    }

    // 隐藏死亡面板（带淡出效果）
    public IEnumerator HideDeathPanel(float duration = 1f)
    {
        if (deathCanvasGroup == null) yield break;
        if (!isDeathPanelActive) yield break;

        // 停止正在进行的淡入效果
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        currentFadeCoroutine = StartCoroutine(FadeCanvasGroup(deathCanvasGroup, 1, 0, duration));
        yield return currentFadeCoroutine;

        deathCanvasGroup.gameObject.SetActive(false);
        isDeathPanelActive = false;
    }

    // 显示游戏结束面板
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Cursor.visible = true; // 确保鼠标可见
            Cursor.lockState = CursorLockMode.None; // 解锁鼠标
            // 确保死亡面板隐藏
            if (deathCanvasGroup != null && isDeathPanelActive)
            {
                deathCanvasGroup.gameObject.SetActive(false);
                isDeathPanelActive = false;
                Cursor.visible = false; // 确保鼠标可见
                Cursor.lockState = CursorLockMode.Locked; // 解锁鼠标
            }
        }
    }
    public void HideGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    // =============== 按钮事件处理 ===============
    // ================ 游戏结束面板按钮功能 ================

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("退出游戏...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // 返回主菜单
    public void GoToMainMenu()
    {
        Debug.Log("返回主菜单...");

        // 重置游戏状态
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        // 加载主菜单场景
        SceneManager.LoadScene("1");

        // 重置UI控制器
        Destroy(gameObject);
    }

    // 重新开始游戏
    public void RestartGame()
    {
        Debug.Log("重新开始当前关卡...");

        // 获取当前场景名称
        string currentScene = SceneManager.GetActiveScene().name;

        // 重新加载当前场景
        SceneManager.LoadScene(currentScene);
    }

    // 淡入淡出辅助函数
    private IEnumerator FadeCanvasGroup(CanvasGroup group, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }

        group.alpha = endAlpha;
    }

}
