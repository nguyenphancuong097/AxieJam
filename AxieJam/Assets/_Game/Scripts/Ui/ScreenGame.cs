using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenGame : ScreenBase
{
    [SerializeField] Button btnPlay;
    [SerializeField] Joystick joystick;
    [SerializeField] TextMeshProUGUI tmpHp;
    [SerializeField] TextMeshProUGUI tmpCountDown;

    [SerializeField] Image imgPause;
    [SerializeField] Sprite spritePause;
    [SerializeField] Sprite spriteResume;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip coolDownClip;
    public override void OnInit()
    {
        base.OnInit();
        btnPlay.onClick.AddListener(OnBtnPlayClick);
        GameManager.Instance.player.GetPCom<PlayerMove>().SetJoyStick(joystick);
    }

    public override void OnShow()
    {
        base.OnShow();
        SetPause(false);
    }

    public void SetHp(float hp)
    {
        tmpHp.SetText(((int)hp).ToString());
    }

    public void UpdateCountDown(string text)
    {
        tmpCountDown.SetText(text);
    }

    public void SetInput(bool isActive)
    {
        joystick.OnPointerUp(null);
        joystick.enabled = isActive;
    }

    public void SetPause(bool isPause)
    {
        imgPause.sprite = isPause ? spriteResume : spritePause;
        if (isPause)
        {
            audioSource.Pause();
            imgPause.sprite = spriteResume;
        }
        else
        {
            audioSource.UnPause();
            imgPause.sprite = spritePause;
        }
    }

    public void OnBtnPlayClick()
    {
        btnPlay.gameObject.SetActive(false);
        GameManager.Instance.StartLevel();
        AudioManager.Instance.PlaySound(audioSource, coolDownClip);
        AudioManager.Instance.PlayOnceShot(AudioType.CoolDown);
    }
}
