using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.QuestSystem
{
    public class ExperienceReward : IQuestReward        //����ġ ������ �����ϴ� Ŭ����
    {
        private int experienceAmount;       //�������� ������ ����ġ��
        public ExperienceReward(int amount)
        {
            this.experienceAmount = amount;
        }
        public void Grant(GameObject player)
        {
            //TODO : ���� ����ġ ���� ���� ����
            Debug.Log($"Granted {experienceAmount} experience");
        }

        private string GetDescription() => $"{experienceAmount}Experience Points";      //���� ������ ���ڿ��� ��ȯ
    }
}
