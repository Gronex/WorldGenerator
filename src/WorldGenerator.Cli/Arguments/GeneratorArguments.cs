using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Cli.Arguments
{
    public class GeneratorArguments
    {
        public int Points { get; set; }

        public int Relax { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int? Seed { get; set; }

        public override string ToString()
        {
            var props = this.GetType().GetProperties();
            var builder = new StringBuilder();

            foreach (var prop in props)
            {
                builder.AppendLine($"{prop.Name + ":",-20} {prop.GetValue(this)}");
            }
            return builder.ToString();
        }
    }
}
