using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Measurements.UI.Desktop.Components
{
    public class DropDownMenuItem : ToolStripMenuItem
    {
        private bool _isMultiCheked;
        private ToolStripMenuItem clickedItem;

        public DropDownMenuItem(string displayedName, string[] options, bool isMultiCheked = false) : base()
        {
            Name = $"SessionFormMenu{displayedName}";
            Text = displayedName;
            CheckOnClick = true;
            _isMultiCheked = isMultiCheked;
            CheckedChanged += ClearCheckings;

            DropDownItems.AddRange(ConvertStringToItems(options));
        }

        private ToolStripMenuItem[] ConvertStringToItems(string[] options)
        {
            var toolStripMenuItems = new List<ToolStripMenuItem>();

            foreach (var option in options)
            {
                toolStripMenuItems.Add(new ToolStripMenuItem { Name = $"{this.Name}{option}", Text = option, CheckOnClick = true});
                toolStripMenuItems.Last().CheckedChanged += WhichCheked;
            }

            return toolStripMenuItems.ToArray();
        }


        //TODO: re-think mechanism for keeping only one cheked item
        private void ClearCheckings(object sender, EventArgs eventArgs)
        {
            if (_isMultiCheked) return;

            if (DropDownItems == null && DropDownItems.Count == 0)
                return;

            foreach (var childItem in DropDownItems)
            {
                ToolStripMenuItem curTM = (ToolStripMenuItem)childItem;
                if (childItem.Equals(clickedItem))
                {
                    curTM.Checked = true;
                    continue;
                }
                curTM.Checked = false;
            }

        }

        private void WhichCheked(object sender, EventArgs eventArgs)
        {
            if (_isMultiCheked) return;

            if (sender is ToolStripMenuItem)
                clickedItem = (ToolStripMenuItem)sender;
        }

    }
}
