using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeInterface;

namespace MazeImplementation1 {
    class Factory1 : MazeInterface.AbstractFactory {

        public Maze createMaze() {  }

        public Wall createWall(WallType type) { }

        public Door createDoor() { }

        public void drawMaze(Maze maze) {


            Form form = new Form();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();
        }
    }
}
