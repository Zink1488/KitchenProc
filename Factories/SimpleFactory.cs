using KitchenProc.Enums;
using KitchenProc.Implementations;
using KitchenProc.Interfaces;
using KitchenProc.Models;

namespace KitchenProc.Factories
{
    
    internal class SimpleFactory : IKitchenFactory
    {
        public KitchenProcessor CreateProcessor(string model) =>
            new SimpleProcessor(model, BodyMaterial.Plastic, CreateTool());

        public IProcessingTool CreateTool() => new Cutting();
    }
}
