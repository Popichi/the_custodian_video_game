using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleLevelDriver : MonoBehaviour
{
    public bool Paused;
    public bool BattleOver;

    public static List<List<Card.InfoForActivate>> TimeLineSlots;

    private void Start()
    {
        BeginABattleLevel(1);
    }
    public void BeginABattleLevel(int ID)
    {
        List<Card.InfoForActivate>[] TimeLinearray = new List<Card.InfoForActivate>[10];
        TimeLineSlots = TimeLinearray.ToList();
        for(int i = 0; i < TimeLineSlots.Count; i++)
        {
            TimeLineSlots[i] = new List<Card.InfoForActivate>();
        }

        BattleData.BattleLevelInit(ID);
        UI.LoadBattleBegin();
        GameData.currentState = GameData.state.Battle;
        StartCoroutine(EnableTimeLineSlots());
        StartCoroutine(BattleLevelGame());
    }

    private IEnumerator BattleLevelGame()
    {        
        yield return new WaitUntil(() => BattleOver == true);
        GameData.currentState = GameData.state.WorldMap;
        //TODO: load gameover or show battle summary
    }

    private IEnumerator EnableTimeLineSlots()
    {
        for (int i = 1; i < BattleData.EnemyDataList.Count + 1; i++)
        {
            BattleData.EnemyData enemy = BattleData.EnemyDataList[i];
            enemy.obj.GetComponent<Enemy>().EnermyChooseACardToPlay();
        }
        UI.UpdateTimeLine(TimeLineSlots);
        while (!Paused && !BattleOver)
        {
            while (Paused)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1);
            //Remove the cards at time steps 0 and add a new list at  time step 10
            List<Card.InfoForActivate> currentCards = TimeLineSlots[0];
            TimeLineSlots.RemoveAt(0);
            TimeLineSlots.Add(new List<Card.InfoForActivate>());

            UI.MoveTimeLine();

            foreach (Card.InfoForActivate info in currentCards)
            {
                if (info.owner_ID == 0)
                {
                    BattleData.AbleToPalyCard = true;
                }
                
                info.card.Activate(info);
                if (info.owner_ID != 0)
                {
                    BattleData.NewCard.Add(info.card);//for duplication
                    BattleData.EnemyDataList[info.owner_ID].enemy.EnermyChooseACardToPlay();
                    UI.UpdateTimeLine(TimeLineSlots);
                }
            }
            
        }
    }

    public static void NewCardPlayed(Card.InfoForActivate info)
    {
        TimeLineSlots[info.card.Speed].Add(info);
        if (info.card.Speed == 0)//instant
        {
            info.card.Activate(info);
            return;
        }
        if (info.card.Speed <= 3)
            UI.UpdateTimeLine(TimeLineSlots);
        
    }

    public IEnumerable Pause()
    {
        Paused = true;
        UI.ShowDuplicationWin();
        yield return new WaitUntil(() => Paused == false);
    }

}
