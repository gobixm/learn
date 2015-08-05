namespace Infotecs.Attika.AttikaDomain.Services.Queuing
{
    public interface IQueueProcessor
    {
    }

    public interface IQueueProcessor<in T> : IQueueProcessor where T : class
    {
        void Process(T message);
    }
}