using KitchenProc.Enums;
using KitchenProc.Interfaces;

namespace KitchenProc.Models
{
    internal class ProfessionalProcessor : KitchenProcessor, IBlender, IMixer
    {
        public ProfessionalProcessor(string model, BodyMaterial material, IProcessingTool tool)
            : base(model, material, tool) { }

        public override void GetInfo() =>
            Console.WriteLine($"[Про серия] {Model}, премиум материал: {Material}");

        public void Blend() =>
            Console.WriteLine($"[{Model}] блендирует ингредиенты в смузи");

        public void Mix() =>
            Console.WriteLine($"[{Model}] миксирует крем до пышной массы");
    }
}
