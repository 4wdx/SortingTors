using System;

namespace CodeBase.Root.Services
{
    public class WalletService : IWalletService
    {
        public event Action<int> OnMoneyChange;
        public event Action<int> OnPinChange;

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
            get => _currentPin;
            private set
            {
                _currentPin = value;
                OnPinChange?.Invoke(value);
            } 
        }

        private readonly ISaveService _saveService;
        private int _currentMoney;
        private int _currentPin;

        public WalletService(ISaveService saveService)
        {
            _saveService = saveService;
            CurrentMoney = _saveService.GetCurrentMoney();
            CurrentPin = _saveService.GetCurrentPin();
        }

        public void AddMoney(int value)
        {
            if (value <= 0)
                throw new Exception("Value must be greater than 0");
            
            CurrentMoney += value;
            _saveService.SaveCurrentMoney(CurrentMoney);
        }

        public void TryRemoveMoney(int value)
        {
            if (value <= 0)
                throw new Exception("Value must be greater than 0");

            if (CurrentMoney < value) 
                throw new Exception("Cannot remove more money than current money");
            
            CurrentMoney -= value;
            _saveService.SaveCurrentMoney(CurrentMoney);
        }

        public void AddPin()
        {
            CurrentPin += 1;
            _saveService.SaveCurrentPin(CurrentPin);
        }

        public void RemovePin()
        {
            CurrentPin -= 1;
            _saveService.SaveCurrentPin(CurrentPin);
        }
    }
}