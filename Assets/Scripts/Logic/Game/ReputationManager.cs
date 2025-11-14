using UnityEngine;


public enum Reputation
{
    Apprentice,
    Foreman,
    Expert,
    Master
}

public class ReputationManager : MonoBehaviour
{
    public static ReputationManager Instance;
    private Reputation _currentReputation;
    public int _reputationPoints { get; private set; }


    private void Awake()
    {
        _reputationPoints = 0;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateReputation()
    {
        if (_reputationPoints <= 100)
        {
            _currentReputation = Reputation.Apprentice;
        }

        if (_reputationPoints > 100 && _reputationPoints <= 500)
        {
            _currentReputation = Reputation.Foreman;
        }

        if (_reputationPoints > 500 && _reputationPoints <= 1500)
        {
            _currentReputation = Reputation.Expert;
        }

        if (_reputationPoints > 1500 && _reputationPoints <= 3000)
        {
            _currentReputation = Reputation.Master;
        }
    }

    public void AddReputation(int value)
    {
        _reputationPoints += value;
        UpdateReputation();
    }

    public void SubtractReputation(int value)
    {
        _reputationPoints -= value;
        UpdateReputation();
    }

    public Reputation GetCurrentReputation()
    {
        return _currentReputation;
    }
}
