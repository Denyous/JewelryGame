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
    private float _reputationProgressValue { get; set; }

    [SerializeField] private ProgressBar _progressBar;

    private void Awake()
    {
        _reputationPoints = 0;
        _reputationProgressValue = 0f;

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
            _reputationProgressValue = Helpers.ClampProgressBarValue(_reputationPoints);
            _progressBar.progressValue = _reputationProgressValue;
        }

        if (_reputationPoints > 100 && _reputationPoints <= 500)
        {
            _currentReputation = Reputation.Foreman;
            _reputationProgressValue = Helpers.ClampProgressBarValue(_reputationPoints, 500);
            _progressBar.progressValue = _reputationProgressValue;
        }

        if (_reputationPoints > 500 && _reputationPoints <= 1500)
        {
            _currentReputation = Reputation.Expert;
            _reputationProgressValue = Helpers.ClampProgressBarValue(_reputationPoints, 1500);
            _progressBar.progressValue = _reputationProgressValue;
        }

        if (_reputationPoints > 1500 && _reputationPoints <= 3000)
        {
            _currentReputation = Reputation.Master;
            _reputationProgressValue = Helpers.ClampProgressBarValue(_reputationPoints, 3000);
            _progressBar.progressValue = _reputationProgressValue;
        }
    }

    public void AddReputation(int value)
    {
        _reputationPoints += value;
        UpdateReputation();
    }

    public void RemoveReputation(int value)
    {
        _reputationPoints -= value;
        UpdateReputation();
    }

    public Reputation GetCurrentReputation()
    {
        return _currentReputation;
    }
}
