using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isMenuActive;

    private void Awake()
    {

        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public GameObject crosshair;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int coins;
    public int experience;

    //Floating text
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel) 
            return false;

        if(coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    //experience
    public int GetCurrentLevel()
    {
        int resultLevel = 0;
        int requiredXp = 0;

        while (experience >= requiredXp)
        {
            requiredXp += xpTable[resultLevel];
            resultLevel++;

            if (resultLevel == xpTable.Count)
                return resultLevel;
        }
        
        return resultLevel;
    }
    public int GetXpToLevel(int level)
    {
        int resultLevel = 0;
        int requiredXp = 0;

        while (resultLevel < level)
        {
            requiredXp += xpTable[resultLevel];
            resultLevel++;
        }

        return requiredXp;
    }
    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLevelUp();
    }

    //save state
    /*
     * INT preferredSkin
     * INT coins
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        string save = "";

        save += "0" + "|";
        save += coins.ToString() + "|";

        save += experience.ToString() + "|";
        save += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", save);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        coins = int.Parse(data[1]);
        
        //Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        player.SetLevel(GetCurrentLevel());

        //change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

        Debug.Log("LoadState");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
