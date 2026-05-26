using KitchenProc.Enums;
using KitchenProc.Interfaces;

namespace KitchenProc.Models
{
   
    public abstract class KitchenProcessor(string model, BodyMaterial material, IProcessingTool tool)
    {
        public string Model { get; } = model;
        public BodyMaterial Material { get; } = material;

        protected IProcessingTool _tool = tool;

        public abstract void GetInfo();

        
        public void Process() =>
            Console.WriteLine($"[{Model}] выполняет обработку: {_tool.GetAction()}");
    }
}
