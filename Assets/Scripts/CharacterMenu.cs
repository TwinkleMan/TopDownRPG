using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, coinsText, upgradeCostText, xpText;

    //logic fields
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void onArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            //if no more characters (went too far)
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }
    public void onSelectClick()
    {
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void onUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //update the character info
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponSprites.Count - 1)
        {
            upgradeCostText.text = "MAX";
        }
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString() + " / " + GameManager.instance.player.maxHitpoint.ToString();
        coinsText.text = GameManager.instance.coins.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //xp bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total XP";
            xpBar.localScale = new Vector3(1.0f, 1, 1);
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float progressRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(progressRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }

    }

    public void Pause()
    {
        GameManager.instance.Pause();
    }

    public void Resume()
    {
        GameManager.instance.Resume();
    }
}
