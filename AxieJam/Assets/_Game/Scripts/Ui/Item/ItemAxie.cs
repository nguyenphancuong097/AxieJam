using Sirenix.OdinInspector.Editor.Drawers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ItemAxie : MonoBehaviour
{
    [SerializeField] PlayerType playerType;
    [SerializeField] Image imgIcon;
    [SerializeField] Image imgSkill;
    [SerializeField] Image imgFood;
    [SerializeField] Image imgPotion;

    [SerializeField] TextMeshProUGUI tmpLevel;
    [SerializeField] TextMeshProUGUI tmpAtk;
    [SerializeField] TextMeshProUGUI tmpHp;
    [SerializeField] TextMeshProUGUI tmpSpeed;
    [SerializeField] TextMeshProUGUI tmpCritRate;
    [SerializeField] TextMeshProUGUI tmpFood;
    [SerializeField] TextMeshProUGUI tmpPotion;
    [SerializeField] TextMeshProUGUI tmpSkillName;
    [SerializeField] TextMeshProUGUI tmpSkillLevel;
    [SerializeField] TextMeshProUGUI tmpDiscription;
    [SerializeField] TextMeshProUGUI tmpCooldown;
    [SerializeField] TextMeshProUGUI tmpDuration;

    [SerializeField] Button btnLevel;
    [SerializeField] Button btnSkill;

    private void Awake()
    {
        btnLevel.onClick.AddListener(OnBtnLevelClick);
        btnSkill.onClick.AddListener(OnBtnSkillClick);
    }
    public void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }

    public void UpdateUI()
    {
        var data = DataManager.Instance.GetData<DataUser>().GetDataPlayer(playerType);
        var asset = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerType);
        var footConfig = DataManager.Instance.GetAsset<FoodAsset>().GetConfig(playerType);
        var potionConfig = DataManager.Instance.GetAsset<PotionAsset>().GetConfig(playerType);
        var levelConfig = asset.data.GetLevelConfig(data.level);
        var skillConfig = asset.data.GetSkillConfig(data.levelSkill);

        float damage = asset.data.damage;
        float hp = asset.data.hp;
        float moveSpeed = asset.data.moveSpeed;
        float critRate = asset.data.critRate;

        for (int i = 1; i < data.level; i++)
        {
            PlayerStatConfig lvConfig = asset.data.GetLevelConfig(i);
            damage += lvConfig.damage;
            hp += lvConfig.hp;
            moveSpeed += lvConfig.moveSpeed;
            critRate += lvConfig.critRate;
        }


        int footRequire = levelConfig.foodRequire;
        int potionRequire = skillConfig.defaultValue.potionRequire;
        float cooldown = skillConfig.defaultValue.cooldown;
        float duration = skillConfig.defaultValue.duration;

        imgIcon.sprite = asset.avatar;
        imgSkill.sprite = asset.skillIcon;
        imgFood.sprite = footConfig.sprite;
        imgPotion.sprite = potionConfig.sprite;

        tmpLevel.SetText("LV.{0}", data.level);
        tmpAtk.SetText(Mathf.CeilToInt(damage).ToString());
        tmpHp.SetText(Mathf.CeilToInt(hp).ToString());
        tmpSpeed.SetText(moveSpeed.ToString());
        tmpCritRate.SetText("{0}%", Mathf.CeilToInt(critRate * 100));
        tmpFood.SetText("{0}/{1}", data.foodCount, footRequire);
        tmpPotion.SetText("{0}/{1}", data.potionCount, potionRequire);
        tmpSkillName.SetText(asset.skillName);
        tmpSkillLevel.SetText("LV.{0}", data.levelSkill);
        tmpDiscription.SetText(asset.discription);
        tmpCooldown.SetText("Cooldown: {0}s", cooldown);
        tmpDuration.SetText("Duration: {0}s", duration);

        btnLevel.interactable = data.foodCount >= footRequire;
        btnSkill.interactable = data.potionCount >= potionRequire;
    }

    public void OnBtnLevelClick()
    {
        var data = DataManager.Instance.GetData<DataUser>().GetDataPlayer(playerType);
        var asset = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerType);
        int require = asset.data.GetLevelConfig(data.level).foodRequire;

        DataManager.Instance.GetData<DataUser>().
            UpLevel(playerType, require);
        UpdateUI();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }

    public void OnBtnSkillClick()
    {
        var data = DataManager.Instance.GetData<DataUser>().GetDataPlayer(playerType);
        var asset = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerType);
        int require = asset.data.GetSkillConfig(data.level).defaultValue.potionRequire;

        DataManager.Instance.GetData<DataUser>().
            UpLevel(playerType, require);
        UpdateUI();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }

}