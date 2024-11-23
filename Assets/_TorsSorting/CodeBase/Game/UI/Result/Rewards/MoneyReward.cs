using CodeBase.Root.Services;

namespace CodeBase.Game.UI.Result
{
    public class MoneyReward : IReward
    {
        private readonly IWalletService _walletService;
        private readonly int _value;

        public MoneyReward(IWalletService walletService, int value)
        {
            _walletService = walletService;
            _value = value;
        }

        public void AddReward(GiftButton giftButton)
        {
            _walletService.AddMoney(_value);
            giftButton.RewardMoney(_value);
        }
    }
}