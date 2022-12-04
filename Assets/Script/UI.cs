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
    const float CARD_HEIGHT = 4;
    const float CARD_WIDTH = 2;

    static List<List<Vector3>> pos;

    static Vector3 mouseWorldPos;
    private void Start()
    {

        //LoadBattleBegin(null);

        GameObject testcard = Resources.Load("TestCard") as GameObject;

        GameObject testicon = Resources.Load("Circle") as GameObject;

        card1 = GameObject.Find("Card1");
        card2 = GameObject.Find("Card2");
        card3 = GameObject.Find("Card3");
        card4 = GameObject.Find("Card4");

        card1 = GameObject.Instantiate(testcard, card1.transform.position, transform.rotation) as GameObject;
        GameObject.Instantiate(testcard, card2.transform.position, transform.rotation);
        GameObject.Instantiate(testcard, card3.transform.position, transform.rotation);

        timeline = GameObject.Find("TimeLine");
        GameObject.Instantiate(testicon, timeline.transform.position + new Vector3(4.24f, -1.2f,0), transform.rotation);
        GameObject.Instantiate(testicon, timeline.transform.position + new Vector3(2.61f, -1.2f,0), transform.rotation);
        UpdateTimeLine();
        

    }
    public static void UpdateHandCard(BattleData battleData)// After drawing a new card, reorgnize the hand card(align right) and move a card from deck to hand at the most left side.
    {

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[0].name) as GameObject, 
            card1.transform.position, card1.transform.rotation);

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[1].name) as GameObject,
            card2.transform.position, card2.transform.rotation);

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[2].name) as GameObject,
            card3.transform.position, card3.transform.rotation);

        GameObject.Instantiate(Resources.Load(battleData.playerData.handCard[3].name) as GameObject,
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
        

        //TEST CASE
        /* health.maxValue = 20;
         health.value = 10;

         energy.maxValue = 20;
         energy.value = 20;*/

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
                GameObject.Instantiate(Resources.Load(timeLineSlots[i][j].card.name) as GameObject,
            timeline.transform.position + pos[i][j],timeline.transform.rotation);
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
        //one line under timeline for enemy, one line above for player, one line for duplication
       float offsite = CARD_WIDTH * TIME_LINE_LENGTH / 2;
       float heightOffsite = CARD_HEIGHT;
       for (int i = 0; i < TIME_LINE_LENGTH; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                pos[i][j] = new Vector3(i * CARD_WIDTH - offsite, j * CARD_HEIGHT / 2 - heightOffsite, 0);
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
