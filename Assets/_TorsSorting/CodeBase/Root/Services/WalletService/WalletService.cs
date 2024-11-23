using System;

namespace CodeBase.Root.Services
{
    public class WalletService : IWalletService
    {
        public event Action<int> OnMoneyChange;
        public event Action<int> OnPinAdd;

        public int CurrentMoney
        {
            get => _currentMoney;
            private set
            {
              _currentMoney = value;
              OnMoneyChange?.Invoke(value);
            } 
        }
        
        public int CurrentPin
        {
            get => _currentMoney;
            private set
            {
                _currentPin = value;
                OnMoneyChange?.Invoke(value);
            } 
        }

        private readonly ISaveService _saveService;
        private int _currentMoney;
        private int _currentPin;

        public WalletService(ISaveService saveService)
        {
            _saveService = saveService;
            CurrentMoney = _saveService.GetCurrentMoney();
        }

        public void AddMoney(int value)
        {
            if (value <= 0)
                throw new Exception("Value must be greater than 0");
            
            CurrentMoney += value;
            _saveService.SaveCurrentMoney(CurrentMoney);
        }

        public bool TryRemoveMoney(int value)
        {
            if (value <= 0)
                throw new Exception("Value must be greater than 0");

            if (CurrentMoney < value) return false;
            
            CurrentMoney -= value;
            _saveService.SaveCurrentMoney(CurrentMoney);
            return true;
        }

        public void AddPin()
        {
            CurrentPin += 1;
            
        }

        public bool TryRemovePin()
        {
            throw new NotImplementedException();
        }
    }
}