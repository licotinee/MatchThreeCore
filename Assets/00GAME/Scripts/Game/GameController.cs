using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH2ten.Game.Common;
using System.Text.RegularExpressions;
using System;

public class GameController : Singleton<GameController> 
{
    [SerializeField] PlayerController[] _players = new PlayerController[2];
    public PlayerController[] Players => _players;

    [SerializeField] int _currentTurn;

    public void CurrentPlayerAction(List<GameObject> tiles, bool isCombo = false)
    {
        if (isCombo)
        {
            _players[_currentTurn].ChargeStart();

        }

        if (tiles == null || tiles.Count == 0)
        {
            if (_players[_currentTurn].playerState == PlayerController.PLAYER_STATE.Skill)
                _players[_currentTurn].ChargeEnd();
            //else changeTurn();
            return;
        } 

        Dictionary<CandyColor, int> CandyColorCount = new Dictionary<CandyColor, int>();

        foreach (GameObject g in tiles)
        {
            CandyColor c = g.GetComponent<Candy>().color;
            if (CandyColorCount.ContainsKey(c))
            {
                CandyColorCount[c]++;
            }
            else
            {
                CandyColorCount.Add(c,1);
            }
        }

        if (CandyColorCount.ContainsKey(CandyColor.Red))
        {
           
            string text = $"-{CandyColorCount[CandyColor.Red]} Physic DMG";
            UIController.Instant.ShowText(_players[_currentTurn == 0 ? 1 : 0].transform.position, text , Color.red);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Blue))
        {
           
            string text = $"-{CandyColorCount[CandyColor.Blue]} Magic DMG";
            UIController.Instant.ShowText(_players[_currentTurn == 0 ? 1 : 0].transform.position, text, Color.blue);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Purple))
        {
            
            string text = $"+{CandyColorCount[CandyColor.Purple]} Physic Def";
            UIController.Instant.ShowText(_players[_currentTurn].transform.position, text, new Color(0.867f,0,1));
        }

        if (CandyColorCount.ContainsKey(CandyColor.Yellow))
        {
           
            string text = $"+{CandyColorCount[CandyColor.Yellow]} Magic Def";
            UIController.Instant.ShowText(_players[_currentTurn].transform.position, text, Color.yellow);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Green))
        {
           
            string text = $"+{CandyColorCount[CandyColor.Green]} HP";
            UIController.Instant.ShowText(_players[_currentTurn].transform.position, text, Color.green);
        }

        if (_players[_currentTurn].playerState == PlayerController.PLAYER_STATE.Skill)
            return;

        if (CandyColorCount.ContainsKey(CandyColor.Red))
        {
            _players[_currentTurn].Attack(_players[_currentTurn == 0 ? 1 : 0], CandyColorCount[CandyColor.Red],IDamgeAble.DamageType.Physic);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Blue))
        {
            _players[_currentTurn].Attack(_players[_currentTurn == 0 ? 1 : 0], CandyColorCount[CandyColor.Blue], IDamgeAble.DamageType.Magic);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Purple))
        {

            _players[_currentTurn].GetBuff(CandyColorCount[CandyColor.Purple],IBuffAble.BuffType.PhysicsDef);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Yellow))
        {

            _players[_currentTurn].GetBuff(CandyColorCount[CandyColor.Yellow], IBuffAble.BuffType.MagicsDef);
        }

        if (CandyColorCount.ContainsKey(CandyColor.Green))
        {

            _players[_currentTurn].GetBuff(CandyColorCount[CandyColor.Green], IBuffAble.BuffType.HP);
        }
 
    }
}
