using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public class AdReward : MonoBehaviour
{
    public string rewardID;

    // Когда объект с данным классом станет активным, метод OnReward подпишется на событие вознаграждения
    private void OnEnable()
    {
        YG2.onRewardAdv += OnReward;
    }

    // Необходимо отписывать методы от событий при деактивации объекта
    private void OnDisable()
    {
        YG2.onRewardAdv -= OnReward;
    }

    // Вызов рекламы за вознаграждение
    public void MyRewardAdvShow(string id)
    {
        YG2.RewardedAdvShow(rewardID);
    }

    // Метод подписан на событие OnReward (ивент вознаграждения)
    private void OnReward(string id)
    {
        // Проверяем ID вознаграждения. Если совпадает с тем ID, с которым вызывали рекламу, то вознаграждаем.
        if (id == rewardID)
        {
            YG2.saves.playerData.money += 3000;
            FindObjectOfType<DisplayMoney>().UpdateInfo(YG2.saves.playerData.money);
            YG2.SaveProgress();
        }
    }
}
