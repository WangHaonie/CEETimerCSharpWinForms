using System;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class ContextMenuExtensions
    {
        public static ContextMenuStrip AddItem(this ContextMenuStrip ContextMenu, string Text, EventHandler OnClickHandler)
        {
            ContextMenu.Items.Add(Text, null, OnClickHandler);
            return ContextMenu;
        }

        public static ContextMenuStrip AddSeparator(this ContextMenuStrip ContextMenu)
        {
            ContextMenu.Items.Add(new ToolStripSeparator());
            return ContextMenu;
        }

        public static ContextMenuStrip AddSubMenu(this ContextMenuStrip ContextMenu, string Text, Action<ToolStripMenuItem> CreateSubMenu)
        {
            var SubMenuItem = new ToolStripMenuItem(Text);
            CreateSubMenu(SubMenuItem);
            ContextMenu.Items.Add(SubMenuItem);
            return ContextMenu;
        }

        public static ToolStripMenuItem AddItem(this ToolStripMenuItem MenuItem, string Text, EventHandler OnClickHandler)
        {
            MenuItem.DropDownItems.Add(Text, null, OnClickHandler);
            return MenuItem;
        }

        public static ToolStripMenuItem AddSeparator(this ToolStripMenuItem MenuItem)
        {
            MenuItem.DropDownItems.Add(new ToolStripSeparator());
            return MenuItem;
        }
    }
}
