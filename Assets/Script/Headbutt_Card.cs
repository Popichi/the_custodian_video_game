using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbutt_Card : Card
{
    public Headbutt_Card(BattleData battleData)
    {
        this.Name = "Headbutt";
        this.Rarity = .basic;
        this.Speed = 4;
        this.Range = 1;
        this.BattleCata = battleData;
        this.Notation = gameObject.transform.Find("notion").gameObject;
    }
    public override void IsPlayed()
    {
        this.Info.card = this;
        this.Info.owner_ID = 0;
        this.Info.direction.Add(Vector2(BattleData.battleData.player.position + Vector2(1, 0)));
        this.Info.direction.Add(Vector2(BattleData.battleData.player.position + Vector2(-1, 0)));
        this.Info.direction.Add(Vector2(BattleData.battleData.player.position + Vector2(0, 1)));
        this.Info.direction.Add(Vector2(BattleData.battleData.player.position + Vector2(0, -1)));
        Notation.SetActive(true);
    }

    public override void Acitvate()
    {
        this.BattleData.battleData.EnemyDataList.at(0).health -= 3;
    }
}
