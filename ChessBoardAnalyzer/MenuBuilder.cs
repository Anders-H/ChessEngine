using System;
using System.Windows.Forms;

namespace ChessBoardAnalyzer;

public class MenuBuilder
{
    private readonly ContextMenuStrip _contextMenuStrip;

    public MenuBuilder(ContextMenuStrip contextMenuStrip)
    {
        _contextMenuStrip = contextMenuStrip;
    }

    public ToolStripMenuItem CreateItem(string text, EventHandler handler)
    {
        var m = new ToolStripMenuItem(text);
        m.Click += handler;
        _contextMenuStrip.Items.Add(m);
        return m;
    }

    public void CreateSeparator() =>
        _contextMenuStrip.Items.Add(new ToolStripSeparator());
}