using KitchenProc.Enums;
using KitchenProc.Interfaces;

namespace KitchenProc.Models
{
    internal class SimpleProcessor : KitchenProcessor
    {
        public SimpleProcessor(string model, BodyMaterial material, IProcessingTool tool)
            : base(model, material, tool) { }

        public override void GetInfo() =>
            Console.WriteLine($"[Простая модель] {Model}, материал корпуса: {Material}");
    }
}
