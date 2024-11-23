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
        public int CurrentLevel = 0;
        public int Money = 0;
        public int Pin = 0;
        
        public bool SoundEnabled = true;
        
        public int CurrentSkin = 1;
        public bool[] AvailableSkins = new bool[24];
        
        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            AvailableSkins[0] = true;
        }
    }
}
