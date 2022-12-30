using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAndCollisionAttack: Card
{

    public override string Name { get { return "RushAndCollisionAttack"; } }
    public override Rarity rarity { get { return Rarity.basic; } }
    public override int Speed { get { return 5; } }
    public override int ID { get { return 7; } }

    public override IEnumerator Play()
    {
        BattleData.playerData.currentEnergy -= 1;
        UI.UpdatePlayerData();
        Info.direction.Add(BattleData.playerData.position + new Vector2(1, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(2, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(3, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(4, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(-1, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(-2, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(-3, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(-4, 0));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, 1));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, 2));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, 3));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, 4));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, -1));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, -2));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, -3));
        Info.direction.Add(BattleData.playerData.position + new Vector2(0, -4));

        // maximum move three grids and attack the enemy on the fourth grid.

        Info.otherInfo.Add(BattleData.playerData.position.x.ToString());
        Info.otherInfo.Add(BattleData.playerData.position.y.ToString());
        yield return new WaitForSeconds(0.1f);
        // BattleData.CardReadyToPlay = this;
        UI.ShowNotation(this);
        TileMapButton.MakeSelectable(this);

        yield return new WaitUntil(() => TargetNum == 0);
        TileMapButton.MakeUnSelectable();
       // BattleData.PlayingACard = false;
        UpdateData(0, ID, Info);
    }

    public override void Activate(InfoForActivate Info)
    {
        if (Info.owner_ID == 0) {
            Vector2 oriPos = BattleData.playerData.position;

            Vector2 moveDir = Info.Selection[0];

            int Distance = (int)moveDir.magnitude - 1;

            for (int i = 1; i < BattleData.EnemyDataList.Count + 1; i++)
            {
                if (BattleData.EnemyDataList[i].position == Info.Selection[0]+oriPos)
                {
                    BattleData.EnemyData data = BattleData.EnemyDataList[i];
                    data.currentHealth -= 2 + Distance;
                    BattleData.EnemyDataList[i] = data;
                    UI.UpdateEnemyData(i);
                }
            }
            BattleData.playerData.position += Info.Selection[0] - Info.Selection[0] / Info.Selection[0].magnitude;
            UI.UpdatePlayerData();
        }

        else
        {
            Vector2 oriPos = BattleData.EnemyDataList[Info.owner_ID].position;

            Vector2 moveDir = Info.Selection[0];

            int Distance = (int)moveDir.magnitude - 1;

            if (BattleData.playerData.position == Info.Selection[0] + oriPos)
            {
                BattleData.playerData.currentHealth -= 2 + Distance;
                UI.UpdatePlayerData();
            }        
            BattleData.EnemyData newData = BattleData.EnemyDataList[Info.owner_ID];
            newData.position += Info.Selection[0] - Info.Selection[0] / Info.Selection[0].magnitude;
            BattleData.EnemyDataList[Info.owner_ID] = newData;
            UI.UpdateEnemyData(Info.owner_ID);
            //Ui.UpdateEnemyData(newData.ID)

        }
    }

    private void Start()
    {
        TargetNum = 1;
        RangeNotation = "MovementNotation";
        SelectionNotation = "ArrowSelection";

    }
    public override void ReSetTarget()
    {
        TargetNum = 1;
    }
}