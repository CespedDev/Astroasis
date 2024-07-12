using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// Namespaces
using GameplayData;
using GameplayEvents;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameData gameData { get; private set; }

    [SerializeField]
    private GameEventSO StartEvent;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        CreateGameData();
        StartCoroutine(StartingLevel());
    }
    
    void Update()
    {
        
    }

    public void CreateGameData()
    {
        gameData = new GameData();
    }

    public void IncreaseScore(ObjectDataListSO data)
    {
        // Carlos 9-2-24: Make a scriptable object that works like a queue of score 
        // which where given by external objects like enemies.

        
        if (data.GetAttributeValue(AttributeType.SCORE_VALUE, out float value))
            gameData.score += value * gameData.bonus;
        

        Debug.Log($"Score: {gameData.score}");
    }

    public void ResetBonus()
    {
        gameData.bonus = 1;
    }

    public IEnumerator StartingLevel()
    {
        yield return new WaitForSeconds(2);
        StartEvent?.Raise();
        Debug.Log("GO!!!");
    }
}
