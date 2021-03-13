using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public ProgressBar manaBar;
    private LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.getInstance();
        levelManager.setCurrentMana(100.0f);
        manaBar.BarValue = levelManager.getCurrentMana();

    }

    public void useMana(float value)
    {
        levelManager.setCurrentMana(levelManager.getCurrentMana() - value);
        if (levelManager.getCurrentMana() < 0.0f)
        {
            levelManager.setCurrentMana(0.0f);
        }
        manaBar.BarValue = levelManager.getCurrentMana();
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
            levelManager.setCurrentMana(levelManager.getCurrentMana() + 0.1f);
            manaBar.BarValue = levelManager.getCurrentMana();
        }

        if (levelManager.getCurrentMana() >= 100.0f)
        {
            levelManager.setCurrentMana(100.0f);
            return;
        }
    }
}
