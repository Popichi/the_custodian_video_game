using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    //left first 
    static GameObject card1;
    static GameObject card2;
    static GameObject card3;
    static GameObject card4;

    static Slider health;
    static Slider energy;
    //static int RAM;

    static GameObject timeline;
    const int TIME_LINE_LENGTH = 10;
    const float CARD_HEIGHT = 2;
    const float CARD_WIDTH = 1;

    //static List<List<Vector3>> pos;
    static Vector3[,] pos;

    static Vector3 mouseWorldPos;


    //test case
    private void Start()
    {

        BattleData battle = new BattleData();
        battle.playerData.maxEnergy = 20;
        battle.playerData.currentEnergy = 18;

        battle.playerData.maxHealth = 20;
        battle.playerData.currentHealth = 10;


        Card testcard1 = new Card();
        testcard1.Name = "TestCard";

        Card testcard2 = new Card();
        testcard2.Name = "Circle";

        battle.playerData.handCard = new List<Card> { testcard1,testcard2,testcard2,testcard1 };
        LoadBattleBegin(battle);

        List<List<Card>> testlist = new List<List<Card>>();
        List<Card> list1 = new List<Card> { testcard2, testcard2 };
        List<Card> list2 = new List<Card> { testcard2 };
        List<Card> list3 = new List<Card> { testcard2, testcard2 };
        testlist.Add(list1);
        testlist.Add(list2);
        testlist.Add(list3);

        MoveTimeLine(testlist);
    }

    public static void MoveTimeLine(List<List<Card>> timeLineSlots)
    {
        for (int i = 0; i < timeLineSlots.Count; ++i)
        {
            for (int j = 0; j < timeLineSlots[i].Count; ++j)
            {
                GameObject.Instantiate(Resources.Load(timeLineSlots[i][j].Name) as GameObject,
            timeline.transform.position + pos[i, j], timeline.transform.rotation);
            }
        }
    }
    public static void UpdateHandCard(BattleData battleData)// After drawing a new card, reorgnize the hand card(align right) and move a card from deck to hand at the most left side.
    {

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[0].Name) as GameObject, 
            card1.transform.position, card1.transform.rotation);

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[1].Name) as GameObject,
            card2.transform.position, card2.transform.rotation);

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[2].Name) as GameObject,
            card3.transform.position, card3.transform.rotation);

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[3].Name) as GameObject,
            card4.transform.position, card4.transform.rotation);
    }

    public static void UpdateTimeLine()
    {

    }

    public static void LoadBattleBegin(BattleData battleData)
    {

        var sliders = Object.FindObjectsOfType<Slider>();
        health = sliders[0];
        energy = sliders[1];

        health.maxValue = battleData.playerData.maxHealth;
        energy.maxValue = battleData.playerData.maxEnergy;

        UpdatePlayerData(battleData.playerData);

        UpdateEnemyData(battleData.EnemyDataList);

        card1 = GameObject.Find("Card1");
        card2 = GameObject.Find("Card2");
        card3 = GameObject.Find("Card3");
        card4 = GameObject.Find("Card4");
        UpdateHandCard(battleData);


        timeline = GameObject.Find("TimeLine");
        InitPos();
        UpdateTimeLine();
        

    }

    public static IEnumerator ShowNotation(GameObject notion, Vector2 characterpos)
    {
        // notion is used to show how is the range or attack damage of a card. For example move left can be arrow pointing left covering one grid.
        //this function will show the notation in the direction coresponding to the mouse and character position. For example if the mouse is at the top side of character, then the notion will placed at the top side of character.
        
        GameObject.Instantiate(notion, new Vector2(characterpos.x, characterpos.y), Quaternion.identity);
        GameObject.Instantiate(notion, new Vector2(mouseWorldPos.x, mouseWorldPos.y), Quaternion.identity);

        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        while (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.Instantiate(notion, new Vector2(mouseWorldPos.x, mouseWorldPos.y), Quaternion.identity);

            //problem!!
       
        }
        UpdateTimeLine();
        yield return null;
    }

    public static void MoveTimeLine(List<List<Card.InfoForActivate>> timeLineSlots)
    {
        for(int i = 0; i < timeLineSlots.Count; ++i)
        {
           for(int j = 0; j < timeLineSlots[0].Count; ++j)
            {
                GameObject.Instantiate(Resources.Load(timeLineSlots[i][j].card.Name) as GameObject,
            timeline.transform.position + pos[i,j],timeline.transform.rotation);
            }
        }
    }
    public static void ShowDuplicationWin()
    {

    }

    public static void UpdatePlayerData(BattleData.PlayerData playerData)
    {
        health.value = playerData.currentHealth;
        energy.value = playerData.currentEnergy;
    }

    public static void UpdateEnemyData(Dictionary<int, BattleData.EnemyData> enemyDataList)
    {

    }

    public static void InitPos()
    {

        pos = new Vector3[10, 3];
        //one line under timeline for enemy, one line above for player, one line for duplication
       float offsite = CARD_WIDTH * TIME_LINE_LENGTH / 2;
       float heightOffsite = CARD_HEIGHT;
       for (int i = 0; i < TIME_LINE_LENGTH; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                pos[i,j] = new Vector3(i * CARD_WIDTH - offsite, j * (CARD_HEIGHT / 2) - heightOffsite, 0);
            }
        }
    }


    /*private void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //Debug.Log(Input.mousePosition);
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }*/
}
