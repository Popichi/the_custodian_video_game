using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleData : MonoBehaviour
{
    public static int BattleLevelCount { get { return 6; } } // just a random nubmer.. to be changed later
    public static int BattleLevelID;
    public static Dictionary<int,EnemyData> EnemyDataList;
    public static PlayerData playerData;
    public static EnvironmentData enviromentData;

    public static bool PlayingACard = false;//When the player plays a card, set Busy to true, when that card is activated, set this to false
    public static bool AbleToPalyCard = true;
    public static List<Card> NewCard; //for duplication

    public Deck deck;//?????
    

    public struct EnemyData
    {
        public Vector2 position;
        public Enemy enemy;//script
        public int maxHealth;
        public int currentHealth;
        //public int ID;
        public List<Card> handCard;
        public List<Card> discardPile;
        public List<Card> drawPile;
        public GameObject obj;
        public BuffAndDebuff.Buff buff;        
    }

    public struct EnvironmentData
    {
        
    }
    public struct PlayerData
    {
        public Vector2 position;
        public int maxHealth;
        public int currentHealth;
        public int maxEnergy;
        public int currentEnergy;
        public List<Card> handCard;
        public List<Card> discardPile;
        public List<Card> drawPile;
        public BuffAndDebuff.Buff buff;
    }
    

    public static void BattleLevelInit(int battleLevelID)
    {
        NewCard = new List<Card>();
        BattleLevelID = battleLevelID;
        LoadEnvironmentData();
        LoadEnermyData();
        LoadPlayerData();       
    }
    public static void LoadEnvironmentData(){
        
    }
    public static void LoadEnermyData(){
        EnemyDataList = new Dictionary<int,EnemyData>();
        //use streamreader to load data.
        //Thefollowing code is hardcode for testing.

        //EnemyData tree = new EnemyData();  
        //tree.position=new Vector2(4,-1);
        //tree.obj = GameObject.Find("TreeEnemy");
        //tree.enemy= tree.obj.GetComponent<Tree_Enemy>();
        //tree.enemy.EnemyID = 1;
        //tree.currentHealth= tree.enemy.Health;
        //tree.maxHealth = tree.enemy.Health;
        //tree.drawPile = tree.enemy.CardsDeck;
        //tree.handCard = new List<Card>();
        //tree.discardPile = new List<Card>();
        //StartingHandCards(1, tree.handCard, tree.drawPile, false);
        //EnemyDataList.Add(1,tree);

        EnemyData sheep = new EnemyData();
        sheep.position = new Vector2(1,-7);
        sheep.obj = GameObject.Find("SheepEnemy");
        sheep.enemy = sheep.obj.GetComponent<SheepEnemy>();
        sheep.enemy.EnemyID = 1;
        sheep.currentHealth = sheep.enemy.Health;
        sheep.maxHealth = sheep.enemy.Health;
        sheep.drawPile = sheep.enemy.CardsDeck;
        sheep.handCard = new List<Card>();
        sheep.discardPile = new List<Card>();
        StartingHandCards(3, sheep.handCard, sheep.drawPile, false);
        EnemyDataList.Add(1, sheep);

    }
    public static void LoadPlayerData(){
        playerData.position = new Vector2(0, 0);
        playerData.maxHealth = GameData.health;
        playerData.maxEnergy = GameData.Energy;
        playerData.currentHealth = playerData.maxHealth;
        playerData.currentEnergy = playerData.maxEnergy;
        playerData.drawPile = GameData.Deck;
        playerData.handCard= new List<Card>();
        playerData.discardPile = new List<Card>();
        StartingHandCards(4, playerData.handCard, playerData.drawPile,true);
        
    }

    public static void StartingHandCards(int num,List<Card> handcards,List<Card> drawPile,bool ShouldSetUI)
    {
        for(int i = 0; i < num; i++)
        {
            int randomNum = Random.Range(0, drawPile.Count);
            handcards.Add(drawPile[randomNum]);
            drawPile.RemoveAt(randomNum);      
        }
        if(ShouldSetUI)
            UI.SetOtherPilesInative();
    }

    public bool CheckWinCondition()
    {
        if(EnemyDataList.Count==0)
            return true;
        else
            return false;
    }
    public bool CheckLoseCondition()
    {
        if (playerData.currentHealth == 0)
            return true;
        else
            return false;
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
}
