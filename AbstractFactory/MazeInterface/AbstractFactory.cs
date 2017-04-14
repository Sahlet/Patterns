using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeInterface {

    abstract class AbstractFactory {
        public abstract Maze createMaze();
        public abstract Wall createWall(WallType type = WallType.Default);
        public abstract Door createDoor();

        public abstract void drawMaze(Maze maze);

        public Maze createMaze(MazeData data) {
            if (data == null || data.walls == null) return null;
            var maze = createMaze();

            maze.MazeSize = new System.Drawing.Size(data.walls.GetLength(0), data.walls.GetLength(1));

            for (int i = 0; i < data.walls.GetLength(0); i++) {
                for (int j = 0; j < data.walls.GetLength(1); j++) {
                    maze[i, j] = createWall(data.walls[i, j]);
                }
            }

            return maze;
        }
        public Wall createWall(WallData data) {
            if (data == null) return null;
            var wall = createWall(data.type);
            wall.Vertical = data.vertical;
            wall.Door = createDoor(data.doorData);
            return wall;
        }
        public Door createDoor(DoorData data) {
            if (data == null) return null;
            var door = createDoor();
            door.Locked = data.locked;
            return door;
        }
    }
}
