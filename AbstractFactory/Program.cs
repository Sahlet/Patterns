﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazeInterface;
using MazeImplementation1;
using System.Windows.Forms;

namespace AbstractFactoryLab {

    static class Program {
        static WallData horwallnodoor1 = new WallData(false);                                       // horizontal wall with no door type1
        static WallData horwallwopend1 = new WallData(false, new DoorData());                       // horizontal wall with open door type1
        static WallData verwallnodoor1 = new WallData(true);                                        // vertical wall with no door type1
        static WallData verwallwopend1 = new WallData(true, new DoorData());                        // vertical wall with open door type1
        static WallData horwallwopend2 = new WallData(false, new DoorData(), WallType.Type2);       // horizontal wall with open door type
        static WallData verwallwopend3 = new WallData(true, new DoorData(), WallType.Type3);        // vertical wall with open door type1
        static WallData _____null_____ = null;

        static MazeData getMazeData1() {
            return new MazeData(new WallData[,] {
                { horwallwopend2, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallwopend1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallwopend1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallwopend3 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallwopend1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallwopend1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallwopend1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallwopend1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallwopend1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallwopend1, horwallwopend1, horwallwopend1, horwallnodoor1, horwallwopend1, horwallnodoor1, _____null_____ },
                { verwallnodoor1, verwallwopend1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1, verwallnodoor1, verwallnodoor1, verwallwopend1, verwallwopend1, verwallnodoor1 },
                { horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, horwallnodoor1, _____null_____ }
            });
        }

        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MazeInterface.AbstractFactory factory = new Factory1();

            MazeData[] Mazes = { getMazeData1() };

            factory.drawMaze(factory.createMaze(Mazes[0]));
        }
    }
}
