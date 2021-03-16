using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    [SerializeField]
    private ProgressBarCircle cooldownProgress;
    [SerializeField]
    private Button correspondingButton;
    [SerializeField]
    private float cooldownTime;
    private float cooldownStep;
    private bool cooldownStarted = false;
    private bool oldCooldownBool = false;

    // Start is called before the first frame update
    void Start()
    {
        cooldownProgress.gameObject.SetActive(false);
        cooldownProgress.BarValue = 0.0f;
        correspondingButton = GetComponent<Button>();
        cooldownStep = (100.0f / cooldownTime) / 10.0f;
    }

    void Update()
    {

    }

    public void startCooldown()
    {
        oldCooldownBool = cooldownStarted;
        cooldownStarted = true;
        if (cooldownStarted != oldCooldownBool)
        {
            correspondingButton.enabled = false;
            cooldownProgress.gameObject.SetActive(true);
            InvokeRepeating("increaseCooldown", 0.0f, 0.1f);
        }
    }

    private void increaseCooldown()
    {
        if (cooldownProgress.BarValue >= 100.0f)
        {
            cooldownProgress.BarValue = 0.0f;
            cooldownProgress.gameObject.SetActive(false);
            correspondingButton.enabled = true;
            cooldownStarted = false;
            CancelInvoke();
            return;
        }
        else
        {
            cooldownProgress.BarValue = cooldownProgress.BarValue + cooldownStep;
        }
    }
}
