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
        public bool SoundEnabled = true;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            
        }
    }
}
