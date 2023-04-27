using Stargazer.Communication.Incoming.Handshake;
using Stargazer.Communication.Interfaces;
using Stargazer.Utils.Collections;

namespace Stargazer.Communication;

public class MessageHandlerManager
{
    private static MessageHandlerManager? _instance;
    public static MessageHandlerManager Instance => _instance ??= new MessageHandlerManager();
    
    private readonly StarDictionary<int, IMessageHandler> _messageHandlers;

    public MessageHandlerManager()
    {
        _messageHandlers = new StarDictionary<int, IMessageHandler>();
    }

    public void RegisterHandlers()
    {
        _messageHandlers[206] = new InitCryptoMessageEvent();
        _messageHandlers[2002] = new GenerateSecretKeyMessageEvent();
        _messageHandlers[1170] = new VersionCheckMessageEvent();
        _messageHandlers[813] = new UniqueIDMessageEvent();
    }

    public bool TryGetHandler(int header, out IMessageHandler? messageHandler)
    {
        return _messageHandlers.TryGetValue(header, out messageHandler);
    }
}