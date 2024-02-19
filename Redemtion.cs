using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P90Ez.ChannelpointPlayer
{
    public class Redemtion : JsonStructure<Redemtion>
    {
        public string Name { get; set; } = string.Empty;
        public bool EnableWhileStreaming { get; set; } = false;
        public bool ForceEnable {  get; set; } = false;
        public bool PlaySound { get; set; } = false;
        public string SoundPath {  get; set; } = String.Empty;
        public int Volume { get; set; } = 50;
    }
}
