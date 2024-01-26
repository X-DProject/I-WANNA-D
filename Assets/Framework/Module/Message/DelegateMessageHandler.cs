namespace Tool.Module.Message
{
    /// <summary>
    /// Delegate for message callbacks and handlers
    /// </summary>
    /// <param name="rMessage">Message that is to be handled</param>
    public delegate void MessageHandler(IMessage rMessage);
}