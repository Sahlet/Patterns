using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeInterface {
    public class DoorData {
        public bool locked;
        public DoorData(bool locked = false) { this.locked = locked; }
    }

    interface Door {
        bool Locked {
            get;
            set;
        }
    }
}
