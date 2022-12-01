
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    //every card is a prefab and has its own script named card_cardname and this one is a superclass
    public int ID;
    public string Name;
    public Rarity rarity;
    public int Speed;
    public int Range;
    
    public BattleData BattleData;
    public GameObject Notation;
    public InfoForActivate Info;

    public Card()
    {

    }

    public enum Rarity
    {
        basic,
        common,
        rare,
        epic,
        legendary,
    }

    public struct InfoForActivate
    {
        public List<Vector2> direction;
        public int owner_ID;// 0 for player
        public Card card;
    }

    public abstract void IsPlayed();// shownotion, wait for choose a dir or a target, 

    //public abstract void GetDirection();

    public abstract void Acitvate();

    //Drag and drop function to play the card

}