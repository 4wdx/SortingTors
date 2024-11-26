using System;

namespace CodeBase.Root.Services
{
    public interface IWalletService : IReadWalletService
    {
        public void AddMoney(int value);

        public void TryRemoveMoney(int value);

        public void AddPin();

        public void RemovePin();
    }

    public interface IReadWalletService
    {
        event Action<int> OnMoneyChange;
        
        event Action<int> OnPinChange;
        
        int CurrentMoney { get; }
        
        int CurrentPin { get; }
    }
}