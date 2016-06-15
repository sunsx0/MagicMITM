namespace MagicMITM.Net.Proxy.Socks4
{
    /// <summary>
    /// Коды команд/ответов Socks4 прокси сервера.
    /// </summary>
    enum Socks4ServiceCode : byte
    {
        /// Команды:

        /// <summary>
        /// Запрос на подключение.
        /// </summary>
        Connect = 0x01,

        /// <summary>
        /// Запрос на бинд порта.
        /// </summary>
        Bind = 0x02,

        /// Ответы:

        /// <summary>
        /// Запрос предоставлен.
        /// </summary>
        Accepted = 0x5A,

        /// <summary>
        /// Запрос отклонен или ошибочен.
        /// </summary>
        Rejected = 0x5B,

        /// <summary>
        /// Запрос не удался, потому что не запущен identd (или не доступен с сервера).
        /// </summary>
        IdentServiceFailed = 0x5C,

        /// <summary>
        /// Запрос не удался, поскольку клиентский identd не может подтвердить идентификатор пользователя в запросе.
        /// </summary>
        IdentificationFailed = 0x5D
    }
}
