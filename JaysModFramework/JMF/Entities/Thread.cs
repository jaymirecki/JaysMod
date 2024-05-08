using Fiber = Rage.GameFiber;

namespace JMF
{
    public static class Thread
    {
        public static void Yield()
        {
            Fiber.Yield();
        }
        public static void Sleep(int duration)
        {
            Fiber.Sleep(duration);
        }
        public static void Wait(int duration)
        {
            Fiber.Wait(duration);
        }
    }
}
