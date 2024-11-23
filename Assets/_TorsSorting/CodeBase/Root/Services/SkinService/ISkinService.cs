using CodeBase.Configs;

namespace CodeBase.Root.Services
{
    public interface ISkinService
    {
        public SkinData CurrentSkin { get; }
        public void SetSkin(int id);
    }
}