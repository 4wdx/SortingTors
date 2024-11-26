namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        // Ваши поля
        public int CurrentLevel = 1;
        public int Money;
        public int Pin;
        
        public bool SoundEnabled = true;
        
        public int CurrentSkin;
        public bool[] AvailableSkins = new bool[24];
        public int CommonSkins = 1;
        public int RareSkins;
        public int LegendSkins;
        
        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            AvailableSkins[0] = true;
        }
    }
}
