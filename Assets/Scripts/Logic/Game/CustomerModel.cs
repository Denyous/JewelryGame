using System;
using System.Collections.Generic;
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
    public const int MAX_MORALE = 100;

    public CustomerMood Mood;
    public CustomerSO Data;
    public List<JewelrySO> CustomerInventory;
    public JewelrySO CurrentJewel;

    public event Action<float> OnCounterOffer;
    public event Action<double> OnCustomerMoraleChanged;
    public event Action<CustomerMood, float, bool> OnCustomerLeave;

    [field: SerializeField] public float Money { get; private set; }
    public float ExpectedItemPrice { get; private set; }
    public float MinRandomMultiplier { get; private set; }
    public float MaxRandomMultiplier { get; private set; }
    public int Morale { get; private set; }
    public bool IsSeller { get; private set; }

    public void Initialize(List<JewelrySO> jewels)
    {
        this.CustomerInventory = jewels;
        Morale = MAX_MORALE;
        MinRandomMultiplier = UnityEngine.Random.Range(0.8f, 1f);
        MaxRandomMultiplier = UnityEngine.Random.Range(1f, 1.2f);
        IsSeller = GetCustomerType();
        CurrentJewel = SelectJewel(IsSeller, CustomerInventory);
        ExpectedItemPrice = CurrentJewel.GetItemWorth();
    }

    private bool GetCustomerType()
    {
        bool playerCanAfford = PlayerCanAfford(CustomerInventory);
        bool isSeller = false;

        if (playerCanAfford)
        {
            isSeller = true;
        }
        else
        {
            isSeller = false;
        }
        return isSeller;
    }

    private bool PlayerCanAfford(List<JewelrySO> jewels)
    {
        bool playerCanAfford = false;
        foreach (var jewel in jewels)
        {
            if (jewel.GetItemWorth() < MoneyManager.Instance.PlayerMoney)
            {
                playerCanAfford = true;
            }
        }
        return playerCanAfford;
    }
    
    private JewelrySO SelectJewel(bool isSeller, List<JewelrySO> jewels)
    {
        if (isSeller)
        {
            return jewels[UnityEngine.Random.Range(0, jewels.Count)];
        }
        else
        {
            jewels = InventoryManager.Instance.GetInventory();
            if (jewels.Count > 0)
            {
                return jewels[UnityEngine.Random.Range(0, jewels.Count)];
            }
        }
        return jewels[UnityEngine.Random.Range(0, jewels.Count)];
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

    public void HandleOffer(float offerValue, float expectedValue, float itemWorth, float minItemWorth, float maxItemWorth)
    {
        float percent = Helpers.GetPercentage(offerValue, expectedValue);

        if (IsSeller)
        {
            if (Mood == CustomerMood.Mad)
            {
                SetMorale(0);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                return;
            }

            if (percent <= 0)
            {
                SetMorale(0);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                return;
            }

            if (percent >= 80 && Data.CustomerCharacter == CustomerCharacter.Generous)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneySpend += offerValue;
                return;
            }

            if (percent >= 85 && Data.CustomerCharacter == CustomerCharacter.Normal)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneySpend += offerValue;
                return;
            }

            if (percent >= 95 && Data.CustomerCharacter == CustomerCharacter.Greedy)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneySpend += offerValue;
                return;
            }

            if (offerValue < minItemWorth)
            {
                UpdateMorale(percent, IsSeller, isOfferBelowMinWorth: true);
                HandleCounterOffer(percent, offerValue, expectedValue, minItemWorth);
                return;
            }

            UpdateMorale(percent, IsSeller);
            HandleCounterOffer(percent, offerValue, expectedValue, minItemWorth);
        }
        else
        {
            if (Mood == CustomerMood.Mad)
            {
                SetMorale(0);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                return;
            }

            if (percent <= 100)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneyEarned += offerValue;
                return;
            }

            if (percent >= 100 && percent <= 105 && Data.CustomerCharacter == CustomerCharacter.Greedy)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneyEarned += offerValue;
                return;
            }

            if (percent >= 100 && percent <= 110 && Data.CustomerCharacter == CustomerCharacter.Normal)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneyEarned += offerValue;
                return;
            }

            if (percent >= 100 && percent <= 115 && Data.CustomerCharacter == CustomerCharacter.Generous)
            {
                SetMorale(100);
                OnCustomerLeave?.Invoke(GetMoodFromMorale(Morale), offerValue, IsSeller);
                DailyReportManager.MoneyEarned += offerValue;
                return;
            }

            if (offerValue > maxItemWorth)
            {
                UpdateMorale(percent, IsSeller, isOfferAboveMaxWorth: true);
                HandleCounterOffer(percent, offerValue, expectedValue, maxItemWorth: maxItemWorth);
                return;
            }

            UpdateMorale(percent, IsSeller);
            HandleCounterOffer(percent, offerValue, expectedValue, maxItemWorth: maxItemWorth);
        }
    }

    private void UpdateMorale(float offerPercent, bool isSeller, bool isOfferBelowMinWorth = false, bool isOfferAboveMaxWorth = false)
    {
        if (isSeller)
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
            return;
        }

        float isOfferAbove = 1f;

        if (isOfferAboveMaxWorth)
        {
            isOfferAbove = 2f;
        }

        float remainingPercentageBuyer = -(100 - (100 - offerPercent));
        float updateMoralePercentageBuyer = remainingPercentageBuyer * (Data.MoraleMultiplier * isOfferAbove);
        Morale = Morale - (int)updateMoralePercentageBuyer;
        Mood = GetMoodFromMorale(Morale);
        OnCustomerMoraleChanged?.Invoke(Helpers.ClampProgressBarValue(Morale));
        return;
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

    private void HandleCounterOffer(float percent, float offerValue, float itemWorth, float minItemWorth = default, float maxItemWorth = default)
    {
        ExpectedItemPrice = GetBargainOffer(percent, offerValue, itemWorth, minItemWorth: minItemWorth, maxItemWorth: maxItemWorth);
        
        OnCounterOffer?.Invoke(ExpectedItemPrice);

        AudioManager.PlayOneShot(SoundType.CounterOffer, 0.4f);
    }

    private float GetBargainOffer(float percent, float offerValue, float totalValue, float minItemWorth = default, float maxItemWorth = default)
    {
        float value = (totalValue * percent) / 100;

        float remainingValue = totalValue - value;

        float newOffer = offerValue + (remainingValue * Data.BargainMultiplier);


        if (minItemWorth != default)
        {
            if (newOffer < minItemWorth * 0.8f * 0.9f)
            {
                newOffer = Mathf.Clamp(newOffer, minItemWorth * MinRandomMultiplier, minItemWorth * MaxRandomMultiplier);
            }
        }

        if (maxItemWorth != default)
        {
            if (newOffer > maxItemWorth * 0.8f * 0.9f)
            {
                newOffer = Mathf.Clamp(newOffer, maxItemWorth * MinRandomMultiplier, maxItemWorth * MaxRandomMultiplier);
            }
        }

        return (int)newOffer;
    }
}
