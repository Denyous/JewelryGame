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
    public event Action<int> OnCustomerMoraleChanged;
    public event Action<CustomerMood, float> OnCustomerLeave;

    public float ExpectedItemPrice { get; private set; }

    public void Initialize(JewelrySO jewelData)
    {
        CurrentJewelData = jewelData;
        ExpectedItemPrice = CurrentJewelData.GetItemWorth();
        Morale = 100;
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

    public void HandleOffer(float offerValue, float expectedValue, float itemWorth)
    {
        float percent = Helpers.GetPercentage(offerValue, expectedValue);

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
        HandleCounterOffer(percent, offerValue, expectedValue);
    }

    private void PlaceHolder(float percent, float offerValue, float expectedValue)
    {
        if (percent <= 25)
        {
            SetMorale(0);
            OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
            return;
        }

        if (percent > 25 && percent < 50)
        {
            if (Data.CustomerCharacter == CustomerCharacter.Greedy || Data.CustomerCharacter == CustomerCharacter.Normal)
            {
                SetMorale(0);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
                return;
            }

            UpdateMorale(percent);
            HandleCounterOffer(percent, offerValue, expectedValue);
            return;
        }

        if (percent > 50 && percent < 95)
        {
            if (Data.CustomerCharacter == CustomerCharacter.Greedy)
            {
                SetMorale(0);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue);
                return;
            }
            UpdateMorale(percent);
            HandleCounterOffer(percent, offerValue, expectedValue);
            return;
        }
    }

    private void UpdateMorale(float offerPercent)
    {
        float remainingPercentage = 100 - offerPercent;
        float updateMoralePercentage = remainingPercentage * Data.GetMoraleMultiplier();

        Morale = Morale - (int)updateMoralePercentage;
        OnCustomerMoraleChanged?.Invoke(Morale);
    }

    private void SetMorale(int value)
    {
        Morale = value;
        OnCustomerMoraleChanged?.Invoke(Morale);
    }

    private CustomerMood GetMoodFromMorale(int morale)
    {
        if (morale > 80)
        {
            return CustomerMood.Happy;
        }

        if (morale < 80 && morale > 50)
        {
            return CustomerMood.Cheerful;
        }

        if (morale < 50 && morale > 40)
        {
            return CustomerMood.Normal;
        }

        if (morale < 40 && morale > 30)
        {
            return CustomerMood.Upset;
        }

        if (morale < 30 && morale > 20)
        {
            return CustomerMood.Angry;
        }

        if (morale < 20)
        {
            return CustomerMood.Mad;
        }

        throw new Exception();
    }

    private void HandleCounterOffer(float percent, float offerValue, float itemWorth)
    {
        ExpectedItemPrice = GetBargainOffer(percent, offerValue, itemWorth);
        
        OnCounterOffer?.Invoke(ExpectedItemPrice);
    }

    private float GetBargainOffer(float percent, float offerValue, float totalValue)
    {
        float value = (totalValue * percent) / 100;

        float remainingValue = totalValue - value;

        float newOffer = offerValue + (remainingValue * Data.GetBargainMultiplier());

        return (int)newOffer;
    }
}
