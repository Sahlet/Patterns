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

        public class TranspCtrl : Control {
            public bool drag = false;
            public bool enab = false;
            private int m_opacity = 100;

            private int alpha;
            public TranspCtrl()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                SetStyle(ControlStyles.Opaque, true);
                this.BackColor = Color.Transparent;
            }

            public int Opacity
            {
                get
                {
                    if (m_opacity > 100)
                    {
                        m_opacity = 100;
                    }
                    else if (m_opacity < 1)
                    {
                        m_opacity = 1;
                    }
                    return this.m_opacity;
                }
                set
                {
                    this.m_opacity = value;
                    if (this.Parent != null)
                    {
                        Parent.Invalidate(this.Bounds, true);
                    }
                }
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle = cp.ExStyle | 0x20;
                    return cp;
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

                Color frmColor = this.Parent.BackColor;
                Brush bckColor = default(Brush);

                alpha = (m_opacity * 255) / 100;

                if (drag)
                {
                    Color dragBckColor = default(Color);

                    if (BackColor != Color.Transparent)
                    {
                        int Rb = BackColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                        int Gb = BackColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                        int Bb = BackColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                        dragBckColor = Color.FromArgb(Rb, Gb, Bb);
                    }
                    else
                    {
                        dragBckColor = frmColor;
                    }

                    alpha = 255;
                    bckColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));
                }
                else
                {
                    bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));
                }

                if (this.BackColor != Color.Transparent | drag)
                {
                    g.FillRectangle(bckColor, bounds);
                }

                bckColor.Dispose();
                g.Dispose();
                base.OnPaint(e);
            }

            protected override void OnBackColorChanged(EventArgs e)
            {
                if (this.Parent != null)
                {
                    Parent.Invalidate(this.Bounds, true);
                }
                base.OnBackColorChanged(e);
            }

            protected override void OnParentBackColorChanged(EventArgs e)
            {
                this.Invalidate();
                base.OnParentBackColorChanged(e);
            }
        }

        public class ControlWithBorder : Control {
            public ControlWithBorder() {
                SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            }

            public Pen BorderPen = Pens.Black;

            protected override void OnPaint(PaintEventArgs e) {
                using (SolidBrush brush = new SolidBrush(BackColor)) e.Graphics.FillRectangle(brush, ClientRectangle);
                e.Graphics.DrawRectangle(BorderPen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }

        }

        private class Door1 : Control, Door {
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
                this.Padding = new Padding(0);

                this.Controls.Add(p1);
                this.Controls.Add(p2);

                this.Click += on_click;
                p1.Click += on_click;
                p2.Click += on_click;

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
                    Size pSize = new Size(0, 5);
                    p1.Size = p2.Size = vertical ? pSize : new Size(pSize.Height, pSize.Width);
                }
            }
        }

        private class Wall1 : TranspCtrl, Wall {
            public static Size getWallSize() { return new Size(10, 44); }
            public static Size getDoorSize() { return new Size(12, 16); }

            private bool vertical;
            public virtual bool Vertical {
                get { return vertical; }
                set {
                    if (vertical == value) return;
                    vertical = value;
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
                    }

                    this.Controls.Add(door);
                    door.BringToFront();

                    state_changed();
                }
            }

            protected WallType get_type() { return WallType.Default; }
            public WallType Type {
                get { return get_type(); }
            }

            protected ControlWithBorder p1 = new ControlWithBorder(), p2 = new ControlWithBorder();

            public Wall1() {
                this.Size = new Size(getWallSize().Height, getWallSize().Height);

                this.Padding = new Padding(0);

                this.Controls.Add(p1);
                this.Controls.Add(p2);

                this.Opacity = 0;

                p1.BackColor = p2.BackColor = Color.DeepPink;

                state_changed();
            }

            private void state_changed() {

                var wallSize = getWallSize();

                if (door != null) {
                    door.Vertical = vertical;
                    Size doorSize = getDoorSize();
                    door.Size = vertical ? doorSize : new Size(doorSize.Height, doorSize.Width);

                    Point doorPos = new Point((this.Size.Width - doorSize.Width) / 2, (this.Size.Height - doorSize.Height) / 2);
                    door.Location = vertical ? doorPos : new Point(doorPos.Y, doorPos.X);

                    wallSize = new Size(wallSize.Width, doorPos.Y);
                }

                p1.Size = vertical ? wallSize : new Size(wallSize.Height, wallSize.Width);

                Point p1Pos = new Point((this.Size.Width - wallSize.Width) / 2, 0);
                p1.Location = vertical ? p1Pos : new Point(p1Pos.Y, p1Pos.X);

                if (door == null) {
                    p2.Size = new Size(0, 0);
                } else {
                    p2.Size = p1.Size;
                    Point p2Pos = new Point(p1Pos.X, this.Size.Height - wallSize.Height);
                    p2.Location = vertical ? p2Pos : new Point(p2Pos.Y, p2Pos.X);
                }
            }
        }
        private class Wall2 : Wall1 {
            static Color[] colors = { Color.Aqua, Color.Blue, Color.Chocolate, Color.Green, Color.Gray, Color.Gold };
            static Random rnd = new Random(DateTime.Now.Millisecond);

            protected void on_click(Object Sender, EventArgs e) {
                Color res;
                do {
                    res = colors[rnd.Next() % colors.Length];
                } while (res == p1.BackColor);
                p1.BackColor = p2.BackColor = res;
            }

            public Wall2() {
                p1.BackColor = p2.BackColor = colors[rnd.Next() % colors.Length];
                p1.Click += on_click;
                p2.Click += on_click;
            }
        }
        private class Wall3 : Wall1 {
            private PictureBox pic1 = new PictureBox();
            private PictureBox pic2 = new PictureBox();

            protected void on_click(Object Sender, EventArgs e) {
                System.Media.SystemSounds.Beep.Play();
            }

            public override bool Vertical {
                get { return base.Vertical; }
                set {
                    base.Vertical = value;
                    pic1.Location = pic2.Location = new Point(base.Vertical ? 1 : 3, base.Vertical ? 1 : -1);
                }
            }

            public Wall3() {
                p1.Click += on_click;
                p2.Click += on_click;
                //String PicturePath = @"D:\Книги\Прога\Даша\8 семестр\Patterns\AbstractFactory\MazeImplementation1\note.png";
                String PicturePath = @"\\Mac\Home\Downloads\Patterns-master\AbstractFactory\MazeImplementation1\note.png";

                pic1.Click += on_click;
                pic2.Click += on_click;
                Image note = Image.FromFile(PicturePath);
                pic1.Image = pic2.Image = note;
                pic1.Size = pic1.MaximumSize = pic2.Size = pic2.MaximumSize = note.Size;
                pic1.Location = pic2.Location = new Point(1, -1);

                p1.Controls.Add(pic1);
                p2.Controls.Add(pic2);
            }
        }

        private class Maze1 : TranspCtrl, Maze {
            private System.Drawing.Size mazeSize;
            private Wall1[,] walls;

            public static Point getPos(int row, int column) {
                Point pos = new Point();
                int row_rest_by_2 = (row % 2);
                int leftShift = ((1 - row_rest_by_2) * Wall1.getWallSize().Height / 2);
                int topShift = -row_rest_by_2 * (Wall1.getWallSize().Height + Wall1.getWallSize().Width) / 2;
                pos.X = column * Wall1.getWallSize().Height + leftShift;
                pos.Y = ((row + 1)/2) * (Wall1.getWallSize().Height + Wall1.getWallSize().Width) + topShift;
                return pos;
            }

            public Maze1() {
                this.Padding = new Padding(0);
                mazeSize = new Size(0, 0);
                walls = new Wall1[0, 0];
                this.Opacity = 0;
            }

            public virtual Size MazeSize {
                get { return mazeSize; }
                set {
                    if (mazeSize == value) return;
                    this.Controls.Clear();
                    mazeSize = value;
                    walls = new Wall1[mazeSize.Width, mazeSize.Height];

                    if (mazeSize.Width == 0 || mazeSize.Height == 0) {
                        Size = new Size(0, 0);
                        return;
                    }

                    var pos_for_the_last = getPos(mazeSize.Width - 1, mazeSize.Height - 1);
                    Size = new Size(pos_for_the_last.X + Wall1.getWallSize().Height, pos_for_the_last.Y + Wall1.getWallSize().Height);
                }
            }

            public virtual Wall this[int row, int column] {
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
                    wall.Location = getPos(row, column);

                    this.Controls.Remove(old_wall);
                    this.Controls.Add(wall);
                }
            }
        }
        private class Maze2 : Maze1 {
            private Point startP;
            private Point posShift = new Point(50, 50);
            private Button clearButton;

            public override Size MazeSize {
                get { return base.MazeSize; }
                set {
                    base.MazeSize = value;
                    this.Size = new Size(this.Size.Width + posShift.X, this.Size.Height + posShift.Y);

                    if (clearButton.Parent != this) this.Controls.Add(clearButton);
                }
            }

            public override Wall this[int row, int column] {
                get { return base[row, column]; }
                set {
                    var old = base[row, column];
                    if (old == value) return;
                    base[row, column] = value;

                    Control ctrl_value = (value as Control);
                    if (ctrl_value == null) return;

                    ctrl_value.Location = new Point(
                        ctrl_value.Location.X + posShift.X,
                        ctrl_value.Location.Y + posShift.Y
                    );

                    ctrl_value.MouseDown += mouseDown;
                    ctrl_value.MouseMove += mouseMove;
                }
            }

            public Maze2() {
                this.MouseMove += mouseMove;
                this.MouseDown += mouseDown;
                this.SizeChanged += sizeChanged;

                clearButton = new Button();
                clearButton.Text = "Clear";
                clearButton.Click += clearClick;
                clearButton.Location = new Point(15, 15);

                this.Controls.Add(clearButton);
            }

            private void mouseDown(object sender, MouseEventArgs e) {
                Control ctrl_sender = (sender as Control);
                if (sender == null) return;

                Point offset = new Point();
                if (ctrl_sender != this) {
                    offset = ctrl_sender.Location;
                }

                startP = new Point(e.X + offset.X, e.Y + offset.Y);
            }
            private void mouseMove(object sender, MouseEventArgs e) {
                if (e.Button == MouseButtons.Left) {
                    Control ctrl_sender = (sender as Control);
                    if (sender == null) return;

                    Point offset = new Point();
                    if (ctrl_sender != this) {
                        offset = ctrl_sender.Location;
                    }
                    Point endP = new Point(e.X + offset.X, e.Y + offset.Y);

                    Graphics g = this.CreateGraphics();
                    g.DrawLine(new Pen(Color.BlueViolet), startP, endP);

                    startP = endP;
                    this.Refresh();
                }
            }

            private void clear() {
                this.Opacity = 0;
            }

            private void sizeChanged(object sender, EventArgs e) {
                clear();
            }
            private void clearClick(object sender, EventArgs e){
                clear();
            }
        }

        public override Maze createMaze() { return new Maze2(); }

        public override Wall createWall(WallType type) {
            if (type == WallType.Type2) return new Wall2();
            if (type == WallType.Type3) return new Wall3();
            return new Wall1();
        }

        public override Door createDoor() { return new Door1(); }

        public override void drawMaze(Maze maze) {
            if (maze == null) throw new ArgumentNullException();
            Control my_maze = (maze as Control);
            if (my_maze == null) throw new Exception("value is not Controll entity");

            Form form = new Form();
            form.AutoSize = true;
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            form.Controls.Add(my_maze);

            Application.Run(form);
        }
    }
}
