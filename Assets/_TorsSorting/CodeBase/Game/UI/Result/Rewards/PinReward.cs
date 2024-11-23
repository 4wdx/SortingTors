using CodeBase.Root.Services;

namespace CodeBase.Game.UI.Result
{
    public class PinReward : IReward
    {
        private readonly IWalletService _walletService;

        public PinReward(IWalletService walletService)
        {
            _walletService = walletService;
        }
        
        public void AddReward(GiftButton giftButton)
        {
            _walletService.AddPin();
            giftButton.RewardPin();
        }
    }
}