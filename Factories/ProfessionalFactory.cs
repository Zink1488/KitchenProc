using KitchenProc.Enums;
using KitchenProc.Implementations;
using KitchenProc.Interfaces;
using KitchenProc.Models;

namespace KitchenProc.Factories
{
   
    internal class ProfessionalFactory : IKitchenFactory
    {
        public KitchenProcessor CreateProcessor(string model) =>
            new ProfessionalProcessor(model, BodyMaterial.StainlessSteel, CreateTool());

        public IProcessingTool CreateTool() => new Whisking();
    }
}
