using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;
using System.Windows.Forms;

namespace DecoratorLab {

    public class Decorator : Control
    {
        private Control control;

        public Control getControl() { return control; }

        public Decorator(Control control) {
            if (control == null) throw new System.ArgumentNullException();
            this.control = control;
        }

        //-------------------------------------------------------------------------------

        public override string ToString() { return control.ToString(); }

        //-------------------------------------------------------------------------------

        public override bool AllowDrop { get { return control.AllowDrop; } set { control.AllowDrop = value; } }
        public override AnchorStyles Anchor { get { return control.Anchor; } set { control.Anchor = value; } }
        public override Point AutoScrollOffset { get { return control.AutoScrollOffset; } set { control.AutoScrollOffset = value; } }
        public override bool AutoSize { get { return control.AutoSize; } set { control.AutoSize = value; } }
        public override Color BackColor { get { return control.BackColor; } set { control.BackColor = value; } }
        public override Image BackgroundImage { get { return control.BackgroundImage; } set { control.BackgroundImage = value; } }
        public override ImageLayout BackgroundImageLayout { get { return control.BackgroundImageLayout; } set { control.BackgroundImageLayout = value; } }
        public override BindingContext BindingContext { get { return control.BindingContext; } set { control.BindingContext = value; } }
        public override ContextMenu ContextMenu { get { return control.ContextMenu; } set { control.ContextMenu = value; } }
        public override ContextMenuStrip ContextMenuStrip { get { return control.ContextMenuStrip; } set { control.ContextMenuStrip = value; } }
        public override Rectangle DisplayRectangle { get { return control.DisplayRectangle; } }
        public override DockStyle Dock { get { return control.Dock; } set { control.Dock = value; } }
        public override bool Focused { get { return control.Focused; } }
        public override Font Font { get { return control.Font; } set { control.Font = value; } }
        public override Color ForeColor { get { return control.ForeColor; } set { control.ForeColor = value; } }
        public override LayoutEngine LayoutEngine { get { return control.LayoutEngine; } }
        public override Size MaximumSize { get { return control.MaximumSize; } set { control.MaximumSize = value; } }
        public override Size MinimumSize { get { return control.MinimumSize; } set { control.MinimumSize = value; } }
        public override RightToLeft RightToLeft { get { return control.RightToLeft; } set { control.RightToLeft = value; } }
        public override ISite Site { get { return control.Site; } set { control.Site = value; } }
        public override string Text { get { return control.Text; } set { control.Text = value; } }
        public override Size GetPreferredSize(Size proposedSize) { return control.GetPreferredSize(proposedSize); }
        public override bool PreProcessMessage(ref Message msg) { return control.PreProcessMessage(ref msg); }
        public override void Refresh() { control.Refresh(); }
        public override void ResetBackColor() { control.ResetBackColor(); }
        public override void ResetCursor() { control.ResetCursor(); }
        public override void ResetFont() { control.ResetFont(); }
        public override void ResetForeColor() { control.ResetForeColor(); }
        public override void ResetRightToLeft() { control.ResetRightToLeft(); }
        public override void ResetText() { control.ResetText(); }
    }

}
