using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAndCollisionAttack_Card : Card
{

    public override string Name { get { return "Rush and Collision Attack"; } }
    public override Rarity rarity { get { return Rarity.basic; } }
    public override int Speed { get { return 5; } }
    public override int ID { get { return 7; } }
    public override int TargetNum { get { return 1; } set { } }

    public override IEnumerator Play()
    {
        Info.owner_ID = 0;
        BattleData.playerData.currentEnergy -= 1;
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

        Notation.Add(this.transform.Find("RangeNotation").gameObject);
        Notation.Add(this.transform.Find("SelectionNotation").gameObject);
        //better to design two notation

       // BattleData.CardReadyToPlay = this;
        UI.ShowNotation(Notation,Info);
        //assign the functionality to grids in info.direction
        yield return new WaitUntil(() => TargetNum == 0);
        //disable the grid selection function
        UpdateData(0, ID, Info);
    }

    public override void Activate(InfoForActivate Info)
    {


        Vector2 oriPos = new Vector2(float.Parse(Info.otherInfo[0]), float.Parse(Info.otherInfo[1]));

        Vector2 moveDir = Info.Selection[0] - oriPos;

        int Distance = (int)moveDir.magnitude - 1;

        for (int i = 1; i < BattleData.EnemyDataList.Count + 1; i++)
        {
            if (BattleData.EnemyDataList[i].position == Info.Selection[0])
            {
                BattleData.EnemyData data = BattleData.EnemyDataList[i];
                data.currentHealth -= 2 + Distance;
                BattleData.EnemyDataList[i] = data;
            }
        }
        if (moveDir.x > 0)
            BattleData.playerData.position = Info.Selection[0] - new Vector2(-1, 0);
        else if (moveDir.x < 0)
            BattleData.playerData.position = Info.Selection[0] - new Vector2(1, 0);
        else if (moveDir.y < 0)
            BattleData.playerData.position = Info.Selection[0] - new Vector2(0, 1);
        else
            BattleData.playerData.position = Info.Selection[0] - new Vector2(0, -1);
    }
}
