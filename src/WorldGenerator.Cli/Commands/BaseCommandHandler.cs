using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Cli
{
    public abstract class BaseCommandHandler<TArgs>
    {
        public abstract Task<int> ExecuteCommand(TArgs args);
    }
}
