using Exiled.API.Interfaces;

namespace JokeSystem
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public int GiveChancePerJoke { get; set; } = 5;

        public string[] Audios { get; set; } =
        {
            "\\laugh1.ogg",
            "\\laugh2.ogg",
            "\\laugh3.ogg",
        };

        public string[] Jokes { get; set; } =
{
            "Почему черепашки ниндзя нападают вчетвером? - У них учитель крыса",
            "Что сказал слепой, войдя в бар? - Всем привет, кого не видел!",
            "Почему в Африке так много болезней? - Потому что таблетки нужно запивать водой.",
            "Как называют человека, который продал свою печень? - Обеспеченный.",
        };
    }
}