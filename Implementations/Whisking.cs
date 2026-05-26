using KitchenProc.Interfaces;

namespace KitchenProc.Implementations
{
    
    internal class Whisking : IProcessingTool
    {
        public string GetAction() => "Взбивает ингредиенты до однородной массы";
    }
}
