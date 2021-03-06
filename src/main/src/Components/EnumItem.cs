﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace Regata.Desktop.WinForms.Components

{
    public class EnumItem
    {
        public ToolStripMenuItem EnumMenuItem;
        public ToolStripStatusLabel EnumStatusLabel;
        public event Action<string> CheckedChanged;
        private string _optionName;
        public string CheckedItemText { get; set; }

        public EnumItem(string[] values, string name)
        {
            _optionName = name;
            EnumMenuItem = new ToolStripMenuItem();
            EnumStatusLabel = new ToolStripStatusLabel();

            EnumMenuItem.CheckOnClick = false;

            foreach (var val in values)
            {
                EnumMenuItem.DropDownItems.Add(new ToolStripMenuItem { Text = val, CheckOnClick = true, Name = $"{name}_{val}" });
                EnumMenuItem.DropDownItems.OfType<ToolStripMenuItem>().Last().Click += CheckHandler;
            }

            EnumStatusLabel.Name = $"{name}StatusLabel";
            EnumStatusLabel.Text = "";
            EnumStatusLabel.ToolTipText = $"Текущий {name}";

            EnumMenuItem.Text = name;
            EnumMenuItem.Name = $"{name}OptionItem";

        }

        private void CheckHandler(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;

            foreach (ToolStripMenuItem item in EnumMenuItem.DropDownItems)
            {
                if (item == currentItem)
                {
                    item.Checked = true;
                    EnumStatusLabel.Text = $"{currentItem.Text}||";
                    CheckedItemText = $"{_optionName}: {currentItem.Text}||";
                    CheckedChanged?.Invoke(currentItem.Text);
                }
                else
                    item.Checked = false;
            }
        }
    }
}
