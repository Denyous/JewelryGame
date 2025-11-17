using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEditor.Timeline;
using UnityEngine;

public enum CustomerMood
{
    Mad = 20,
    Angry = 30,
    Upset = 40,
    Normal = 50,
    Cheerful = 80,
    Happy = 100,
}

[System.Serializable]
public class CustomerModel
{
    public CustomerMood Mood;
    public CustomerSO Data;
    public JewelrySO CurrentJewelData;
    [field: SerializeField] public float Money { get; private set; }

    public int Morale { get; private set; }

    public const int MAX_MORALE = 100; 

    public event Action<float> OnCounterOffer;
    public event Action<double> OnCustomerMoraleChanged;
    public event Action<CustomerMood, float> OnCustomerLeave;

    public float ExpectedItemPrice { get; private set; }
    public float minRandomMultiplier { get; private set; }
    public float maxRandomMultiplier { get; private set; }

    public void Initialize(JewelrySO jewelData)
    {
        CurrentJewelData = jewelData;
        ExpectedItemPrice = CurrentJewelData.GetItemWorth();
        Morale = MAX_MORALE;
        minRandomMultiplier = UnityEngine.Random.Range(0.8f, 1f);
        maxRandomMultiplier = UnityEngine.Random.Range(1f, 1.2f);
    }

    public void Reset()
    {
        Morale = MAX_MORALE;
        Mood = CustomerMood.Normal;
    }

    public void AddMoney(float amount)
    {
        Money += amount;
    }

    public void RemoveMoney(float amount)
    {
        if (Money > 0)
        {
            Money -= amount;
        }
        else
        {
            Money = 0;
        }
    }

    public void HandleOffer(float offerValue, float expectedValue, float itemWorth, float minItemWorth)
    {
        float percent = Helpers.GetPercentage(offerValue, expectedValue);

        if (Mood == CustomerMood.Mad)
        {
            SetMorale(0);
            OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
            return;
        }

        if (offerValue < minItemWorth)
        {
            UpdateMorale(percent, isOfferBelowMinWorth: true);
            HandleCounterOffer(percent, offerValue, expectedValue, minItemWorth);
            return;
        }

        if (percent <= 0)
        {
            SetMorale(0);
            OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
            return;
        }

        if (percent >= 80 && Data.CustomerCharacter == CustomerCharacter.Generous)
        {
            SetMorale(100);
            OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
            return;
        }

        if (percent >= 85 && Data.CustomerCharacter == CustomerCharacter.Normal)
        {
            SetMorale(100);
            OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
            return;
        }

        if (percent >= 95 && Data.CustomerCharacter == CustomerCharacter.Greedy)
        {
            SetMorale(100);
            OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
            return;
        }

        UpdateMorale(percent);
        HandleCounterOffer(percent, offerValue, expectedValue, minItemWorth);
    }

    private void UpdateMorale(float offerPercent, bool isOfferBelowMinWorth = false)
    {
        float isOfferBelow = 1f;

        if (isOfferBelowMinWorth)
        {
            isOfferBelow = 2f;
        }

        float remainingPercentage = 100 - offerPercent;
        float updateMoralePercentage = remainingPercentage * (Data.MoraleMultiplier * isOfferBelow);

        Morale = Morale - (int)updateMoralePercentage;
        Mood = GetMoodFromMorale(Morale);
        OnCustomerMoraleChanged?.Invoke(Helpers.ClampProgressBarValue(Morale));
    }

    private void SetMorale(int value)
    {
        Morale = value;
        Mood = GetMoodFromMorale(Morale);
        OnCustomerMoraleChanged?.Invoke(Helpers.ClampProgressBarValue(Morale));
    }

    private CustomerMood GetMoodFromMorale(int morale)
    {
        if (morale >= 80)
        {
            return CustomerMood.Happy;
        }

        if (morale < 80 && morale >= 50)
        {
            return CustomerMood.Cheerful;
        }

        if (morale < 50 && morale >= 40)
        {
            return CustomerMood.Normal;
        }

        if (morale < 40 && morale >= 30)
        {
            return CustomerMood.Upset;
        }

        if (morale < 30 && morale >= 20)
        {
            return CustomerMood.Angry;
        }

        if (morale < 20)
        {
            return CustomerMood.Mad;
        }

        throw new Exception();
    }

    private void HandleCounterOffer(float percent, float offerValue, float itemWorth, float minItemWorth)
    {
        ExpectedItemPrice = GetBargainOffer(percent, offerValue, itemWorth, minItemWorth);
        
        OnCounterOffer?.Invoke(ExpectedItemPrice);
    }

    private float GetBargainOffer(float percent, float offerValue, float totalValue, float minItemWorth)
    {
        float value = (totalValue * percent) / 100;

        float remainingValue = totalValue - value;

        float newOffer = offerValue + (remainingValue * Data.BargainMultiplier);

        if (newOffer < minItemWorth * 0.8f * 0.9f)
        {
            newOffer = Mathf.Clamp(newOffer, minItemWorth * minRandomMultiplier, minItemWorth * maxRandomMultiplier);
        }

        return (int)newOffer;
    }
}
