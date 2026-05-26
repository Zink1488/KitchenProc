using KitchenProc.Enums;
using KitchenProc.Implementations;
using KitchenProc.Interfaces;
using KitchenProc.Models;

namespace KitchenProc.Factories
{
  
    internal class UniversalFactory : IKitchenFactory
    {
        public KitchenProcessor CreateProcessor(string model) =>
            new UniversalProcessor(model, BodyMaterial.Aluminum, CreateTool());

        public IProcessingTool CreateTool() => new Grinding();
    }
}
