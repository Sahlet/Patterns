using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeInterface {
    public enum WallType { type1, type2, type3 }

    public class WallData {
        public bool vertical;
        public DoorData doorData;
        public WallType type;
        public WallData() { }
        public WallData(bool vertical, DoorData doorData = null, WallType type = WallType.type1) {
            this.vertical = vertical;
            this.doorData = doorData;
            this.type = type;
        }
    }

    interface Wall {
        bool Vertical {
            get;
            set;
        }

        MazeInterface.Door Door {
            get;
            set;
        }

        WallType Type {
            get;
        }

    }
}
