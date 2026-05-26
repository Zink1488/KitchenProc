using KitchenProc.Interfaces;
using KitchenProc.Models;

namespace KitchenProc.Factories
{
    internal interface IKitchenFactory
    {
        KitchenProcessor CreateProcessor(string model);
        IProcessingTool CreateTool();
    }
}
