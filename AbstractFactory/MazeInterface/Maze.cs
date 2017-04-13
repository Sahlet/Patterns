using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeInterface {

    public class MazeData {
        public WallData[,] walls;
        public MazeData(WallData[,] walls = null ) { this.walls = walls; }
    }

    interface Maze {
        System.Drawing.Size Size {
            get;
            set;
        }

        Wall this[int row, int column] {
            get;
            set;
        }
    }
}
