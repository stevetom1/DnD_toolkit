using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dices : MonoBehaviour
{
    int roll;

    void Update()
    {

    }
    public void RollD4()
    {
        roll = Random.Range(1, 5);
        Debug.Log($"You rolled a: {roll}");
    }
    public void RollD6()
    {
        roll = Random.Range(1, 7);
        Debug.Log($"You rolled a: {roll}");
    }
    public void RollD10()
    {
        roll = Random.Range(1, 11);
        Debug.Log($"You rolled a: {roll}");
    }
    public void RollD12()
    {
        roll = Random.Range(1, 13);
        Debug.Log($"You rolled a: {roll}");
    }
    public void RollD20()
    {
        roll = Random.Range(1, 21);
        Debug.Log($"You rolled a: {roll}");
    }
}