using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeInterface;
using System.Drawing;

namespace MazeImplementation1
{
    class Factory1 : MazeInterface.AbstractFactory {

        class Door1 : Control, Door {
            private bool locked;
            public bool Locked {
                get { return locked; }
                set {
                    if (locked == value) return;
                    locked = value;
                    state_changed();
                }
            }

            private bool vertical;
            public bool Vertical {
                get { return vertical; }
                set {
                    if (vertical == value) return;
                    vertical = value;
                    state_changed();
                }
            }

            private Control p1 = new Control(), p2 = new Control();

            private void on_click(Object Sender, EventArgs e) {
                Locked = !Locked;
            }

            public Door1() {
                this.Controls.Add(p1);
                this.Controls.Add(p2);

                var handler = new EventHandler(on_click);
                this.Click += handler;
                p1.Click += handler;
                p2.Click += handler;

                this.BackColor = Color.Transparent;

                state_changed();
            }

            private void state_changed() {
                p1.BackColor = p2.BackColor = locked ? Color.Red : Color.Blue;
                if (locked) {
                    p1.Dock = DockStyle.Fill;
                    p2.Dock = DockStyle.None;
                    p2.Size = new Size(0, 0);
                } else {
                    p1.Dock = vertical ? DockStyle.Top : DockStyle.Left;
                    p2.Dock = vertical ? DockStyle.Bottom : DockStyle.Right;
                    p1.Size = p2.Size = new Size(vertical ? 0 : 5, vertical ? 5 : 0);
                }
            }
        }

        private class Wall1 : Control, Wall {
            public static Size getWallSize() { return new Size(10, 60); }

            private bool vertical;
            public bool Vertical {
                get { return vertical; }
                set {
                    if (vertical == value) return;
                    vertical = value;
                    if (door != null) door.Vertical = vertical;
                    state_changed();
                }
            }

            private Door1 door;
            public MazeInterface.Door Door {
                get { return door; }
                set {
                    if (door == value) return;

                    if (value == null) {
                        this.Controls.Remove(door);
                        door = null;
                        return;
                    }

                    {
                        Door1 new_door = (value as Door1);
                        if (new_door == null) throw new Exception("value is not Door1 entity");

                        this.Controls.Remove(door);

                        door = new_door;

                        this.Controls.Add(door);
                    }

                    door.Size = new Size(20, 20);
                    door.Margin = new Padding((this.Size.Width - door.Size.Width) / 2, (this.Size.Height - door.Size.Height) / 2, 0, 0);

                    state_changed();
                }
            }

            protected WallType get_type() { return WallType.Default; }
            public WallType Type {
                get { return get_type(); }
            }

            protected Control p1 = new Control(), p2 = new Control();

            public Wall1() {
                this.Size = new Size(getWallSize().Height, getWallSize().Height);

                this.Controls.Add(p1);
                this.Controls.Add(p2);

                this.BackColor = Color.Transparent;

                //p1.BackColor = p2.BackColor = Color.;

                state_changed();
            }

            private void state_changed() {
                var wallSize = getWallSize();
                wallSize = new Size(wallSize.Width, (door == null) ? wallSize.Height : ((wallSize.Height - door.Height) / 2));

                p1.MaximumSize = p1.Size = vertical ? wallSize : new Size(wallSize.Height, wallSize.Width);

                if (door == null) {
                    p1.Dock = DockStyle.Fill;
                    p2.Dock = DockStyle.None;
                    p2.MaximumSize = p2.Size = new Size(0, 0);
                } else {
                    p1.Dock = vertical ? DockStyle.Top : DockStyle.Left;
                    p2.Dock = vertical ? DockStyle.Bottom : DockStyle.Right;
                    p2.MaximumSize = p2.Size = p1.Size;
                }
            }
        }
        private class Wall1T2 : Wall1 {
            static Color[] colors = { Color.Azure, Color.Blue, Color.Chocolate, Color.Green, Color.Gray, Color.Gold };
            static Random rnd = new Random();

            private void on_click(Object Sender, EventArgs e) {
                p1.BackColor = p2.BackColor = colors[rnd.Next() % colors.Length];
            }

            public Wall1T2() {
                p1.BackColor = p2.BackColor = Color.Aqua;
                p1.Click += on_click;
                p2.Click += on_click;
            }
        }

        private class Wall1T3 : Wall1 {

        }

        private class Maze1 : Control, Maze {
            private System.Drawing.Size mazeSize;
            private Wall1[,] walls;

            private void clear() {
                walls = new Wall1[0, 0];
                mazeSize = new Size(0, 0);
                this.Controls.Clear();
            }

            public static Padding get_margin(int row, int column) {
                Padding margin = new Padding();
                int row_rest_by_2 = (row % 2);
                int leftShift = ((1 - row_rest_by_2) * Wall1.getWallSize().Height / 2);
                int topShift = -row_rest_by_2 * (Wall1.getWallSize().Height + Wall1.getWallSize().Width) / 2;
                margin.Left = column * Wall1.getWallSize().Height + leftShift;
                margin.Top = row * (Wall1.getWallSize().Height + Wall1.getWallSize().Width) + topShift;
                return margin;
            }

            public Maze1() {
                this.AutoSize = true;
                this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
                //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                //SetStyle(ControlStyles.Opaque, false);
                this.BackColor = Color.Transparent;
                clear();
            }

            public Size MazeSize {
                get { return mazeSize; }
                set {
                    if (mazeSize == value) return;
                    mazeSize = value;
                    clear();
                }
            }

            public Wall this[int row, int column] {
                get { return walls[row, column]; }
                set {
                    Wall1 old_wall = walls[row, column];

                    if (old_wall == value) return;

                    if (value == null) {
                        this.Controls.Remove(old_wall);
                        walls[row, column] = null;
                        return;
                    }

                    Wall1 wall = (value as Wall1);
                    if (wall == null) throw new Exception("value is not Wall1 entity");

                    walls[row, column] = wall;
                    wall.Margin = get_margin(row, column);

                    this.Controls.Remove(old_wall);
                    this.Controls.Add(wall);
                }
            }
        }

        public override Maze createMaze() { return new Maze1(); }

        public override Wall createWall(WallType type) {
            if (type == WallType.Type2) return new Wall1T2();
            //if (type == WallType.Type3) return new Wall1T3();
            return new Wall1();
        }

        public override Door createDoor() {
            return new Door1();
        }

        public override void drawMaze(Maze maze) {
            if (maze == null) throw new ArgumentNullException();
            Maze1 my_maze = (maze as Maze1);
            if (my_maze == null) throw new Exception("value is not Wall1 entity");

            my_maze.Dock = DockStyle.Fill;

            Form form = new Form();
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.Controls.Add(my_maze);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form);
        }
    }
}
