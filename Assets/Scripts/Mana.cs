using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public ProgressBar manaBar;
    private LevelManager levelManager;
    private float manaIncrease = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.GetInstance();
        levelManager.setCurrentMana(100.0f);
        manaBar.BarValue = levelManager.GetCurrentMana();

    }

    public void UseMana(float value)
    {
        levelManager.setCurrentMana(levelManager.GetCurrentMana() - value);
        if (levelManager.GetCurrentMana() < 0.0f)
        {
            levelManager.setCurrentMana(0.0f);
        }
        manaBar.BarValue = levelManager.GetCurrentMana();
    }

    public void increaseMana()
    {
        //do every second
        if (!levelManager.getManaIncreasing())
        {
            return;
        }
        else
        {
            levelManager.setCurrentMana(levelManager.GetCurrentMana() + manaIncrease);
            manaBar.BarValue = levelManager.GetCurrentMana();
        }

        if (levelManager.GetCurrentMana() >= 100.0f)
        {
            levelManager.setCurrentMana(100.0f);
            return;
        }
    }

    public void setManaIncrease(float newIncreaseValue)
    {
        manaIncrease = newIncreaseValue;
    }
}
