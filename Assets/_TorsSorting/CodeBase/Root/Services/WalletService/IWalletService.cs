using System;

namespace CodeBase.Root.Services
{
    public interface IWalletService : IReadWalletService
    {
        public void AddMoney(int value);

        public bool TryRemoveMoney(int value);

        public void AddPin();

        public bool TryRemovePin();
    }

    public interface IReadWalletService
    {
        event Action<int> OnMoneyChange;
        
        event Action<int> OnPinAdd;
        
        int CurrentMoney { get; }
        
        int CurrentPin { get; }
    }
}