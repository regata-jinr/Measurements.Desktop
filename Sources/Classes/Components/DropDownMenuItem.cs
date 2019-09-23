using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Measurements.Core.Handlers;

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

            DropDownItems.AddRange(ConvertStringToItems(options));
        }

        public ToolStripMenuItem this[string name]
        {
            get
            {
                try
                {
                    return DropDownItems.OfType<ToolStripMenuItem>().First(ti => ti.Text == name);
                }
                catch (Exception e)
                {
                    ExceptionHandler.ExceptionNotify(this, e, ExceptionLevel.Error);
                    return null;
                }

            }
        }

        private ToolStripMenuItem[] ConvertStringToItems(string[] options)
        {
            var toolStripMenuItems = new List<ToolStripMenuItem>();

            foreach (var option in options)
            {
                toolStripMenuItems.Add(new ToolStripMenuItem { Name = $"{this.Name}{option}", Text = option, CheckOnClick = true});
                toolStripMenuItems.Last().Click += ItemClick;
            }

            return toolStripMenuItems.ToArray();
        }


        private void ItemClick(object sender, EventArgs eventArgs)
        {
            if (_isMultiCheked)
            {
                ShowDropDown();
                return;
            }

            var currentItem = sender as ToolStripMenuItem;

            foreach (ToolStripMenuItem item in DropDownItems)
            {
                if (item == currentItem)
                    item.Checked = true;
                else
                    item.Checked = false;
            }
            ShowDropDown();
        }

    }
}
