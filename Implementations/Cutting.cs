using KitchenProc.Interfaces;

namespace KitchenProc.Implementations
{
   
    internal class Cutting : IProcessingTool
    {
        public string GetAction() => "Шинкует овощи на мелкие кусочки";
    }
}
