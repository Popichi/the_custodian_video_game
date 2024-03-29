using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormStrike : Card
{
    [SerializeField] private AudioSource Audio;
    public override string Name { get { return "StormStrike"; } }
    public override Rarity rarity { get { return Rarity.legendary; } }
    public override int Speed { get { return 8; } }
    public override int ID { get { return 21; } }
    public override IEnumerator Play()
    {
        BattleData.playerData.currentEnergy -= 5;
        UI.UpdatePlayerData();
        Info.owner_ID = 0;
        yield return new WaitForSeconds(0.1f);
        UpdateData(0, ID, Info);
    }

    public override void Activate(InfoForActivate Info)
    {
        Audio.Play();
        Info.animator.SetTrigger("Attack");
        List<Vector2> range = new List<Vector2>();
        range.Add(new Vector2(1, 0));
        range.Add(new Vector2(0, 1));
        range.Add(new Vector2(0, -1));
        range.Add(new Vector2(-1, 0));
        range.Add(new Vector2(1, 1));
        range.Add(new Vector2(-1, -1));
        range.Add(new Vector2(-1, 1));
        range.Add(new Vector2(1, -1));
        range.Add(-new Vector2(2, 0));
        range.Add(-new Vector2(0, 2));
        range.Add(-new Vector2(0, -2));
        range.Add(-new Vector2(2, 0));
        range.Add(new Vector2(2, 2));
        range.Add(new Vector2(-2, -2));
        range.Add(new Vector2(-2, 2));
        range.Add(new Vector2(2, -2));
        if (Info.owner_ID == 0)
        {
            for (int i = 1; i < BattleData.EnemyDataList.Count + 1; i++)
            {
                if (BattleData.EnemyDataList[i].position == range[0] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[1] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[2] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[3] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[4] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[5] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[6] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[7] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[8] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[9] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[10] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[11] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[12] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[13] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[14] + BattleData.playerData.position ||
                    BattleData.EnemyDataList[i].position == range[15] + BattleData.playerData.position
                    )
                {
                    BattleData.EnemyData data = BattleData.EnemyDataList[i];
                    data.currentHealth -= 10;
                    BattleData.EnemyDataList[i] = data;
                    UI.UpdateEnemyData(i);
                    return;
                }
            }
        }
        else
        {
            if (BattleData.playerData.position == range[0] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[1] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[2] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[3] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[4] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[5] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[6] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[7] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[8] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[9] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[10] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[11] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[12] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[13] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[14] + BattleData.EnemyDataList[Info.owner_ID].position
                || BattleData.playerData.position == range[15] + BattleData.EnemyDataList[Info.owner_ID].position)
            {
                BattleData.playerData.currentHealth -= 10;
                UI.UpdatePlayerData();
            }
        }
    }

    public override void ReSetTarget()
    {
        TargetNum = 0;
    }
}
