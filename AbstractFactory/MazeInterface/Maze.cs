using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeInterface {

    public class MazeData {
        public WallData[,] walls;
        public MazeData(WallData[,] walls = null ) { this.walls = walls; }
    }

    interface Maze {
        Size MazeSize {
            get;
            set;
        }

        Wall this[int row, int column] {
            get;
            set;
        }
    }
}
