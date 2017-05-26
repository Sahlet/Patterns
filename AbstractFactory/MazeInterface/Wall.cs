using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeInterface {
    public enum WallType { Default, Type2, Type3 }

    public class WallData {
        public bool vertical;
        public DoorData doorData;
        public WallType type;
        public WallData() { }
        public WallData(bool vertical = false, DoorData doorData = null, WallType type = WallType.Default) {
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

    interface WallDecorator : Wall { }
    interface WallSolidBorderDecorator : WallDecorator {
        Color BorderColor
        {
            get;
            set;
        }
    }
    interface WallDashedBorderDecorator : WallDecorator {
        Color DashColor
        {
            get;
            set;
        }
    }
    interface WallRotOnClickDecorator : WallDecorator { }
}
