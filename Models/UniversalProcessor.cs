using KitchenProc.Enums;
using KitchenProc.Interfaces;

namespace KitchenProc.Models
{
    
    internal class UniversalProcessor : KitchenProcessor, IBlender, IMixer, IDoughMixer
    {
        public UniversalProcessor(string model, BodyMaterial material, IProcessingTool tool)
            : base(model, material, tool) { }

        public override void GetInfo() =>
            Console.WriteLine($"[Универсал] {Model}, материал: {Material} — выполняет любые задачи");

        public void Blend() =>
            Console.WriteLine($"[{Model}] блендирует суп до однородной консистенции");

        public void Mix() =>
            Console.WriteLine($"[{Model}] миксирует яйца с сахаром");

        public void KneadDough() =>
            Console.WriteLine($"[{Model}] замешивает тесто в течение 10 минут");
    }
}
